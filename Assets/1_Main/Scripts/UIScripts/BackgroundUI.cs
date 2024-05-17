using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundUI : MonoBehaviour
{

    [SerializeField] private Image background;

    private float scale;
    private const float defaultScale = 2.5f;
    private float screenWidth;

    private void Start()
    {
        //Debug.Log("Screen width: " + Screen.width + " Screen height: " + Screen.height);
        scale = Screen.width / background.rectTransform.rect.width;
    }
}
