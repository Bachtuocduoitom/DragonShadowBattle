using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player;

public class PlayerSkillBase : MonoBehaviour
{
    public enum SizeType
    {
        Small,
        Medium,
        Large
    }

    [SerializeField] protected PlayerSkillSO playerSkillSO;

    protected float speed;
    protected float damage;
    protected float manaCost;
    protected SizeType sizeType;

    protected virtual void Awake()
    {
        speed = playerSkillSO.speed;
        damage = playerSkillSO.damage;
        manaCost = playerSkillSO.manaCost;
        sizeType = playerSkillSO.sizeType;
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
            //Debug.Log(enemySkill.GetDamage() + " " + damage);
            if (enemySkill.GetDamage() < this.damage)
            {
                enemySkill.HitPlayer();
            }
            else
            {
                HitEnemy();
            }
            //ExplosionSpawner.Instance.PlayExplosionMedium(transform.position);
            //Destroy(gameObject);
        }
    }

    public virtual void HandleMovement()
    {
        
    }

    public virtual void HitEnemy()
    {
        PlayExplosion();
        Destroy(gameObject);
    }

    public void PlayExplosion()
    {
        ExplosionSpawner.Instance.PlayPlayerSkillExposion(GetSizeType(), GetPosition());
    }

    public void ScaleDamageDependOnEnemyScalePower(float enemyScalePower)
    {
        damage *= enemyScalePower;
    }

    public float GetDamage()
    {
        return damage;
    }

    public float GetManaCost()
    {
        return manaCost;
    }   

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public SizeType GetSizeType()
    {
        return sizeType;
    }
}
