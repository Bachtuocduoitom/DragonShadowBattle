using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour, IScreen
{

    private const string VICTORY_TEXT = "Victory"; 
    private const string GAMEOVER_TEXT = "Game over"; 

    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button playButton;
    [SerializeField] private PlayerResultCard playerResultCard;
    [SerializeField] private ParticleSystem particleStarMoveUp;
    [SerializeField] private RatePopup ratePopup;


    private void Awake()
    {
        menuButton.onClick.AddListener(() =>
        {
            //SceneManager.LoadScene("MenuScene");
            // Reset data level
            DataManager.Instance.ResetDataLevel();

            SceneController.Instance.LoadMenuScene(ScreenController.ScreenType.MenuScreen);

            AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        });

        shopButton.onClick.AddListener(() =>
        {
            // Reset data level
            DataManager.Instance.ResetDataLevel();

            SceneController.Instance.LoadMenuScene(ScreenController.ScreenType.TransformScreen);

            AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        });

        playButton.onClick.AddListener(() =>
        {
            // Reset data level
            DataManager.Instance.ResetDataLevel();

            SceneManager.LoadScene("GamePlayScene");

            AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        });
    }

    public void Show()
    {
        gameObject.SetActive(true);

        particleStarMoveUp.Play();

        if (DataManager.Instance.CanShowRatePopup())
        {
            ratePopup.Show();
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);

        particleStarMoveUp.Stop();
    }

    public void ShowVictory()
    {
        Show();
        resultText.text = VICTORY_TEXT;

        playerResultCard.ShowResultSequentially(true);
    }

    public void ShowGameOver()
    {
        Show();
        resultText.text = GAMEOVER_TEXT;

        playerResultCard.ShowResultSequentially(false);
    }

    public bool IsShowed()
    {
        return gameObject.activeSelf;
    }
}
