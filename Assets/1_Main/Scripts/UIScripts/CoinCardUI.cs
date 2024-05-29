using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinCardUI : MonoBehaviour
{
    public enum CoinsCardType
    {
        BeanAndGold,
        Gold,
        Ads
    }

    public enum LabelType
    {
        MostValue,
        Popular,
        BestDeal,
        CleanAds,
        MostUsed
    }

    [SerializeField] private Image cardIcon;
    [SerializeField] private Image goldIcon;
    [SerializeField] private TextMeshProUGUI goldGainText;
    [SerializeField] private TextMeshProUGUI goldGainTextButBigger;
    [SerializeField] private TextMeshProUGUI cardValueText;
    [SerializeField] private TextMeshProUGUI cardCost;
    [SerializeField] private RectTransform blueLabel;
    [SerializeField] private TextMeshProUGUI blueLabelText;
    [SerializeField] private RectTransform redLabel;
    [SerializeField] private TextMeshProUGUI redLabelText; 

    public void SetCoinsCardSO(CoinsCardSO coinsCardSO)
    {
        cardIcon.sprite = coinsCardSO.cardIcon;
        cardIcon.SetNativeSize();
        cardCost.text = "$ " + coinsCardSO.cardCost;

        switch (coinsCardSO.cardType)
        {
            case CoinsCardType.BeanAndGold:
                goldGainText.text = "+" + Util.GetCurrencyFormat(coinsCardSO.goldGainText);
                cardValueText.text = coinsCardSO.cardValueText + " bean";
                break;
            case CoinsCardType.Ads:
                cardIcon.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
                goldGainText.text = "+" + Util.GetCurrencyFormat(coinsCardSO.goldGainText);
                cardValueText.text = coinsCardSO.cardValueText;
                break;
            case CoinsCardType.Gold: 
                goldGainTextButBigger.text = Util.GetCurrencyFormat(coinsCardSO.goldGainText);
                goldIcon.gameObject.SetActive(false);
                break;
            
        }
        
    } 

    public void SetLabel(LabelType labelType)
    {
        switch (labelType)
        {
            case LabelType.MostValue:
                redLabel.gameObject.SetActive(true);
                redLabelText.text = "Most Value";
                break;
            case LabelType.Popular:
                redLabel.gameObject.SetActive(true);
                redLabelText.text = "Popular";
                break;
            case LabelType.BestDeal:
                redLabel.gameObject.SetActive(true);
                redLabelText.text = "Best Deal";
                break;
            case LabelType.CleanAds:
                blueLabel.gameObject.SetActive(true);
                blueLabelText.text = "Clean Ads";
                break;
            case LabelType.MostUsed:
                blueLabel.gameObject.SetActive(true);
                blueLabelText.text = "Most Used";
                break;
        }
    }

}
