using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerController : MonoBehaviour
{

    private void OnEnable()
    {
        foreach(ItemContainer itemContainer in GetComponentsInChildren<ItemContainer>(true))
        {
            if (Inventory.InventoryItems.Contains(itemContainer.Item))
            {
                itemContainer.gameObject.SetActive(true);
            }
            else
            {
                itemContainer.gameObject.SetActive(false);
            }
        }
    }
}
