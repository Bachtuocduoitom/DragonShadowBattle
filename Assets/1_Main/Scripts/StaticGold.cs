using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticGold : MonoBehaviour
{
    private float speed = 300f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.left * speed);
    }

    private void Update()
    {

        if (transform.position.x < -15f)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
       
    }


}
