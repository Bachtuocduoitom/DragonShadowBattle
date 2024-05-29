using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ItemBase
{
    private float speed = 5f;

    private void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < -15f)
        {
            Destroy(gameObject);
        }
    }

   
    public override void HitPlayer()
    {
        Destroy(gameObject);
    }
}
