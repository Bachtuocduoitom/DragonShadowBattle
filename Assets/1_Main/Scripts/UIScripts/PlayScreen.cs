using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScreen : MonoBehaviour, IScreen
{

    [SerializeField] EnemyStatisticBarUI enemyStatisticBarUI;
    [SerializeField] EndAnnouncement endAnnouncement;

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
            endAnnouncement.ShowVictory();
        }
        else
        {
            endAnnouncement.ShowGameOver();
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

