using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSpinButton : MonoBehaviour
{
    [SerializeField] private Image spinButtonImage;
    void Start()
    {
        RotateSpinButton();
    }

    private void RotateSpinButton()
    {
        LeanTween.rotateAroundLocal(spinButtonImage.gameObject, Vector3.forward, 360f, 2f)
        .setLoopClamp();
    }
}
