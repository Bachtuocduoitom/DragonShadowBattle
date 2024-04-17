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
    private List<EnemySO> enemySOList;

    private int currentEnemy;
    private int targetEnemy;
    private Vector3 targetPosition;

    private float speed = 500f;

    private float waitingToMoveTimer = 1f;
    private bool startMoveEnemyList = false;
    private float delayTime = 0.5f;

    private void Start()
    {
        enemySOList = DataManager.Instance.GetEnemySOList();
        
        for (int i = 1; i < enemySOList.Count + 1; i++)
        {
            EnemyPopcupUI enemyPopcupUI = Instantiate(enemyPopcupUIPrefab, transform);
            enemyPopcupUI.SetEnemyImage(enemySOList[i-1].sprite);
            enemyPopcupUI.SetNumberText(i);
            enemyPopcupUI.transform.localPosition = new Vector3(0, i * Y_OFFSET_BETWEEN_ENEMY, 0);
            enemyPopcupUIList.Add(enemyPopcupUI);
        }

        currentEnemy = DataManager.Instance.GetPreEnemy();
        targetEnemy = DataManager.Instance.GetCurrentEnemy();

        transform.localPosition = transform.localPosition + Vector3.down * currentEnemy * Y_OFFSET_BETWEEN_ENEMY;
        targetPosition = transform.localPosition + Vector3.down * targetEnemy * Y_OFFSET_BETWEEN_ENEMY;

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
        }
        
    }

    public void PlayMoveEnemyList()
    {
        startMoveEnemyList = true;
    }
    
}
