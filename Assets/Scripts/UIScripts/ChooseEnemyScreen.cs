using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseEnemyScreen : MonoBehaviour, IScreen
{

    [SerializeField] private EnemyPopcupListUI enemyPopcupListUI;


    private void Start()
    {
        enemyPopcupListUI.OnMoveEnemyListFinished += EnemyPopcupListUI_OnMoveEnemyListFinished;
    }

    private void EnemyPopcupListUI_OnMoveEnemyListFinished()
    {
        // Told GameManager to change state to PreStartGameplay
        GameManager.Instance.UpdateGameState(GameManager.State.PreStartGameplay);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        enemyPopcupListUI.PlayMoveEnemyList();
    }

    public bool IsShowed()
    {
        return gameObject.activeSelf;
    }
}
