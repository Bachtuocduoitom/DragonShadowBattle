using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatisticBarUI : MonoBehaviour
{
    [SerializeField] private Image healthBarImage;
    [SerializeField] private Image manaBarImage;
    [SerializeField] private Image enemyImage;
    [SerializeField] private TextMeshProUGUI enemyName;

    private Mana mana;
    private Health health;

    private Enemy enemy;
    private const string TAG_ENEMY = "Enemy";

    private void Awake()
    {
        mana = new Mana();
        health = new Health();
    }

    private void Start()
    {
        
    }

    private void Enemy_Damage(float amount)
    {
        health.TakeDamage(amount);
    }

    private void Health_OutOfHealth()
    {
        enemy.Die();
    }

    private void Update()
    {
        mana.Update();

        manaBarImage.fillAmount = mana.GetManaNormalized();
        healthBarImage.fillAmount = health.GetHealthNormalized();
    }

    private void SetupStatisticData()
    {
        EnemySO enemySO = enemy.GetEnemySO();

        health.UpdateMaxHealth(enemySO.maxHealth);
        mana.UpdateMaxMana(enemySO.maxMana);
        enemyImage.sprite = enemySO.sprite;
        enemyName.text = enemySO.enemyName;
    }

    public void SetEnemy()
    {
        // Get enemy
        enemy = GameObject.FindWithTag(TAG_ENEMY).GetComponent<Enemy>();

        SetupStatisticData();

        enemy.OnHitDamage += Enemy_Damage;

        health.OnOutOfHealth += Health_OutOfHealth;
    }
}
