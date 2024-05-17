using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PurchasePopup : MonoBehaviour, IScreen
{
    [SerializeField] private TextMeshProUGUI purchaseText;
    [SerializeField] private TextMeshProUGUI purchasingText;
    [SerializeField] private Button okButton;
    [SerializeField] private BlackBackroundTouchable blackBackgroundTouchable;

    private bool isPurchasing = false;

    private void Start()
    {
        okButton.onClick.AddListener(() =>
        {
            Hide();

            AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        });

        blackBackgroundTouchable.OnTouchBlackBackground += () =>
        {
            if (isPurchasing) return;

            Hide();
        };
    }

    public void PurchaseSomething(Action<CoinsCardSO> PurchaseCallback, CoinsCardSO coinsCardSO)
    {
        Show();
        purchaseText.gameObject.SetActive(false);
        purchasingText.gameObject.SetActive(true);
        okButton.gameObject.SetActive(false);

        isPurchasing = true;
        purchasingText.text = "Purchasing .";

        LeanTween.delayedCall(0.3f, () =>
        {
            purchasingText.text = "Purchasing . .";
        });
        LeanTween.delayedCall(0.6f, () =>
        {
            purchasingText.text = "Purchasing . . .";
        });
        LeanTween.delayedCall(0.9f, () =>
        {
            purchasingText.text = "Purchasing . . . .";
        });
        LeanTween.delayedCall(1.2f, () =>
        {
            isPurchasing = false;
            purchaseText.gameObject.SetActive(true);
            purchasingText.gameObject.SetActive(false);
            okButton.gameObject.SetActive(true);

            purchaseText.text = "Purchase Successful";

            PurchaseCallback(coinsCardSO);

            // Play sound
            AudioManager.Instance.PlaySFX(AudioManager.Instance.ka_ching);
        });
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
