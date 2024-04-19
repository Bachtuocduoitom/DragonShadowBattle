using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillBase : MonoBehaviour
{

    [SerializeField] protected PlayerSkillSO playerSkillSO;

    protected float speed;
    protected float damage;
    protected float manaCost;

    protected virtual void Awake()
    {
        speed = playerSkillSO.speed;
        damage = playerSkillSO.damage;
        manaCost = playerSkillSO.manaCost;
    }

    protected void Update()
    {
        HandleMovement();

        if (transform.position.x >= 12)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out EnemySkill enemySkill))
        {
            //enemySkill.HitPlayer();
            Destroy(gameObject);
        }
    }

    public virtual void HandleMovement()
    {
        
    }

    public void HitEnemy()
    {
        Destroy(gameObject);
    }

    public void ScaleDamageDependOnEnemyScalePower(float enemyScalePower)
    {
        damage = damage * enemyScalePower;
    }

    public float GetDamage()
    {
        return damage;
    }

    public float GetManaCost()
    {
        return manaCost;
    }   
}
