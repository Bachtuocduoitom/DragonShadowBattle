using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BottomItemsUI : MonoBehaviour
{
    private const string NUMBER_POPUP = "NumberPopUP";
    [SerializeField] private Button beanItemButton;
    [SerializeField] private Button adsItemButton;
    [SerializeField] private TextMeshProUGUI goldDeductionText;

    Animator goldDeductionAnimator;  

    private void Awake()
    {
        goldDeductionAnimator = goldDeductionText.GetComponent<Animator>();
        goldDeductionText.GetComponent<CanvasGroup>().alpha = 0;

        beanItemButton.onClick.AddListener(() =>
        {
            Player.Instance.UseBeanItem();

            goldDeductionAnimator.SetTrigger(NUMBER_POPUP);
        });

    }

    private void Update()
    {
        
    }
}
