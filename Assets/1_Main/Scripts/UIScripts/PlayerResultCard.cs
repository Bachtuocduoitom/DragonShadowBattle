using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResultCard : MonoBehaviour
{


    [SerializeField] private TextMeshProUGUI levelBonusText;
    [SerializeField] private TextMeshProUGUI earnCoinText;
    [SerializeField] private TextMeshProUGUI totalScoreText;
    [SerializeField] private TextMeshProUGUI myCoinsText;
    [SerializeField] private Image highestTransform;

    private float timeToWait = 0.5f;


    public void HideResultText()
    {       
        levelBonusText.gameObject.SetActive(false);
        earnCoinText.gameObject.SetActive(false);
        totalScoreText.gameObject.SetActive(false);
        myCoinsText.gameObject.SetActive(false);
    }   

    public void ShowResultSequentially(bool isVictory)
    {
        highestTransform.sprite = DataManager.Instance.GetHighestTransform();
        highestTransform.SetNativeSize();

        HideResultText();

        LeanTween.delayedCall(timeToWait, () =>
        {
            if (gameObject == null) return;
            
            levelBonusText.gameObject.SetActive(true);
            if (isVictory)
            {
                levelBonusText.text = Util.GetCurrencyFormat(DataManager.Instance.GetLevelBonus());
            }
            else
            {
                levelBonusText.text = "0";
            }

            // Play sound
            AudioManager.Instance.PlaySFX(AudioManager.Instance.cach);
        });
        LeanTween.delayedCall(timeToWait * 2, () =>
        {
            if (gameObject == null) return;

            earnCoinText.gameObject.SetActive(true);
            earnCoinText.text = Util.GetCurrencyFormat(DataManager.Instance.GetEarnCoin());

            // Play sound
            AudioManager.Instance.PlaySFX(AudioManager.Instance.cach);
        });
        LeanTween.delayedCall(timeToWait * 3, () =>
        {
            if (gameObject == null) return;

            totalScoreText.gameObject.SetActive(true);
            if (isVictory)
            {
                totalScoreText.text = Util.GetCurrencyFormat(DataManager.Instance.GetTotalScore());
            }
            else
            {
                totalScoreText.text = Util.GetCurrencyFormat(DataManager.Instance.GetEarnCoin());
            }

            // Play sound
            AudioManager.Instance.PlaySFX(AudioManager.Instance.cach);
        });
        LeanTween.delayedCall(timeToWait * 4, () =>
        {
            if (gameObject == null) return;

            myCoinsText.gameObject.SetActive(true);
            myCoinsText.text = Util.GetCurrencyFormat(DataManager.Instance.GetGoldAmount());

            // Play sound
            AudioManager.Instance.PlaySFX(AudioManager.Instance.coinPay);
        });
    }
}
