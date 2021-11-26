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
    private void Awake()
    {
        Instance = this;
        dialogueSpeakerName = dialoguePanel.GetComponentsInChildren<TMP_Text>(true)[0];
        dialogueText = dialoguePanel.GetComponentsInChildren<TMP_Text>(true)[1];
        speakerImage = dialoguePanel.GetComponentsInChildren<Image>(true)[1];
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueIndex = 0;
        this.dialogue = dialogue;
        dialoguePanel.SetActive(true);
        NextSentence();
        InventoryPanelController.Instance.HidePanels();
    }

    public void ShowDialogueSentence(DialogueSentence dialogueSentence)
    {      
        dialogueSpeakerName.text = dialogueSentence.SpeakerName;
        dialogueText.text = dialogueSentence.Text;
        speakerImage.sprite = dialogueSentence.SpeakerSprite;
        
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
        InventoryPanelController.Instance.ShowPanels();
    }

    public void StartLockedDialogue()
    {
        StartDialogue(lockedDialogue);
    }
}
