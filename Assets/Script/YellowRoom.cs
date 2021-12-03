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
        DoorClosed.SetActive(false);
        DoorOpen.SetActive(true);
        gameObject.SetActive(false);
    }
}
