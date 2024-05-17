using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScreen : MonoBehaviour, IScreen
{

    [SerializeField] EnemyStatisticBarUI enemyStatisticBarUI;
    [SerializeField] EndAnnouncement endAnnouncement;

    private float delayDuration = 0.5f;

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

