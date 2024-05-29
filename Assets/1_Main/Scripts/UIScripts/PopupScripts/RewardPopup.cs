using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardPopup : MonoBehaviour, IScreen
{

    [SerializeField] private TextMeshProUGUI rewardText;
    [SerializeField] private Button okButton;
    [SerializeField] private BlackBackroundTouchable blackBackgroundTouchable;



    private void Start()
    {
        okButton.onClick.AddListener(() =>
        {
            Hide();

            AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        });

        blackBackgroundTouchable.OnTouchBlackBackground += () =>
        {
            Hide();
        };
    }

    public void SetRewardText(int amountOfCoin)
    {
        rewardText.text = "You got " + amountOfCoin + " coins\nThanks!";
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
