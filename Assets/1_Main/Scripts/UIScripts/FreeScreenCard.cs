using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FreeScreenCard : MonoBehaviour
{
    [SerializeField] Image fakeButton;
    [SerializeField] Sprite redButton;
    [SerializeField] TextMeshProUGUI fakeButtonText;

    public void DisableCardBeforeClick()
    {
        fakeButton.sprite = redButton;
        gameObject.GetComponent<Button>().interactable = false;
    }

    public void SetCardText(string text)
    {
        fakeButtonText.text = text;
    }
}
