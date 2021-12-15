using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Dialogue : ScriptableObject
{
    public string DialogueTitle;
    public Sprite DialogueSprite;
    public DialogueSentence[] Sentences;
    public SceneData[] ScenesToUnlock;
    public bool Record;
}

[System.Serializable]
public struct DialogueSentence {
    public string SpeakerName;
    public Sprite SpeakerSprite;
    [TextArea(5,20)]
    public string Text;
   
}

