using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class GoldAmountPlaySreenUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldAmountText;

    private string formattedMoney;

    private void Start()
    {
        UpdateGoldAmount();
        Player.Instance.OnHitGold += UpdateGoldAmount;
    }

    private string FormatMoney(int amount)
    {
        string formattedM = string.Format("{0:#,###}", amount.ToString());
        return formattedM.Replace(",", ".");
    }

    public void UpdateGoldAmount()
    {
        formattedMoney = Util.GetCurrencyFormat(DataManager.Instance.GetGoldAmount());
        goldAmountText.text = formattedMoney;
    }
}
