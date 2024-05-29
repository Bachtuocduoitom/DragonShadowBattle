using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    private ItemSpawner.ItemType itemType;

    public void SetItemType(ItemSpawner.ItemType itemType)
    {
        this.itemType = itemType;
    }

    public ItemSpawner.ItemType GetItemType()
    {
        return itemType;
    }

    public virtual void HitPlayer()
    {
    }
}
