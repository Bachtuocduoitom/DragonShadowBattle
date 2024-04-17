using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{

    [SerializeField] private SpriteRenderer imgBackgroud;
    private float speed = 0.2f;

    void Update()
    {
        imgBackgroud.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);    
    }
}
