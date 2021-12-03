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
    public bool UsedItem=false;
    public delegate void EnviromentalUnlockAction();
    public EnviromentalUnlockAction EnviromentalUnlock;
    public void Interact()
    {
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
        UsedItem = true;
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
    }

    private void TryToOpen()
    {
        if (Inventory.ItemHolding != key|| Inventory.ItemHolding==null)
        {
            DialogueController.Instance.StartLockedDialogue();
        }
        else
        {
            if (item.ItemType == ItemType.ContainerItem)
            {
                Focus.Instance.FocusItem(item.ContainedItem);
                Inventory.Instance.TakeItem(item.ContainedItem);
                Inventory.Instance.RemoveItem(item);
                gameObject.SetActive(false);
            }
            else if(item.ItemType == ItemType.EnviromentLocked)
            {
                EnviromentalUnlock();
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