    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{

    [SerializeField] private SpriteRenderer imgBackgroud;
    [SerializeField] private Sprite[] imgBackgroudList;
    private float speed = 0.5f;

    private void Start()
    {
        imgBackgroud.sprite = imgBackgroudList[Random.Range(0, imgBackgroudList.Length)];

    }

    void Update()
    {
        imgBackgroud.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);    
    }
}
