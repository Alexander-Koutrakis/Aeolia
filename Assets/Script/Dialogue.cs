using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Dialogue : ScriptableObject
{
    public DialogueSentence[] Sentences;
}

[System.Serializable]
public struct DialogueSentence {
    public string SpeakerName;
    public Sprite SpeakerSprite;
    [TextArea]
    public string Text;
}

