using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public Sprite Sprite;
    public string Name;
    public ItemType ItemType;
}

public enum ItemKey { Key1,Key2,Key3}
public enum ItemType { KeyItem,ContainerItem,BackgroundItem}