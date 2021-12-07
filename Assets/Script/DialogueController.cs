using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class DialogueController : MonoBehaviour
{
    private TMP_Text dialogueText;
    private TMP_Text dialogueSpeakerName;
    private Image speakerImage;
    [SerializeField]private GameObject dialoguePanel;
    public static DialogueController Instance;
    private int dialogueIndex;
    private Dialogue dialogue;
    [SerializeField]private Dialogue lockedDialogue;
    [SerializeField]private Dialogue repeatItemDialogue;
    [SerializeField] private Dialogue repeatSceneDialogue;

    public void Awake()
    {
        Instance = this;
        dialogueSpeakerName = dialoguePanel.GetComponentsInChildren<TMP_Text>(true)[0];
        dialogueText = dialoguePanel.GetComponentsInChildren<TMP_Text>(true)[0];
        speakerImage = dialoguePanel.GetComponentsInChildren<Image>(true)[1];
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueIndex = 0;
        this.dialogue = dialogue;
        dialoguePanel.SetActive(true);
        NextSentence();
       // PanelController.Instance.HidePanels();
    }

    public void ShowDialogueSentence(DialogueSentence dialogueSentence)
    {      
        dialogueText.text = dialogueSentence.Text;
        if (dialogueSentence.SpeakerName == "Δημοσιογράφος")
        {
            speakerImage.sprite = Gamemaster.PlayerPortraitSprite;
        }
        else
        {
            speakerImage.sprite = dialogueSentence.SpeakerSprite;
        }        
    }

    public void NextSentence()
    {
        if (dialogueIndex < dialogue.Sentences.Length)
        {
            ShowDialogueSentence(this.dialogue.Sentences[dialogueIndex]);
            dialogueIndex++;
        }
        else
        {
            EndDialogue();
        }
        
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        PanelController.Instance.ShowPanels();
        if (dialogue.ScenesToUnlock == null)
        {
            return;
        }

       for(int i = 0; i < dialogue.ScenesToUnlock.Length; i++)
       {         
            if (!SceneNavigation.AvailableScenes.Contains(dialogue.ScenesToUnlock[i]))
            {
                SceneNavigation.AddAvailableScene(dialogue.ScenesToUnlock[i]);
            }      
       }
    }

    public void StartLockedDialogue()
    {
        StartDialogue(lockedDialogue);
    }

    public void StartRepeatItemDialogue()
    {
        StartDialogue(repeatItemDialogue);
    }

    public void StartRepeatSceneDialogue()
    {
        StartDialogue(repeatSceneDialogue);
    }
}
