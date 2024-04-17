using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FreeScreen : MonoBehaviour, IScreen
{

    [SerializeField] private Button menuButton;
    [SerializeField] private GoldAmountTouchable goldAmountTouchable;
    [SerializeField] private TextMeshProUGUI goldText;


    private void Awake()
    {
        menuButton.onClick.AddListener(() =>
        {
            ScreenController.Instance.ShowScreen(ScreenController.ScreenType.MenuScreen);
        });

        goldAmountTouchable.OnTouchGoldAmount += () =>
        {
            ScreenController.Instance.ShowScreen(ScreenController.ScreenType.CoinsScreen);
        };
    }

    private void Start()
    {
        goldText.text = Util.GetCurrencyFormat(DataManager.Instance.GetGoldAmount());
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
