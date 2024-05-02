using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArmor : MonoBehaviour
{

    [SerializeField] ParticleSystem twinkArmor;

    private float timeToDisappear = 10f;


    private void Start()
    {
        twinkArmor.Play();    
    }

    private void Update()
    {
        timeToDisappear -= Time.deltaTime;
        if (timeToDisappear <= 0)
        {
            Player.Instance.OutOfArmor();
            twinkArmor.Stop();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out EnemySkill enemySkill)) {
            enemySkill.HitPlayer();

            // Play explosion effect
            ExplosionSpawner.Instance.PlayEnemySkillExposion(enemySkill.GetSizeType(), enemySkill.GetPosition());
        }
    }

    public void SetTimeToDisappear(float time)
    {
        timeToDisappear = time;
    }

    public void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
