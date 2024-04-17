using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill : MonoBehaviour
{

    [SerializeField] private EnemySkillSO enemySkillSO;

    private float speed;
    private float damage;

    private void Start()
    {
        speed = enemySkillSO.speed;
        damage = enemySkillSO.damage;
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
}
