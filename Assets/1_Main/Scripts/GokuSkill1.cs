using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GokuSkill1 : PlayerSkillBase
{
    

    public override void HandleMovement()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out EnemySkill enemySkill))
        {
            ExplosionSpawner.Instance.PlayExplosionMedium(transform.position);
            Destroy(gameObject);
        }
    }
}
