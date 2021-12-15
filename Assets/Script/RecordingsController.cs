using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RecordingsController : MonoBehaviour
{
    private static List<Dialogue> recordedDialogues = new List<Dialogue>();
    public static List<Dialogue> RecordedDialogues { get { return recordedDialogues; } }
    [SerializeField] private GameObject recordedSentencePrefab;
    [SerializeField] private GameObject recordedDialoguePrefab;
    private Queue<GameObject> sentencesObjectsToDestroy = new Queue<GameObject>();
    private Queue<GameObject> recordedDialogueButtonToDestroy = new Queue<GameObject>();
    [SerializeField] private RectTransform verticalLayoutArea;
    [SerializeField] private RectTransform gridLayoutArea;
    [SerializeField] private GameObject recordedDialoguePanel;
    [SerializeField] private GameObject recordingsPanel;
    private string lastSpoken;



    private void OnEnable()
    {
        recordedDialoguePanel.SetActive(false);
        recordingsPanel.SetActive(true);
        ShowRecordedDialogues();
    }
    private void OnDisable()
    {
        ClearQueue(recordedDialogueButtonToDestroy);
        ClearQueue(sentencesObjectsToDestroy);
    }
    private void ShowRecordedDialogues()
    {
        ClearQueue(recordedDialogueButtonToDestroy);
        for (int i = 0; i < recordedDialogues.Count; i++)
        {
            ShowRecordedDialogue(recordedDialogues[i]);
        }
    }
    private void ShowRecordedDialogue(Dialogue dialogue)
    {
        GameObject recordedDialoguePrefabClone = Instantiate(recordedDialoguePrefab, gridLayoutArea);
        Image recordingImage = recordedDialoguePrefabClone.GetComponentsInChildren<Image>()[1];
        TMP_Text recordingTitle = recordedDialoguePrefabClone.GetComponentInChildren<TMP_Text>();
        Button recordingButton = recordedDialoguePrefabClone.GetComponent<Button>();

        recordingButton.onClick.AddListener(delegate { ShowDialogue(dialogue.name); });
        recordingButton.onClick.AddListener(delegate { recordedDialoguePanel.SetActive(true); });
        recordingButton.onClick.AddListener(delegate { recordingsPanel.SetActive(false); });
        recordingImage.sprite = dialogue.DialogueSprite;
        recordingImage.preserveAspect = true;
        recordingTitle.text = dialogue.DialogueTitle;

        recordedDialogueButtonToDestroy.Enqueue(recordedDialoguePrefabClone);
    }
    private Dialogue GetDialogue(string dialogueName)
    {
        for(int i = 0; i < recordedDialogues.Count; i++)
        {
            if (recordedDialogues[i].name == dialogueName)
            {
                return recordedDialogues[i];
            }
        }

        return null;
    }
    public void ShowDialogue(string dialogueName)
    {
        Dialogue dialogue = GetDialogue(dialogueName);
        ShowDialogue(dialogue);
    }
    public void ShowDialogue(Dialogue dialogue)
    {
        lastSpoken = null;
        verticalLayoutArea.anchoredPosition = new Vector2(verticalLayoutArea.anchoredPosition.x, 0);
        ClearQueue(sentencesObjectsToDestroy);
        for(int i = 0; i < dialogue.Sentences.Length; i++)
        {
            CreateSentence(dialogue.Sentences[i]);
        }
    }
    private void CreateSentence(DialogueSentence dialogueSentence)
    {
        Sprite interviewerPortrait = Gamemaster.Instance.PlayerPortrait();
        GameObject recordedSentencePrefabClone = Instantiate(recordedSentencePrefab, verticalLayoutArea);
        Image speakerImage = recordedSentencePrefabClone.GetComponentsInChildren<Image>()[1];
        TMP_Text text = recordedSentencePrefabClone.GetComponentInChildren<TMP_Text>();


        if (lastSpoken != dialogueSentence.SpeakerName)
        {
            speakerImage.gameObject.SetActive(true);
            if (dialogueSentence.SpeakerName == "Δημοσιογράφος")
            {
                speakerImage.sprite = interviewerPortrait;
            }
            else
            {
                speakerImage.sprite = dialogueSentence.SpeakerSprite;
            }
        }
        else
        {
            speakerImage.gameObject.SetActive(false);
        }
        lastSpoken = dialogueSentence.SpeakerName;
        text.text = dialogueSentence.Text;
        sentencesObjectsToDestroy.Enqueue(recordedSentencePrefabClone);
    }

    public void BackToDialogues()
    {
        recordedDialoguePanel.SetActive(false);
        recordingsPanel.SetActive(true);
    }

    private void ClearQueue(Queue<GameObject> queueToClear)
    {
        while (queueToClear.Count > 0)
        {
            Destroy(queueToClear.Dequeue());
        }
    }

    public static void RecordDialogue(Dialogue dialogue)
    {
        for(int i = 0; i < recordedDialogues.Count; i++)
        {
            if (recordedDialogues[i].DialogueTitle == dialogue.DialogueTitle)
            {
                return;
            }
        }
        recordedDialogues.Add(dialogue);
    }

    public static void LoadRecordings()
    {
        string[] savedRecordings = Gamemaster.SavedGame.LoadRecordedDialogues();
        for(int i = 0; i < savedRecordings.Length; i++)
        {
            Dialogue dialoguetoSave =Instantiate(Resources.Load<Dialogue>("Dialogues/" + savedRecordings[i]));
            dialoguetoSave.name = savedRecordings[i];
            recordedDialogues.Add(dialoguetoSave);
        }
    }

}
