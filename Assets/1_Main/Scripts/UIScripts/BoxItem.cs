using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoxItem : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI describe;
    [SerializeField] private Image greenFrame;

    [SerializeField] private BoxItemSO boxItemSO;

    private void Start()
    {
        image.sprite = boxItemSO.sprite;
        describe.text = boxItemSO.describe;
        
        HideGreenFrame();
    }

    public void ShowGreenFrame()
    {
        greenFrame.gameObject.SetActive(true);
    }

    public void HideGreenFrame()
    {
        greenFrame.gameObject.SetActive(false);
    }

    public Sprite GetSpriteImage()
    {
        return image.sprite;
    }

    public string GetDescribe()
    {
        return describe.text;
    }
}
