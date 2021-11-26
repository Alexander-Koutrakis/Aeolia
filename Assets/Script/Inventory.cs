using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item> inventoryItems=new List<Item>();
    private Item itemHolding;
    public Item ItemHolding { get { return this.ItemHolding; } }
    public static Inventory Instance;

    private void Awake()
    {
        Instance = this;
    }
    public void TakeItem(Item item)
    {
        if (!inventoryItems.Contains(item))
        {
            inventoryItems.Add(item);
        }
    }

    public void HoldItem(Item item)
    {
        itemHolding = item;
    }
}
