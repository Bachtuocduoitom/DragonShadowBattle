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
        UseSkill
    }

    public enum EnemySkillTypes
    {
        Skill1,
        Skill2,
        Skill3,
    }

    private Rigidbody2D rb;
    private State state;
    private UseSkillState useSkillState;

    private float waitingToMoveTimer;
    private float waitingToMoveTimerMin = 1f;
    private float waitingToMoveTimerMax = 3f;
    private float waitingToUseSkillTimer;
    private float waitingToUseSkillTimerMin = 0.5f;

    private float moveSpeed = 10f;
    private float moveInDuration = 1.5f;
    private Vector3 newPosition;
    private bool isPreUsedSkill = false;
    private EnemySkillTypes currentSkill;
    private List<Transform> enemySkillList;

    private float powerScale;

    private Vector3[] moveablePositionList = { new Vector3(7.3f, 2.5f, 0), new Vector3(4.3f, 2.5f, 0), 
        new Vector3(7.3f, 0, 0), new Vector3(4.3f, 0, 0), 
        new Vector3(7.3f, -3.7f, 0), new Vector3(4.3f, -3.7f, 0), 
        new Vector3(0, 0, 0) };

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemySkillList = enemySO.skillPrefabList;

        powerScale = enemySO.powerScale;

        state = State.Move;
        useSkillState = UseSkillState.Idle;
        
        //Move enemy into the scene
        MoveInEnemy();
    }

    private void Update()
    {
        switch (state)
        {
            case State.MoveIn:
                break;
            case State.Idle:
                if (!GameManager.Instance.IsGamePlay())
                {
                    return;
                }

                //Random time wait to move
                waitingToMoveTimer = UnityEngine.Random.Range(waitingToMoveTimerMin, waitingToMoveTimerMax);
                state = State.WaitingToMove;

                bool isUseSkill = false;
                //Check whether last Move used Skill or not
                if (isPreUsedSkill)
                {
                    //random use skillTransform or not
                    isUseSkill = (UnityEngine.Random.value > 0.45f);
                } else
                {
                    isUseSkill = true;
                }
                
                if (isUseSkill)
                {
                    waitingToUseSkillTimer = UnityEngine.Random.Range(waitingToUseSkillTimerMin, waitingToMoveTimer);
                    useSkillState = UseSkillState.WaitingToUseSkill;
                    isPreUsedSkill = true;
                } else
                {
                    isPreUsedSkill = false;
                }
                break;
            case State.WaitingToMove:
                //wait to move
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

        switch (useSkillState)
        {
            case UseSkillState.Idle:
                break;
            case UseSkillState.WaitingToUseSkill:
                //wait to use skillTransform
                waitingToUseSkillTimer -= Time.deltaTime;
                if (waitingToUseSkillTimer < 0f)
                {
                    useSkillState = UseSkillState.UseSkill;
                }
                break;
            case UseSkillState.UseSkill:
                UseRandomSkill();
                useSkillState = UseSkillState.Idle;
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
        Vector3 target = new Vector3(UnityEngine.Random.Range(6f, 9f), UnityEngine.Random.Range(-3.7f, 2.5f), 0f);
        Vector3 startPosition = target + new Vector3(15f, 0f, 0f);

        LeanTween.move(gameObject, target, moveInDuration)
            .setEaseOutBack()
            .setFrom(startPosition)
            .setOnComplete(() => {
                waitingToMoveTimer = 1f;
                state = State.WaitingToMove;
            });
    }

    public void useSkill()
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

            // Play explosion effect
            ExplosionSpawner.Instance.PlayPlayerSkillExposion(playerSkill.GetSizeType(), playerSkill.GetPosition());
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
