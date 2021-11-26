using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private Dialogue startDialogue;
    private void Start()
    {
        DialogueController.Instance.StartDialogue(startDialogue);
    }
}
