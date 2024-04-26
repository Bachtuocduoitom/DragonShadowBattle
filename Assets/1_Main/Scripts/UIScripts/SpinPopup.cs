using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpinPopup : MonoBehaviour, IScreen
{

    [SerializeField] private SpinWheel spinWheel;
    [SerializeField] private Image pointer;
    [SerializeField] private Button spinButton;
    [SerializeField] private BlackBackroundTouchable blackBackroundTouchable;
    [SerializeField] private RectTransform itemResult;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemDescribe;

    private bool canSpin = true;
    private bool onSpin = false;

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

            LeanTween.rotateAround(pointer.rectTransform, Vector3.forward, -20f, 0.2f)
            .setLoopPingPong(2)
            .setOnComplete(() =>
            {
                LeanTween.rotateAround(pointer.rectTransform, Vector3.forward, -30f, 0.1f)
                .setLoopPingPong(14)
                .setOnComplete(() =>
                {
                    LeanTween.rotateAround(pointer.rectTransform, Vector3.forward, -20f, 0.3f)
                    .setLoopPingPong(1)
                    .setOnComplete(() =>
                    {
                        LeanTween.rotateAround(pointer.rectTransform, Vector3.forward, -10f, 0.3f)
                        .setOnComplete(() =>
                        {
                            LeanTween.rotateAround(pointer.rectTransform, Vector3.forward, 10f, 0.4f)
                            .setEaseOutCubic();
                    });

                    });
                });
            });


            AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        });

        blackBackroundTouchable.OnTouchBlackBackground += () =>
        {
            if (onSpin) return;

            Hide();
        };

        spinWheel.OnSpinWheelFinished += (itemSprite, describe) =>
        {
            spinButton.GetComponent<SpinButton>().SetAdsSpin();
            LeanTween.delayedCall(0.5f, () =>
            {
                ShowItemResult(itemSprite, describe);
            });

            onSpin = false;
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

        LeanTween.scale(itemResult, new Vector3(1.5f, 1.5f, 1.5f), 2f)
            .setOnComplete(() =>
            {
                LeanTween.delayedCall(0.2f, () =>
                {
                    itemResult.gameObject.SetActive(false);
                });
            });

        
    }

    public bool IsShowed()
    {
        return gameObject.activeSelf;
    }
}
