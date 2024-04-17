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
    [SerializeField] private CoinsCardSO[] coinsCardSOList;

    private void Awake()
    {
        shopButton.onClick.AddListener(() =>
        {
            ScreenController.Instance.ShowScreen(ScreenController.ScreenType.TransformScreen);
        });
       

    }
    private void Start()
    {
        goldText.text = Util.GetCurrencyFormat(DataManager.Instance.GetGoldAmount());

        for (int i = 0; i < coinsCardSOList.Length; i++)
        {
            CoinsCardSO coinsCardSO = coinsCardSOList[i];
            CoinCardUI coinCardUIInstance = Instantiate(coinCardUI, content);
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


    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
