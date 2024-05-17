using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PointerSpinWheel : MonoBehaviour
{
    private const string CIRCLELIGHTSPINWHEELTAG = "CircleLightSpinWheel";

    private Rigidbody2D rb;
    private float rotationSpeed = 200f;
    private LTDescr currentTween;
    private bool canTrigger = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.gameObject.CompareTag(CIRCLELIGHTSPINWHEELTAG))
        {

            if (currentTween != null)
            {
                LeanTween.cancel(currentTween.id);
            }
            if (!canTrigger) return;

            LeanTween.rotateAround(GetComponent<RectTransform>(), Vector3.forward, -30f, 0.1f)
                .setOnComplete(() =>
                {
                    currentTween = LeanTween.rotateAround(GetComponent<RectTransform>(), Vector3.forward, 360f - GetComponent<RectTransform>().localEulerAngles.z, 0.1f);
                });

            // PLayer spin wheel sound
            AudioManager.Instance.PlaySFX(AudioManager.Instance.cach);
        }
    }

    public void StartPointer()
    {
        canTrigger = true;
    }

    public void StopPointer()
    {
        canTrigger = false;
    }

}
