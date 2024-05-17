using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvaResize : MonoBehaviour
{

    [SerializeField] private CanvasScaler canvasScaler;
    private float screenScale;

    private void Awake()
    {
        screenScale = Screen.width / Screen.height;

        if (screenScale <= 1.47)
        {
            canvasScaler.matchWidthOrHeight = 0;
        }
        else 
        {
            canvasScaler.matchWidthOrHeight = 1;
        }
    }

}
