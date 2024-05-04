using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyPopcupListUI : MonoBehaviour
{

    public Action OnMoveEnemyListFinished;

    private const float Y_OFFSET_BETWEEN_ENEMY = 630f;

    [SerializeField] private EnemyPopcupUI enemyPopcupUIPrefab;

    private List<EnemyPopcupUI> enemyPopcupUIList = new List<EnemyPopcupUI>();

    private int currentEnemy;
    private int targetEnemy;
    private Vector3 targetPosition;

    private float speed;
    private const float speedDown = 900f;
    private const float speedUp = 500f;

    private float waitingToMoveTimer = 1f;
    private bool startMoveEnemyList = false;
    private const float delayTime = 0.5f;

    private bool playSound = true;

    private void Start()
    {
        for (int i = 1; i < DataManager.Instance.GetNumberOfLevel() + 1; i++)
        {
            EnemyPopcupUI enemyPopcupUI = Instantiate(enemyPopcupUIPrefab, transform);
            enemyPopcupUI.SetEnemyImages(DataManager.Instance.GetLevelEnemySpriteList(i));
            enemyPopcupUI.SetNumberText(i);
            enemyPopcupUI.transform.localPosition = new Vector3(0, i * Y_OFFSET_BETWEEN_ENEMY, 0);
            enemyPopcupUIList.Add(enemyPopcupUI);
        }

        currentEnemy = DataManager.Instance.GetPreLevel();
        targetEnemy = DataManager.Instance.GetCurrentLevel();

        if (currentEnemy > targetEnemy)
        {
            speed = speedDown;
        }
        else
        {
            speed = speedUp;
        }

        // Set target position and current position of enemy list
        targetPosition = transform.localPosition + Vector3.down * targetEnemy * Y_OFFSET_BETWEEN_ENEMY;
        transform.localPosition = transform.localPosition + Vector3.down * currentEnemy * Y_OFFSET_BETWEEN_ENEMY;

    }

    private void Update()
    {
        if (!startMoveEnemyList) return;

        if (waitingToMoveTimer > 0)
        {
            waitingToMoveTimer -= Time.deltaTime;
            
        } else if (transform.localPosition != targetPosition)
        {
            //Move to target position
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, speed * Time.deltaTime);
            if (transform.localPosition == targetPosition)
            {
                LeanTween.delayedCall(delayTime, () =>
                {
                    OnMoveEnemyListFinished?.Invoke();
                });
            }

            // Play sound
            if (playSound)
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.cach);
                playSound = false;
            }
        }
        
    }

    public void PlayMoveEnemyList()
    {
        startMoveEnemyList = true;
    }
    
}
