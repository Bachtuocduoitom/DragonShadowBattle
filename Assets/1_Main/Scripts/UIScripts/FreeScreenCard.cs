using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreeScreenCard : MonoBehaviour
{
    [SerializeField] Image fakeButton;
    [SerializeField] Sprite redButton;

    public void DisableCardBeforeClick()
    {
        fakeButton.sprite = redButton;
    }
}
