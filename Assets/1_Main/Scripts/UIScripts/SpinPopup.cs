using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpinPopup : MonoBehaviour, IScreen
{

    [SerializeField] private SpinWheel spinWheel;
    [SerializeField] private PointerSpinWheel pointer;
    [SerializeField] private Button spinButton;
    [SerializeField] private BlackBackroundTouchable blackBackroundTouchable;
    [SerializeField] private RectTransform itemResult;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemDescribe;

    private bool canSpin = true;
    private bool onSpin = false;
    private float rewardScaleUp = 1.5f;

    private void Awake()
    {
        spinButton.onClick.AddListener(() =>
        {
            if (!canSpin)
            {
                return;
            }

            spinWheel.Spin();
            canSpin = false;
            onSpin = true;
            spinButton.gameObject.SetActive(false);
            pointer.StartPointer();

            AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        });

        blackBackroundTouchable.OnTouchBlackBackground += () =>
        {
            if (onSpin) return;

            Hide();
        };

        spinWheel.OnSpinWheelFinished += (boxItemType, itemSprite, describe, reward) =>
        {
            spinButton.gameObject.SetActive(true);
            
            spinButton.GetComponent<SpinButton>().SetAdsSpin();
            LeanTween.delayedCall(0.5f, () =>
            {
                ShowItemResult(itemSprite, describe);
                GetReward(boxItemType, reward);
            });

            pointer.StopPointer();

            onSpin = false;

            AudioManager.Instance.PlaySFX(AudioManager.Instance.win);
        };
    }

    private void Start()
    {
        spinButton.GetComponent<SpinButton>().SetFreeSpin();
    }

    public void Show()
    {
        gameObject.SetActive(true);

        spinWheel.ResetWheel();

        itemResult.gameObject.SetActive(false);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void ShowItemResult(Sprite itemSprite, string describe)
    {
        itemImage.sprite = itemSprite;
        itemDescribe.text = describe;
        itemResult.gameObject.SetActive(true);

        LeanTween.scale(itemResult, new Vector3(rewardScaleUp, rewardScaleUp, rewardScaleUp), 1f)
            .setOnComplete(() =>
            {
                LeanTween.delayedCall(1.5f, () =>
                {
                    itemResult.gameObject.SetActive(false);
                });
            });
    }

    private void GetReward(BoxItem.BoxItemTypes boxItemType, string reward)
    {
        int rewardValue;
        switch (boxItemType)
        {
            case BoxItem.BoxItemTypes.LevelReward:
                rewardValue = int.Parse(reward);
                DataManager.Instance.IncreaseTransformsForCurrentCharracter(rewardValue);
                break;
            case BoxItem.BoxItemTypes.CoinReward:
                rewardValue = int.Parse(reward);
                DataManager.Instance.IncreaseGoldAmount(rewardValue);
                break;
            case BoxItem.BoxItemTypes.BeanReward:
                rewardValue = int.Parse(reward);
                DataManager.Instance.IncreaseBeanAmount(rewardValue);
                break;
        }
    }

    public bool IsShowed()
    {
        return gameObject.activeSelf;
    }
}
