using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PointerSpinWheel : MonoBehaviour
{
    private const string CIRCLELIGHTSPINWHEELTAG = "CircleLightSpinWheel";

    private Rigidbody2D rb;
    private float rotationSpeed = 200f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(CIRCLELIGHTSPINWHEELTAG))
        {

            //LeanTween.rotateAround(GetComponent<RectTransform>(), Vector3.forward, -30f, 0.1f)
            //    .setLoopPingPong(1);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.cach);
        }
    }

}
