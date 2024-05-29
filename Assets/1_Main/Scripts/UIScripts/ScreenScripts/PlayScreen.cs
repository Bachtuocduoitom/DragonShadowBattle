using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayScreen : MonoBehaviour, IScreen
{

    [SerializeField] EnemyStatisticBarUI enemyStatisticBarUI;
    [SerializeField] EndAnnouncement endAnnouncement;
    [SerializeField] Button pauseButton;
    [SerializeField] PausePopup pausePopup;

    private float delayDuration = 0.5f;

    private void Awake()
    {
        pauseButton.onClick.AddListener(() => {
            Time.timeScale = 0;
            pausePopup.Show();
            GameManager.Instance.UpdateGameState(GameManager.State.Pause);

            AudioManager.Instance.PauseSFX();
        });

        pausePopup.OnResume += () =>
        {
            Time.timeScale = 1;
            GameManager.Instance.UpdateGameState(GameManager.State.Gameplay);

            AudioManager.Instance.UnPauseSFX();
            AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        };

        pausePopup.OnBackToMenu += () =>
        {
            
            DOTween.To(() => 0, x => { }, 0, 0.4f)
            .OnComplete(() =>
            {
                Time.timeScale = 1;
            })
            .SetUpdate(true);
            
            SceneController.Instance.LoadMenuScene(ScreenController.ScreenType.MenuScreen);

            AudioManager.Instance.UnPauseSFX();
            AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        };
    }

    private void Start()
    {
        Hide();
    }

    public void StartGamePlay()
    {
        enemyStatisticBarUI.SetEnemy();
    }

    public void OnAnEnemyDie()
    {
        enemyStatisticBarUI.RemoveEnemy();
    }

    public void ShowEndAnnouncement(bool isVictory)
    {
        if (isVictory)
        {
            DOTween.To(() => 0, x => { }, 0, delayDuration)
            .OnComplete(() => endAnnouncement.ShowVictory());
        }
        else
        {
            DOTween.To(() => 0, x => { }, 0, delayDuration)
            .OnComplete(() => endAnnouncement.ShowGameOver());
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public bool IsShowed()
    {
        return gameObject.activeSelf;
    }
}

