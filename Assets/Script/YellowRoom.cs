using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowRoom : EnviromentalItem
{
    [SerializeField] private GameObject DoorClosed;
    [SerializeField] private GameObject DoorOpen;
    private void Awake()
    {
        itemContainer = GetComponent<ItemContainer>();
        itemContainer.EnviromentalUnlock = Action;
    }
    public override void Action()
    {
        if (DoorClosed != null)
        {
            DoorClosed.SetActive(false);
        }
        if (DoorOpen != null)
        {
            DoorOpen.SetActive(true);
        }
        
        gameObject.SetActive(false);
    }
}
