using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Action<EnemySkillTypes> OnUseSkill;
    public Action<float> OnHitDamage;

    [SerializeField] private Transform skillSpawnPos;
    [SerializeField] private EnemySO enemySO;

    public enum State
    {
        MoveIn,
        Idle,
        WaitingToMove,
        Move,
        Die
    }
    
    public enum UseSkillState
    {
        Idle,
        WaitingToUseSkill,
        UseSkill,
        UsingSkill
    }

    public enum EnemySkillTypes
    {
        Skill1,
        Skill2,
        Skill3
    }

    private Rigidbody2D rb;
    private State state;
    private UseSkillState useSkillState;

    private float waitingToMoveTimer;
    private readonly float waitingToMoveTimerMin = 1f;
    private readonly float waitingToMoveTimerMaxWhenDontUseSkill = 1.5f;
    private readonly float waitingToMoveTimerMax = 3f;
    private float waitingToUseSkillTimer;
    private readonly float waitingToUseSkillTimerMin = 0.2f;

    private readonly float moveSpeed = 10f;
    private readonly float moveInDuration = 1.5f;
    private Vector3 newPosition;
    private bool isPreUsedSkill = false;
    private EnemySkillTypes currentSkill;
    private List<Transform> enemySkillList;

    private float powerScale;

    private Vector3[] moveablePositionList;

    private float halfScreenWitdh;
    private float halfScreenHeight;
    private float behindPositionX;
    private float topPositionY;
    private float midPositionX;
    private float botPositionY;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemySkillList = enemySO.skillPrefabList;
        powerScale = enemySO.powerScale;

        // Init State
        state = State.MoveIn;
        useSkillState = UseSkillState.Idle;

        halfScreenWitdh = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        halfScreenHeight = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;

        behindPositionX = halfScreenWitdh * 10 / 13;
        midPositionX = halfScreenWitdh * 1 / 2;
        topPositionY = halfScreenHeight * 5 / 9;
        botPositionY = - halfScreenHeight * 7 / 10;

        moveablePositionList = new Vector3[] { new Vector3(behindPositionX, topPositionY, 0), new Vector3(midPositionX, topPositionY, 0),
        new Vector3(behindPositionX, 0, 0), new Vector3(midPositionX, 0, 0),
        new Vector3(behindPositionX, botPositionY, 0), new Vector3(midPositionX, botPositionY, 0),
        new Vector3(0, 0, 0) };

        //Move enemy into the scene
        MoveInEnemy();
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGamePlay())
        {
            return;
        }

        // Handle Enemy State
        switch (state)
        {
            case State.MoveIn:
                break;
            case State.Idle:    
                state = State.WaitingToMove;

                // Check whether last Move used Skill or not
                bool isUseSkill = false;
                if (isPreUsedSkill)
                {
                    // Random use skillTransform or not
                    isUseSkill = (UnityEngine.Random.value > 0.45f);
                } else
                {
                    isUseSkill = true;
                }
                
                if (isUseSkill)
                {
                    if (useSkillState == UseSkillState.Idle)
                    {
                        // Random time wait to move
                        waitingToMoveTimer = UnityEngine.Random.Range(waitingToMoveTimerMin, waitingToMoveTimerMax);

                        // Random time wait to use skillTransform
                        waitingToUseSkillTimer = UnityEngine.Random.Range(waitingToUseSkillTimerMin, waitingToMoveTimer);

                        useSkillState = UseSkillState.WaitingToUseSkill;
                        isPreUsedSkill = true;
                    } else
                    {
                        // Random time wait to move
                        waitingToMoveTimer = UnityEngine.Random.Range(waitingToMoveTimerMin, waitingToMoveTimerMaxWhenDontUseSkill);
                        
                        isPreUsedSkill = false;
                    }
                    
                } else
                {
                    // Random time wait to move
                    waitingToMoveTimer = UnityEngine.Random.Range(waitingToMoveTimerMin, waitingToMoveTimerMaxWhenDontUseSkill);

                    isPreUsedSkill = false;
                }
                break;
            case State.WaitingToMove:
                // Wait to move
                waitingToMoveTimer -= Time.deltaTime;
                if (waitingToMoveTimer < 0f)
                {
                    int randomNumber = UnityEngine.Random.Range(0, moveablePositionList.Length);
                    newPosition = moveablePositionList[randomNumber];
                    state = State.Move;
                }
                break;
            case State.Move:
                MoveToRandomPostion();
                break;
            case State.Die:
                break;
        }

        // Handle Use Skill State
        switch (useSkillState)
        {
            case UseSkillState.Idle:
                break;
            case UseSkillState.WaitingToUseSkill:
                // Wait to use skillTransform
                waitingToUseSkillTimer -= Time.deltaTime;
                if (waitingToUseSkillTimer < 0f)
                {
                    useSkillState = UseSkillState.UseSkill;
                }
                break;
            case UseSkillState.UseSkill:
                UseRandomSkill();
                useSkillState = UseSkillState.UsingSkill;
                break;
            case UseSkillState.UsingSkill:
                break;
        }
    }

    private void MoveToRandomPostion()
    {
        transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);

        // Check wheter Enemy reached New Position
        if (transform.position == newPosition)
        {
            state = State.Idle;
        }
    }

    private void UseRandomSkill()
    {
        int randomSkillIndex = UnityEngine.Random.Range(0, 3); 
        currentSkill = (EnemySkillTypes)randomSkillIndex;

        OnUseSkill?.Invoke(currentSkill);
    }

    private void MoveInEnemy()
    {
        Vector3 target = new Vector3(UnityEngine.Random.Range(midPositionX, behindPositionX), UnityEngine.Random.Range(botPositionY, topPositionY), 0f);
        Vector3 startPosition = target + new Vector3(15f, 0f, 0f);

        LeanTween.move(gameObject, target, moveInDuration)
            .setEaseOutBack()
            .setFrom(startPosition)
            .setOnComplete(() => {
                waitingToMoveTimer = 1f;
                state = State.WaitingToMove;
            });
    }

    public void UseSkill()
    {
        Transform skillTransform;
        EnemySkill enemySkill;
        switch (currentSkill)
        {
            case EnemySkillTypes.Skill1:
                skillTransform = Instantiate(enemySkillList[0], skillSpawnPos.position, Quaternion.identity);
                enemySkill = skillTransform.GetComponent<EnemySkill>();
                enemySkill.ScaleDamageDependOnEnemyScalePower(powerScale);
                break;
            case EnemySkillTypes.Skill2:
                skillTransform = Instantiate(enemySkillList[1], skillSpawnPos.position, Quaternion.identity);
                enemySkill = skillTransform.GetComponent<EnemySkill>();
                enemySkill.ScaleDamageDependOnEnemyScalePower(powerScale);
                break;
            case EnemySkillTypes.Skill3:
                skillTransform = Instantiate(enemySkillList[2], skillSpawnPos.position, Quaternion.identity);
                enemySkill = skillTransform.GetComponent<EnemySkill>();
                enemySkill.ScaleDamageDependOnEnemyScalePower(powerScale);
                break;
        }

        // Play sound
        AudioManager.Instance.PlaySFX(AudioManager.Instance.skillFly);
    }

    public void CompleteUsingSkill()
    {
        useSkillState = UseSkillState.Idle;
    }

    public EnemySO GetEnemySO()
    {
        return enemySO;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerSkillBase playerSkill = collider.GetComponent<PlayerSkillBase>();
        if (playerSkill != null)
        {
            playerSkill.HitEnemy();
            Damage(playerSkill.GetDamage());

            // Play sound
            AudioManager.Instance.PlayEnemyHit();
        }
    }

    private void Damage(float amount)
    {
        OnHitDamage?.Invoke(amount);
    }

    public void Die()
    {
        state = State.Die;
        GameManager.Instance.HandleOnEnemyDie();

        // Play explosion
        ExplosionSpawner.Instance.PlayEnemyExplosion(transform.position);

        // Play sound
        AudioManager.Instance.PlaySFX(AudioManager.Instance.enemyDieDie);

        Destroy(gameObject);
    }

}
