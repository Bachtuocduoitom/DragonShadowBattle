using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoxItem : MonoBehaviour
{

    public enum BoxItemTypes
    {
        LevelReward,
        BeanReward,
        CoinReward,
        AdsReward,
        Lose
    }

    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI describe;
    [SerializeField] private Image greenFrame;
    [SerializeField] private BoxItemSO boxItemSO;

    private LTDescr currentTween;

    private void Start()
    {
        image.sprite = boxItemSO.sprite;
        switch (boxItemSO.boxItemType)
        {
            case BoxItemTypes.LevelReward:
                describe.text = "+" + boxItemSO.describe + " lv";
                break;
            case BoxItemTypes.BeanReward:
                describe.text = "+" + boxItemSO.describe;
                break;
            case BoxItemTypes.CoinReward:
                describe.text = "+" + boxItemSO.describe;
                break;
            case BoxItemTypes.AdsReward:
                describe.text = boxItemSO.describe;
                break;
            case BoxItemTypes.Lose:
                describe.text = boxItemSO.describe;
                break;
        }
        
        HideGreenFrame();
    }

    public void ShowGreenFrame()
    {
        greenFrame.gameObject.SetActive(true);
        currentTween = LeanTween.alphaCanvas(greenFrame.GetComponent<CanvasGroup>(), 0.1f, 0.5f).setLoopPingPong(-1);
    }

    public void HideGreenFrame()
    {
        greenFrame.gameObject.SetActive(false);
    }

    public Sprite GetSpriteImage()
    {
        return boxItemSO.sprite;
    }

    public string GetDescribe()
    {
        return describe.text;
    }

    public string GetReward()
    {
        return boxItemSO.describe;
    }

    public BoxItemTypes GetBoxItemType()
    {
        return boxItemSO.boxItemType;
    }
}
