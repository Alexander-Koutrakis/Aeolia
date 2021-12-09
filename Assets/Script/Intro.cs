using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    [SerializeField] private Dialogue introDialogue;
    
    private void OnEnable()
    {
        DialogueController.Instance.StartDialogue(introDialogue);
        DialogueController.Instance.DialogueEnd = LoadScene;
    }

    private void LoadScene()
    {
        Gamemaster.NewGame();
        SceneLoader.Instance.LoadScene("Aivali");
    }
}
