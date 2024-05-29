using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWheel : MonoBehaviour
{
    public Action<BoxItem.BoxItemTypes, Sprite, string, string> OnSpinWheelFinished;

    [SerializeField] private GameObject listBoxItem;
    [SerializeField] public AnimationCurve animationCurve;

    private readonly float numberOfItem = 8f;
    private readonly float timeToSpin = 5f;
    private readonly int numberofCircleRotate = 5;
    private readonly float CIRCLE = 360f;

    public float anglePerItem;
    private float targetItem;
    private BoxItem currentRewardBoxItem;

    private void Start()
    {
        anglePerItem = CIRCLE / numberOfItem;
    }


    [ContextMenu("Spin")]
    public void Spin()
    {
        if (currentRewardBoxItem != null)
        {
            currentRewardBoxItem.HideGreenFrame();
        }

        StartCoroutine(SpinWheelEffect());
    }

    private IEnumerator SpinWheelEffect()
    {
        float startAngle = transform.eulerAngles.z;
        targetItem = UnityEngine.Random.Range(0, numberOfItem);
        //targetItem = 4.62f;
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
        float index = Mathf.Abs(transform.eulerAngles.z % 360) / anglePerItem;
        if ( Mathf.RoundToInt(index) - index < 0.315f || Mathf.RoundToInt(index) - index < 0)
        {
            index = Mathf.RoundToInt(index);
        } else
        {
            index = Mathf.RoundToInt(index) - 1;
        }

        if (index == numberOfItem)
        {
            index = 0;
        }
        BoxItem boxItem = listBoxItem.transform.GetChild((int)index).GetComponent<BoxItem>();
        if (boxItem != null)
        {
            boxItem.ShowGreenFrame();
            currentRewardBoxItem = boxItem;
        }
        OnSpinWheelFinished?.Invoke(boxItem.GetBoxItemType(),
            boxItem.GetSpriteImage(),
            boxItem.GetDescribe(),
            boxItem.GetReward());
    }

    public void ResetWheel()
    {
        transform.eulerAngles = Vector3.zero;
    }

}
