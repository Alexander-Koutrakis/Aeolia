using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemContainer : MonoBehaviour
{
    [SerializeField] private Item item;
    public Item Item { get { return this.item; } }
    [SerializeField] private Item key;
    [SerializeField] private Dialogue focusDialogue;
    [SerializeField] private Action playerAction;
    public Action PlayerAction { get { return this.playerAction; } }
    public bool UsedItem=false;
    public delegate void EnviromentalUnlockAction();
    public EnviromentalUnlockAction EnviromentalUnlock;
    public void Interact()
    {
        if (DialogueController.ShowingDialogue)
        {
            return;
        }

        switch (playerAction) {
            case Action.Look:
                Look();
                break;
            case Action.Take:
                Take();
                break;
            case Action.TryToOpen:
                TryToOpen();
                break;
            case Action.SpeakOnce:
                SpeakOnce();
                break;
            default:
                break;
        }
        
    }

    private void Look()
    {

        Focus.Instance.FocusItem(item);
        
        if (focusDialogue != null)
        {
            if (!UsedItem)
            {
                DialogueController.Instance.StartDialogue(focusDialogue);
            }
            else
            {
                DialogueController.Instance.StartRepeatItemDialogue();
            }
        }
        UsedItem = true;
    }

    private void Take()
    {
        Focus.Instance.FocusItem(item);
        if (focusDialogue != null)
        {
            DialogueController.Instance.StartDialogue(focusDialogue);
        }
        Inventory.Instance.TakeItem(item);
        gameObject.SetActive(false);
        UsedItem = true;
    }

    private void TryToOpen()
    {

        if (Inventory.ItemHolding == null)
        {
            DialogueController.Instance.StartLockedDialogue();
        }
        else if (Inventory.ItemHolding.Name != key.Name)
        {
            DialogueController.Instance.StartLockedDialogue();
        }
        else
        {
            if (item.ItemType == ItemType.ContainerItem)
            {
                Inventory.RemoveItem(Inventory.ItemHolding);
                Inventory.Instance.RemoveHoldingItem();
                Focus.Instance.FocusItem(item.ContainedItem);
                Inventory.Instance.TakeItem(item.ContainedItem);
                Inventory.RemoveItem(item);
                gameObject.SetActive(false);
                Inventory.ItemHolding = null;
                UsedItem = true;
            }
            else if(item.ItemType == ItemType.EnviromentLocked)
            {
                Inventory.RemoveItem(Inventory.ItemHolding);
                Inventory.Instance.RemoveHoldingItem();
                EnviromentalUnlock();
                UsedItem = true;
            }       
        }
    }
    private void SpeakOnce()
    {
        DialogueController.Instance.StartDialogue(focusDialogue);
        gameObject.SetActive(false);
        UsedItem = true;
    }
    public ItemSave SaveItem()
    {
        ItemSave itemSave = new ItemSave(gameObject.name, gameObject.activeSelf,UsedItem);
        return itemSave;
    }

    public void LoadItem(ItemSave itemSave)
    {
        gameObject.SetActive(itemSave.Active);
        UsedItem = itemSave.Used;
        if (UsedItem)
        {
            if (EnviromentalUnlock != null)
            {
                EnviromentalUnlock();
            }
           
        }
    }
}
[System.Serializable]
public enum Action { Take,TryToOpen,Look,SpeakOnce}