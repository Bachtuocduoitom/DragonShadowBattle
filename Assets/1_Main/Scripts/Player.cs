using System;
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
    public Action<PlayerSkill, float> OnTryUseSkill;
    public Action OnUseBeanItem;
    public Action<ItemSpawner.ItemType> OnCollectItem;
    public Action OnHitGold;

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
    private float borderX;
    private float spawnPositionX;

    private int currentSkin = 0;
    private PlayerSkill currentSkill = PlayerSkill.None;

    private bool hasArmor = false;
    private PlayerArmor currentArmor;

    private GameObject enemy;
    private const string TAG_ENEMY = "Enemy";

    // Player skill prefab
    private Transform skill1Prefab;
    private Transform skillSpamPrefab;
    private Transform skillKamehaPrefab;
    private Transform skillSpiritBoomPrefab;
    private Transform skillDragonPrefab;


    [SerializeField] private Transform skillSpawnPosition;
    [SerializeField] private Transform kamehaChargeSkillPosition;
    [SerializeField] private Transform spiritBoomChargeSkillPosition;
    [SerializeField] private Transform spiritBoomSpawnPosition;
    [SerializeField] private Transform playerArmor;
    [SerializeField] private Transform spamSkillCurvePosition1;
    [SerializeField] private Transform spamSkillCurvePosition2;
    [SerializeField] private Transform spamSkillCurvePosition3;
    [SerializeField] private Transform spamSkillCurvePosition4;
    [SerializeField] private Transform spamSkillCurvePosition5;

    [SerializeField] private ParticleSystem buff_01;
    [SerializeField] private ParticleSystem chargeSkillFx;
    [SerializeField] private ParticleSystem energyBallSpiritBoom;
    [SerializeField] private Transform energyBallKameha;

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

        // Assign Player skill prefab
        CharacterSkillsSO characterSkillsSO = DataManager.Instance.GetCurrentCharacterSkillsPrefabs();
        skill1Prefab = characterSkillsSO.skillDonPrefab;
        skillSpamPrefab = characterSkillsSO.skillSpamPrefab;
        skillKamehaPrefab = characterSkillsSO.skillKamehaPrefab;
        skillSpiritBoomPrefab = characterSkillsSO.skillSpiritBoomPrefab;
        skillDragonPrefab = characterSkillsSO.skillDragonPrefab;

        // Get border X
        borderX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x * 5 / 12;
        spawnPositionX = - Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x * 1 / 2;

        transform.position = new Vector3(spawnPositionX, transform.position.y, 0);
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
                if (touchPosition.x > borderX) return;
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

    #region Collision
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!GameManager.Instance.IsGamePlay()) return;

        if (state == State.UseGatherSkill && currentSkill == PlayerSkill.SpiritBoomSkill) return;
       
        if (collider.TryGetComponent(out EnemySkill enemySkill))
        {
            // Check if player has armor
            if (hasArmor) return;

            enemySkill.HitPlayer();
            Damage(enemySkill.GetDamage());

            // Play sound
            AudioManager.Instance.PlayPlayerHit();

        } else if (collider.TryGetComponent(out ItemBase item))
        {
            ItemSpawner.ItemType itemType = item.GetItemType();
            switch (itemType)
            {
                case ItemSpawner.ItemType.GreenBean:
                    OnCollectItem?.Invoke(itemType);

                    // Play sound
                    AudioManager.Instance.PlaySFX(AudioManager.Instance.item);

                    // Play particle effect
                    buff_01.gameObject.SetActive(true);
                    LeanTween.delayedCall(1.5f, () =>
                    {
                        buff_01.gameObject.SetActive(false);
                    });
                    break;
                case ItemSpawner.ItemType.RedBean:
                    OnCollectItem?.Invoke(itemType);

                    // Play sound
                    AudioManager.Instance.PlaySFX(AudioManager.Instance.item);

                    // Play particle effect
                    buff_01.gameObject.SetActive(true);
                    LeanTween.delayedCall(1.5f, () =>
                    {
                        buff_01.gameObject.SetActive(false);
                    });
                    break;
                case ItemSpawner.ItemType.BlueBean:
                    OnCollectItem?.Invoke(itemType);

                    // Play sound
                    AudioManager.Instance.PlaySFX(AudioManager.Instance.item);

                    // Play particle effect
                    buff_01.gameObject.SetActive(true);
                    LeanTween.delayedCall(1.5f, () =>
                    {
                        buff_01.gameObject.SetActive(false);
                    });
                    break;
                case ItemSpawner.ItemType.Armor:
                    SpawnArmor();

                    // Play sound
                    AudioManager.Instance.PlaySFX(AudioManager.Instance.item);

                    // Play particle effect
                    buff_01.gameObject.SetActive(true);
                    LeanTween.delayedCall(1.5f, () =>
                    {
                        buff_01.gameObject.SetActive(false);
                    });
                    break;
                case ItemSpawner.ItemType.Gold:
                    DataManager.Instance.IncreaseGoldAmount(200);
                    DataManager.Instance.AddEarnCoin();
                    OnHitGold?.Invoke();

                    // Play sound
                    AudioManager.Instance.PlaySFX(AudioManager.Instance.coinEarn);
                    break;
            }
            item.HitPlayer();
        }
    }
    #endregion

    #region Spawn Skill
    public void SpawnSpamSkill()
    {
        // Gọi hàm SpawnBulletsWithDelay với một khoảng thời gian delay giữa mỗi viên đạn
        StartCoroutine(SpawnBulletsWithDelay(0.001f));

        // Play sound
        AudioManager.Instance.PlaySFX(AudioManager.Instance.playerSpam);
    }
    IEnumerator SpawnBulletsWithDelay(float delay)
    {
        for (int i = 0; i < numberOfTurnPerSpamSkill; i++)
        {
            Transform skillSpamTransform = Instantiate(skillSpamPrefab, skillSpawnPosition.position, Quaternion.identity);
            GokuSpamSkill skillSpam = skillSpamTransform.GetComponent<GokuSpamSkill>();
            skillSpam.ScaleDamageDependOnEnemyScalePower(DataManager.Instance.GetPowerScaleForCurrentTransform(currentSkin));
            Vector3 pointBPosition;
            switch (i)
            {
                case 1:
                    pointBPosition = spamSkillCurvePosition1.position + Vector3.up * UnityEngine.Random.Range(-4, 1);
                    skillSpam.SetUpCurvePosition(pointBPosition);
                    break;
                case 3:
                    pointBPosition = spamSkillCurvePosition2.position + Vector3.up * UnityEngine.Random.Range(-3, 0);
                    skillSpam.SetUpCurvePosition(pointBPosition);
                    break;
                case 0:
                    skillSpam.SetUpCurvePosition(spamSkillCurvePosition3.position);
                    break;
                case 4:
                    pointBPosition = spamSkillCurvePosition5.position + Vector3.up * UnityEngine.Random.Range(0, 3);
                    skillSpam.SetUpCurvePosition(pointBPosition);
                    break;
                case 2:
                    pointBPosition = spamSkillCurvePosition5.position + Vector3.up * UnityEngine.Random.Range(-1, 4);
                    skillSpam.SetUpCurvePosition(pointBPosition);
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

        // Play sound
        AudioManager.Instance.PlaySFX(AudioManager.Instance.playerDon);
    }

    public void SpawnSpiritBoomSkill()
    {
        // Get Enemy position
        Vector3 enemyPosition = enemy.transform.position;

        // Spawn SpiritBoom
        Transform skillTransform = Instantiate(skillSpiritBoomPrefab, spiritBoomSpawnPosition.position, Quaternion.identity);
        GokuSpiritBoomSkill spiritBoomSkill = skillTransform.GetComponent<GokuSpiritBoomSkill>();
        spiritBoomSkill.SetDirection(enemyPosition);
        spiritBoomSkill.ScaleDamageDependOnEnemyScalePower(DataManager.Instance.GetPowerScaleForCurrentTransform(currentSkin));

        
    }

    public void SpawnDragonSkill()
    {
        Transform skillTransform = Instantiate(skillDragonPrefab, skillSpawnPosition.position, Quaternion.identity);
        GokuDragonSkill dragonSkill = skillTransform.GetComponent<GokuDragonSkill>();
        dragonSkill.ScaleDamageDependOnEnemyScalePower(DataManager.Instance.GetPowerScaleForCurrentTransform(currentSkin));

        // Play sound
        AudioManager.Instance.PlaySFX(AudioManager.Instance.playerDragon);
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
    #endregion


    // Handle UI Button Click
    public void TryUseSkill(PlayerSkill skill)
    {
        if (!GameManager.Instance.IsGamePlay() || state == State.UseGatherSkill || state == State.Transform) return;

        currentSkill = skill;

        float manaCost = DataManager.Instance.GetManaCostForSkill(currentSkill);
        OnTryUseSkill?.Invoke(currentSkill, manaCost);
    }

    public void UseBeanItem()
    {
        OnUseBeanItem?.Invoke();

        // Play particle effect
        buff_01.gameObject.SetActive(true);
        LeanTween.delayedCall(1.5f, () =>
        {
            buff_01.gameObject.SetActive(false);
        });
    }


    #region Handle Skill
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

        // Play charge skill effect
        ParticleSystem particleSystem = Instantiate(chargeSkillFx, kamehaChargeSkillPosition.position, Quaternion.identity);
        particleSystem.Play();

        LeanTween.delayedCall(0.8f, () =>
        {
            energyBallKameha.gameObject.SetActive(true);

            LeanTween.delayedCall(2.2f, () =>
            {
                energyBallKameha.gameObject.SetActive(false);
            });
        });

        // Play sound
        AudioManager.Instance.PlaySFX(AudioManager.Instance.playerKameha);
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

        // Play charge skill effect
        ParticleSystem particleSystem = Instantiate(chargeSkillFx, spiritBoomChargeSkillPosition.position, Quaternion.identity);
        particleSystem.Play();

        LeanTween.delayedCall(1f, () =>
        {
            ParticleSystem energyBall = Instantiate(energyBallSpiritBoom, spiritBoomChargeSkillPosition.position, Quaternion.identity);
            energyBall.Play();

            LeanTween.delayedCall(1.2f, () =>
            {
                LeanTween.moveLocal(energyBall.gameObject, spiritBoomSpawnPosition.position, 0.2f).setOnComplete(() =>
                {
                    Destroy(energyBall.gameObject);
                });
            });
        });

        // Play sound
        AudioManager.Instance.PlaySFX(AudioManager.Instance.playerSpiritBoom);
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

        // Play sound
        AudioManager.Instance.PlaySFX(AudioManager.Instance.playerPowerUp);

        // Play particle effect
        buff_01.gameObject.SetActive(true);
        LeanTween.delayedCall(1.2f, () =>
        {
            buff_01.gameObject.SetActive(false);
        });
    }
    #endregion


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
        // Get enemy
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

        // Play explosion
        ExplosionSpawner.Instance.PlayEnemyExplosion(transform.position);

        // Play sound
        AudioManager.Instance.StopSFX();
        AudioManager.Instance.PlaySFX(AudioManager.Instance.playerDie);
    }
}
