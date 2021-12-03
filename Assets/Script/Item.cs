using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public Sprite FocusSprite;
    public Sprite HoldingSprite;
    public string Name;
    public ItemType ItemType;
    public Item ContainedItem;
}

public enum ItemKey { Key1,Key2,Key3}
public enum ItemType { KeyItem,ContainerItem,BackgroundItem,EnviromentLocked}