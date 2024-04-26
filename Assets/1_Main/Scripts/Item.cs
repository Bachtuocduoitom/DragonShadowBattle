using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private float speed = 5f;
    private ItemSpawner.ItemType itemType;

    private void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < -15f)
        {
            Destroy(gameObject);
        }
    }

    public void SetItemType(ItemSpawner.ItemType itemType)
    {
        this.itemType = itemType;
    }

    public ItemSpawner.ItemType GetItemType()
    {
        return itemType;
    }

    public void HitPlayer()
    {
        Destroy(gameObject);
    }
}
