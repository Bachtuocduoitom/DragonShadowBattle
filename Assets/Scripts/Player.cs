﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{

    //Create Instance for Player
    public static Player Instance { get; private set; }

    public Action<State, PlayerSkill> OnChangeState;
    public Action<int, float, float> OnChangeTransform;
    public Action<int> OnFinishChangeTransform;
    public Action<float> OnHitDamage;
    public Action<float> OnTryUseSkill;
    public Action OnUseBeanItem;
    public Action<ItemSpawner.ItemType> OnCollectItem;

    public enum State
    {
        Idle,
        UseNormalSkill,
        UseGatherSkill,
        Transform,
        Die
    }

    public enum PlayerSkill
    {
        None,
        KamehaSkill,
        SpamSKill,
        DonSkill,
        SpiritBoomSkill,
        DragonSkill,
    }

    private Rigidbody2D rb;
    private bool isMoving = false;
    private Vector3 touchPosition;
    private Vector2 direction;
    private Vector3 lastPosition;
    private float moveSpeed = 20f;

    private State state;
    private int numberOfTurnPerSpamSkill = 5;
    private float delayPerSpawnSkillTurn = 0.01f;

    private int currentSkin = 0;
    private PlayerSkill currentSkill = PlayerSkill.None;

    private bool hasArmor = false;
    private PlayerArmor currentArmor;

    private GameObject enemy;
    private const string TAG_ENEMY = "Enemy";

    
    [SerializeField] private Transform skill1Prefab;
    [SerializeField] private Transform skillSpamPrefab;
    [SerializeField] private Transform skillKamehaPrefab;
    [SerializeField] private Transform skillSpiritBoomPrefab;
    [SerializeField] private Transform skillDragonPrefab;
    [SerializeField] private Transform skillSpawnPosition;
    [SerializeField] private Transform playerArmor;
    [SerializeField] private Transform spamSkillCurvePosition1;
    [SerializeField] private Transform spamSkillCurvePosition2;
    [SerializeField] private Transform spamSkillCurvePosition3;
    [SerializeField] private Transform spamSkillCurvePosition4;
    [SerializeField] private Transform spamSkillCurvePosition5;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    private void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        state = State.Idle;
    }

    private void Update()
    {
        HandleMovement();
        switch (state)
        {
            case State.Idle:
                break;
            case State.UseNormalSkill:
                break;
            case State.UseGatherSkill:
                break;
            case State.Transform:
                break;
            case State.Die:
                break;
        }
    }

    private void HandleMovement()
    {
        // Can't move when transform or use gather skill
        if (state == State.UseGatherSkill) return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            

            if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId) && touch.phase == TouchPhase.Moved)
            {
                isMoving = true;

                touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0;
                if (touchPosition.x > 4.5f) return;
                lastPosition = new Vector3(touchPosition.x, touchPosition.y, 0);
            }
        }

    }

    private void FixedUpdate()
    {
        if (!isMoving) return;

        Vector2 currentPosition = rb.position;
        Vector2 newPosition = Vector2.MoveTowards(currentPosition, lastPosition, moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out EnemySkill enemySkill))
        {
            if (hasArmor) return;

            enemySkill.HitPlayer();
            Debug.Log("Hit player");

            Damage(enemySkill.GetDamage());
        } else if (collider.TryGetComponent(out Item item))
        {
            ItemSpawner.ItemType itemType = item.GetItemType();
            switch (itemType)
            {
                case ItemSpawner.ItemType.GreenBean:
                    OnCollectItem?.Invoke(itemType);
                    break;
                case ItemSpawner.ItemType.RedBean:
                    OnCollectItem?.Invoke(itemType);
                    break;
                case ItemSpawner.ItemType.BlueBean:
                    OnCollectItem?.Invoke(itemType);
                    break;
                case ItemSpawner.ItemType.Armor:
                    SpawnArmor();
                    break;
                case ItemSpawner.ItemType.Gold:
                    DataManager.Instance.IncreaseGoldAmount(200);
                    DataManager.Instance.AddEarnCoin();
                    break;
            }
            item.HitPlayer();

        }
        
    }

    
    // Player spawn skill
    public void SpawnSpamSkill()
    {
        // Gọi hàm SpawnBulletsWithDelay với một khoảng thời gian delay giữa mỗi viên đạn
        StartCoroutine(SpawnBulletsWithDelay(0.01f));
    }
    IEnumerator SpawnBulletsWithDelay(float delay)
    {
        for (int i = 0; i < numberOfTurnPerSpamSkill; i++)
        {
            Transform skillSpamTransform = Instantiate(skillSpamPrefab, skillSpawnPosition.position, Quaternion.identity);
            GokuSpamSkill skillSpam = skillSpamTransform.GetComponent<GokuSpamSkill>();
            skillSpam.ScaleDamageDependOnEnemyScalePower(DataManager.Instance.GetPowerScaleForCurrentTransform(currentSkin));

            switch (i)
            {
                case 0:
                    skillSpam.SetUpCurvePosition(spamSkillCurvePosition1.position);
                    break;
                case 1:
                    skillSpam.SetUpCurvePosition(spamSkillCurvePosition5.position);
                    break;
                case 2:
                    skillSpam.SetUpCurvePosition(spamSkillCurvePosition2.position);
                    break;
                case 3:
                    skillSpam.SetUpCurvePosition(spamSkillCurvePosition4.position);
                    break;
                case 4:
                    skillSpam.SetUpCurvePosition(spamSkillCurvePosition3.position);
                    break;
            }

            yield return new WaitForSeconds(delay);
        }
    }

    public void SpawnKamehaSkill()
    {
        // Get Enemy position
        Vector3 enemyPosition = enemy.transform.position;

        // Spawn Kameha
        Transform skillTransform = Instantiate(skillKamehaPrefab, skillSpawnPosition.position, Quaternion.identity);
        GokuKamehaSkill kamehaSkill = skillTransform.GetComponent<GokuKamehaSkill>();
        kamehaSkill.SetDirection(enemyPosition);
        kamehaSkill.ScaleDamageDependOnEnemyScalePower(DataManager.Instance.GetPowerScaleForCurrentTransform(currentSkin));
    }
    
    public void SpawnDonSkill()
    {
        Transform skill1 = Instantiate(skill1Prefab, skillSpawnPosition.position, Quaternion.identity);
        GokuSkill1 donSkill = skill1.GetComponent<GokuSkill1>();
        donSkill.ScaleDamageDependOnEnemyScalePower(DataManager.Instance.GetPowerScaleForCurrentTransform(currentSkin));
    }

    public void SpawnSpiritBoomSkill()
    {
        // Get Enemy position
        Vector3 enemyPosition = enemy.transform.position;

        // Spawn SpiritBoom
        Transform skillTransform = Instantiate(skillSpiritBoomPrefab, skillSpawnPosition.position, Quaternion.identity);
        GokuSpiritBoomSkill spiritBoomSkill = skillTransform.GetComponent<GokuSpiritBoomSkill>();
        spiritBoomSkill.SetDirection(enemyPosition);
        spiritBoomSkill.ScaleDamageDependOnEnemyScalePower(DataManager.Instance.GetPowerScaleForCurrentTransform(currentSkin));
    }

    public void SpawnDragonSkill()
    {
        Transform skillTransform = Instantiate(skillDragonPrefab, skillSpawnPosition.position, Quaternion.identity);
        GokuDragonSkill dragonSkill = skillTransform.GetComponent<GokuDragonSkill>();
        dragonSkill.ScaleDamageDependOnEnemyScalePower(DataManager.Instance.GetPowerScaleForCurrentTransform(currentSkin));
    }

    private void SpawnArmor()
    {
        Transform armorTransform = Instantiate(playerArmor, skillSpawnPosition.position, Quaternion.identity);
        armorTransform.SetParent(transform);
        if (hasArmor)
        {
            currentArmor.SelfDestroy();
        } else
        {
            hasArmor = true;
        }
        currentArmor = armorTransform.GetComponent<PlayerArmor>();

    }




    // Handle UI Button Click
    public void TryUseSkill(PlayerSkill skill)
    {
        if (!GameManager.Instance.IsGamePlay() || state == State.UseGatherSkill || state == State.Transform) return;

        currentSkill = skill;

        float manaCost = DataManager.Instance.GetManaCostForSkill(currentSkill);
        OnTryUseSkill?.Invoke(manaCost);
    }

    public void UseBeanItem()
    {
        OnUseBeanItem?.Invoke();
    }



    // Fire event to update Visual
    public void UseSkill()
    {
        switch (currentSkill)
        {
            case PlayerSkill.KamehaSkill:
                UseKamehaSkill();
                break;
            case PlayerSkill.SpamSKill:
                UseSpamSkill();
                break;
            case PlayerSkill.DonSkill:
                UseDonSkill();
                break;
            case PlayerSkill.SpiritBoomSkill:
                UseSpiritBoomSkill();
                break;
            case PlayerSkill.DragonSkill:
                UseDragonSkill();
                break;
        }
    }
    private void UseSpamSkill()
    {
        OnChangeState?.Invoke(State.UseNormalSkill, PlayerSkill.SpamSKill);
        state = State.UseNormalSkill;
    }

    private void UseKamehaSkill()
    {
        OnChangeState?.Invoke(State.UseGatherSkill, PlayerSkill.KamehaSkill);
        state = State.UseGatherSkill;
    }

    private void UseDonSkill()
    {
        OnChangeState?.Invoke(State.UseNormalSkill, PlayerSkill.DonSkill);
        state = State.UseNormalSkill;
    }

    private void UseSpiritBoomSkill()
    {
        OnChangeState?.Invoke(State.UseGatherSkill, PlayerSkill.SpiritBoomSkill);
        state = State.UseGatherSkill;
    }  
    
    private void UseDragonSkill()
    {
        OnChangeState?.Invoke(State.UseNormalSkill, PlayerSkill.DragonSkill);
        state = State.UseNormalSkill;
    }

    public void TransformNextLevel()
    {
        if (state == State.UseGatherSkill || state == State.Transform) return;

        currentSkin++;
        DataManager.Instance.SetHighestTransform(currentSkin);
        DataManager.Instance.SetLevelBonus(currentSkin);

        // Fire event to update UI
        float newMaxHealth = DataManager.Instance.GetMaxHealthForCurrentTransform(currentSkin);
        float newMaxMana = DataManager.Instance.GetMaxManaForCurrentTransform(currentSkin);
        OnChangeTransform?.Invoke(currentSkin, newMaxHealth, newMaxMana);

        // Fire event to update Visual
        OnChangeState?.Invoke(State.Transform, PlayerSkill.None);
        state = State.Transform;

        // Spawn Armor    
        Transform armorTransform = Instantiate(playerArmor, skillSpawnPosition.position, Quaternion.identity);
        armorTransform.SetParent(transform);
        armorTransform.GetComponent<PlayerArmor>().SetTimeToDisappear(1f);
    }

    

    public void BackToIdleState()
    {
        OnChangeState?.Invoke(State.Idle, PlayerSkill.None);
        state = State.Idle;
    }

    public void FinishTransform()
    {
        OnFinishChangeTransform?.Invoke(currentSkin);
    }

    public void OutOfArmor()
    {
        hasArmor = false;
        currentArmor = null;
    }

    public int GetCurrentSkin()
    {
        return currentSkin;
    }

    public void StartGameplay()
    {
        //get enemy
        enemy = GameObject.FindWithTag(TAG_ENEMY);
    }

    private void Damage(float amount)
    {
        OnHitDamage?.Invoke(amount);
    }

    public void Die()
    {
        state = State.Die;
        GameManager.Instance.UpdateGameState(GameManager.State.Defeat);
        gameObject.SetActive(false);
    }
}