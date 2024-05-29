using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill : MonoBehaviour
{

    public enum SizeType
    {
        Small,
        Medium,
        Large
    }

    [SerializeField] private EnemySkillSO enemySkillSO;

    private float speed;
    private float damage;
    private SizeType sizeType;

    private void Awake()
    {
        speed = enemySkillSO.speed;
        damage = enemySkillSO.damage;
        sizeType = enemySkillSO.sizeType;
    }

    private void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x <= -12)
        {
            Destroy(gameObject);
        }
    }

    public void HitPlayer()
    {
        // Play explosion effect
        ExplosionSpawner.Instance.PlayEnemySkillExposion(GetSizeType(), GetPosition());

        Destroy(gameObject);
    }

    public void ScaleDamageDependOnEnemyScalePower(float enemyScalePower)
    {
        //Debug.Log("Enemy scale power: " + enemyScalePower + ", Skill damage: " + damage);
        damage *= enemyScalePower;
        //Debug.Log("New skill damage: " + damage);
    }

    public float GetDamage()
    {
        return damage;
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
