using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkLightUI : MonoBehaviour
{

    [SerializeField] private Image lightOnImage;
    [SerializeField] private float delayTime = 0;

    private void Start()
    {
        PlayLightOnTween();
    }

    public void PlayLightOnTween()
    {
        LeanTween.alpha(lightOnImage.rectTransform, 0, 0.5f)
            .setDelay(delayTime)
            .setLoopPingPong();
    }
}
