using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWheel : MonoBehaviour
{
    public Action<Sprite> OnSpinWheelFinished;

    [SerializeField] private GameObject listBoxItem;
    [SerializeField] public AnimationCurve animationCurve;

    private float numberOfItem = 8f;
    private float timeToSpin = 5f;
    private float numberofCircleRotate = 5;

    private float CIRCLE = 360f;
    public float anglePerItem;

    private float targetItem;
    private bool canSpin = true;

    private void Start()
    {
        anglePerItem = CIRCLE / numberOfItem;
    }


    [ContextMenu("Spin")]
    public void Spin()
    {
        if (!canSpin) return;

        StartCoroutine(SpinWheelEffect());
    }

    private IEnumerator SpinWheelEffect()
    {
        float startAngle = transform.eulerAngles.z;
        targetItem = UnityEngine.Random.Range(0, numberOfItem);
        float endAngle = startAngle + numberofCircleRotate * CIRCLE + targetItem * anglePerItem;
        float t = 0f;

        while (t < timeToSpin)
        {
            t += Time.deltaTime;
            float zRotation = Mathf.Lerp(startAngle, endAngle, animationCurve.Evaluate(t / timeToSpin)) % 360.0f;
            transform.eulerAngles = new Vector3(0, 0, zRotation);
            yield return null;
        }

        // Get the item that the wheel is pointing to
        int index = Mathf.RoundToInt(Mathf.Abs(transform.eulerAngles.z % 360) / anglePerItem);
        if (index == numberOfItem)
        {
            index = 0;
        }
        
        listBoxItem.transform.GetChild(index).GetComponent<BoxItem>().ShowGreenFrame();
        OnSpinWheelFinished?.Invoke(listBoxItem.transform.GetChild(index).GetComponent<BoxItem>().GetSpriteImage());
        canSpin = false;
    }

    public void ResetWheel()
    {
        transform.eulerAngles = Vector3.zero;
    }

}
