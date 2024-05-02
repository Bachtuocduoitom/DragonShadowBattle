using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FreeScreen : MonoBehaviour, IScreen
{

    [SerializeField] private Button menuButton;
    [SerializeField] private Button spinCard;
    [SerializeField] private Button likeCard;
    [SerializeField] private Button watchVideosCard;
    [SerializeField] private Button rateUsCard;
    [SerializeField] private GoldAmountTouchable goldAmountTouchable;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private SpinPopup spinPopup;
    [SerializeField] private RewardPopup rewardPopup;


    private void Awake()
    {
        if (DataManager.Instance.IsLikedPage())
        {
            likeCard.GetComponent<FreeScreenCard>().DisableCardBeforeClick();

            DataManager.Instance.SetLikedPage();
        }

        if (DataManager.Instance.IsWatchedVideo())
        {
            watchVideosCard.GetComponent<FreeScreenCard>().DisableCardBeforeClick();

            DataManager.Instance.SetWatchedVideo();
        }

        menuButton.onClick.AddListener(() =>
        {
            ScreenController.Instance.ShowScreenWithTransition(ScreenController.ScreenType.MenuScreen);

            AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        });

        goldAmountTouchable.OnTouchGoldAmount += () =>
        {
            ScreenController.Instance.ShowScreenWithTransition(ScreenController.ScreenType.CoinsScreen);
        };

        spinCard.onClick.AddListener(() =>
        {
            spinPopup.Show();

            AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        });

        likeCard.onClick.AddListener(() =>
        {
            if (!(DataManager.Instance.IsLikedPage()))
            {
                rewardPopup.Show();
                rewardPopup.SetRewardText(500);

                // Increase gold amount
                DataManager.Instance.IncreaseGoldAmount(500);

                // Update gold text
                goldText.text = Util.GetCurrencyFormat(DataManager.Instance.GetGoldAmount());

                // Disable card
                likeCard.GetComponent<FreeScreenCard>().DisableCardBeforeClick();
                DataManager.Instance.SetLikedPage();


            }

            AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        });

        watchVideosCard.onClick.AddListener(() =>
        {
            if (!(DataManager.Instance.IsWatchedVideo()))
            {
                rewardPopup.Show();
                rewardPopup.SetRewardText(200);

                // Increase gold amount
                DataManager.Instance.IncreaseGoldAmount(200);

                // Update gold text
                goldText.text = Util.GetCurrencyFormat(DataManager.Instance.GetGoldAmount());

                // Disable card
                watchVideosCard.GetComponent<FreeScreenCard>().DisableCardBeforeClick();
                DataManager.Instance.SetWatchedVideo();
            }

            AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        });

        rateUsCard.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        });
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

    public bool IsShowed()
    {
        return gameObject.activeSelf;
    }
}
