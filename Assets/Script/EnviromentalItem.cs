using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnviromentalItem : MonoBehaviour
{
    protected ItemContainer itemContainer;

    public abstract void Action();
}
