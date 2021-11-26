using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemContainer : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private Item key;
    [SerializeField] private Dialogue focusDialogue;
    [SerializeField] private Action playerAction;
    public bool UsedItem=false;

    public void Interact()
    {

        if (item.ItemType == ItemType.BackgroundItem)
        {
            Focus.Instance.FocusItem(item);
            if (focusDialogue != null)
            {
                DialogueController.Instance.StartDialogue(focusDialogue);
            }
            UsedItem = true;
            return;
        }

        if (playerAction == Action.Take)
        {
            Focus.Instance.FocusItem(item);
            if (focusDialogue != null)
            {
                DialogueController.Instance.StartDialogue(focusDialogue);
            }
            Inventory.Instance.TakeItem(item);
            gameObject.SetActive(false);
            UsedItem = true;
        }else if (playerAction == Action.TryToOpen)
        {
            if (Inventory.Instance.ItemHolding == key)
            {
                DialogueController.Instance.StartLockedDialogue();
            }
            else
            {
                Focus.Instance.FocusItem(item);
                Inventory.Instance.TakeItem(item);
            }
        }
       
    }
}
[System.Serializable]
public enum Action { Take,TryToOpen}
