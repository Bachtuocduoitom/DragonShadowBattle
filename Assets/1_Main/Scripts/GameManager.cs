using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private ChooseEnemyScreen chooseEnemyScreen;
    [SerializeField] private PreStartGameplayScreen preStartGameplayScreen;
    [SerializeField] private PlayScreen playScreen;
    [SerializeField] private BlackScreen blackScreen;
    [SerializeField] private EndScreen endScreen;

    private Vector3 enemyInitPosition = new Vector3(12f, 0, 0);
    public static GameManager Instance { get; private set; }

    public Action<State> OnStateChanged;

    public enum State
    {
        ChooseEnemy,
        PreStartGameplay,
        WaitingToStartGameplay,
        WaitingForEnemyMoveIn,
        Gameplay,
        Victory,
        Defeat
    }

    private State state;
    private float waitingForEnemyMoveInTime = 1f;


    private void Awake()
    {
        Instance = this;

       state = State.ChooseEnemy;
    }

    private void Start()
    {
        UpdateGameState(State.ChooseEnemy);
    }

    private void Update()
    {
    
    }

    public void UpdateGameState(State newState)
    {
        state = newState;

        switch (state)
        {
            case State.ChooseEnemy:
                HandleChooseEnemy();
                break;
            case State.PreStartGameplay:
                HandlePreStartGameplay();
                break;
            case State.WaitingToStartGameplay:
                HandleWaitingToStartGameplay();
                break;
            case State.WaitingForEnemyMoveIn:             
                HandleWaitingForEnemy();   
                break;
            case State.Gameplay:
                HandleGameplay();
                break;
            case State.Victory:
                HandleVictory();
                break;
            case State.Defeat:
                HandleDefeat();
                break;

        }

        OnStateChanged?.Invoke(state);
    }

    public State GetState()
    {
        return state;
    }


    private void HandleChooseEnemy()
    {
        chooseEnemyScreen.Show();

    }

    private void HandlePreStartGameplay()
    {
        chooseEnemyScreen.Hide();
        preStartGameplayScreen.Show();
    }

    private void HandleWaitingToStartGameplay()
    {
        blackScreen.Show();      
    }

    private void HandleWaitingForEnemy()
    {
        preStartGameplayScreen.Hide();
        playScreen.Show();
        blackScreen.Hide();
        LeanTween.delayedCall(waitingForEnemyMoveInTime,() =>
        {
            UpdateGameState(State.Gameplay);
        });
    }

    private void HandleGameplay()
    {
        Transform enemyTransform = Instantiate(DataManager.Instance.GetPrefabForCurrentEnemy(), enemyInitPosition, Quaternion.identity);
        Player.Instance.StartGameplay();
        ItemSpawner.Instance.StartSpawning();
        playScreen.StartGamePlay();

        AudioManager.Instance.PlayGameplayMusic();
    }

    private void HandleVictory()
    {
        playScreen.ShowEndAnnouncement(true);
        ItemSpawner.Instance.StopSpawning();
        DataManager.Instance.UpdataCurrentLevelAndPreLevelOnVictory();

        LeanTween.delayedCall(3f, () =>
        {
            playScreen.Hide();
            endScreen.ShowVictory();
        });
    }

    private void HandleDefeat()
    {
        playScreen.ShowEndAnnouncement(false);
        ItemSpawner.Instance.StopSpawning();
        DataManager.Instance.UpdateCurrentLevelAndPreLevelOnGameOver();

        // Reset data level when player lose
        DataManager.Instance.ResetDataLevel();

        LeanTween.delayedCall(3f, () =>
        {
            playScreen.Hide();
            endScreen.ShowGameOver();
        });
    }

    public bool IsGamePlay()
    {
        return state == State.Gameplay;
    }
}
