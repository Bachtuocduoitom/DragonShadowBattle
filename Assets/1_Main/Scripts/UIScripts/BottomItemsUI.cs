using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BottomItemsUI : MonoBehaviour
{
    private const string NUMBER_POPUP = "NumberPopUP";
    private const int beanCost = 500;

    [SerializeField] private Button beanItemButton;
    [SerializeField] private Button adsItemButton;
    [SerializeField] private TextMeshProUGUI goldDeductionText;
    [SerializeField] private TextMeshProUGUI beanAmount;
    [SerializeField] private GoldAmountPlaySreenUI goldAmount;

    Animator goldDeductionAnimator;  

    private void Awake()
    {
        UpdateBeanAmount();

        goldDeductionAnimator = goldDeductionText.GetComponent<Animator>();
        goldDeductionText.GetComponent<CanvasGroup>().alpha = 0;

        beanItemButton.onClick.AddListener(() =>
        {  
            if (DataManager.Instance.TryDecreaseBeanAmount(1))
            {
                Player.Instance.UseBeanItem();
                UpdateBeanAmount();
            } else
            {
                if (DataManager.Instance.TryDecreaseGoldAmount(beanCost))
                {
                    goldDeductionAnimator.SetTrigger(NUMBER_POPUP);
                    Player.Instance.UseBeanItem();
                    goldAmount.UpdateGoldAmount();
                } 
            }
           

            
        });

    }

    public void UpdateBeanAmount()
    {
        beanAmount.text = Util.GetCurrencyFormat(DataManager.Instance.GetBeanAmount());
    }
}
