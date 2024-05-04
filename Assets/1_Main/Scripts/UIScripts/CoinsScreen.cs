using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinsScreen : MonoBehaviour, IScreen
{

    [SerializeField] private Button shopButton;
    [SerializeField] private GoldAmountTouchable goldAmountTouchable;
    [SerializeField] private CoinCardUI coinCardUI;
    [SerializeField] private RectTransform content;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private PurchasePopup purchasePopup;
    [SerializeField] private CoinsCardSO[] coinsCardSOList;

    private void Awake()
    {
        shopButton.onClick.AddListener(() =>
        {
            ScreenController.Instance.ShowScreenWithTransition(ScreenController.ScreenType.TransformScreen);

            AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        });
       

    }
    private void Start()
    {
        goldText.text = Util.GetCurrencyFormat(DataManager.Instance.GetGoldAmount());

        for (int i = 0; i < coinsCardSOList.Length; i++)
        {
            CoinsCardSO coinsCardSO = coinsCardSOList[i];
            CoinCardUI coinCardUIInstance = Instantiate(coinCardUI, content);
            coinCardUIInstance.GetComponentInChildren<Button>().onClick.AddListener(() =>
            {
                purchasePopup.PurchaseSomething(this.HandlePurchase, coinsCardSO);

                AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        
            });
            coinCardUIInstance.SetCoinsCardSO(coinsCardSO);
            switch (i)
            {
                case 1:
                    coinCardUIInstance.SetLabel(CoinCardUI.LabelType.MostValue);
                    break;
                case 4:
                    coinCardUIInstance.SetLabel(CoinCardUI.LabelType.Popular);
                    break;
                case 6:
                    coinCardUIInstance.SetLabel(CoinCardUI.LabelType.BestDeal);
                    break;
                case 9:
                    coinCardUIInstance.SetLabel(CoinCardUI.LabelType.CleanAds);
                    break;
                case 10:
                    coinCardUIInstance.SetLabel(CoinCardUI.LabelType.MostUsed);
                    break;
            }
        }
    }

    public void HandlePurchase(CoinsCardSO coinsCardSO)
    {
        switch (coinsCardSO.cardType)
        {
            case CoinCardUI.CoinsCardType.BeanAndGold:
                DataManager.Instance.IncreaseGoldAmount(coinsCardSO.goldGainText);
                DataManager.Instance.IncreaseBeanAmount(coinsCardSO.goldGainText);
                goldText.text = Util.GetCurrencyFormat(DataManager.Instance.GetGoldAmount());
                break;
            case CoinCardUI.CoinsCardType.Gold:
                DataManager.Instance.IncreaseGoldAmount(coinsCardSO.goldGainText);
                goldText.text = Util.GetCurrencyFormat(DataManager.Instance.GetGoldAmount());
                break;
            case CoinCardUI.CoinsCardType.Ads:
                DataManager.Instance.IncreaseGoldAmount(coinsCardSO.goldGainText);
                goldText.text = Util.GetCurrencyFormat(DataManager.Instance.GetGoldAmount());
                break;
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public bool IsShowed()
    {
        return gameObject.activeSelf;
    }
}
