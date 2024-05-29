using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GokuKamehaSkill : PlayerSkillBase
{

    [SerializeField] private ParticleSystem trail;
    [SerializeField] private ParticleSystem trail2;

    private bool startMove = false;
    private bool endMove = false;
    private Vector3 direction;

    public override void HandleMovement()
    {
        if (startMove && direction != Vector3.zero && !endMove)
        {
            transform.Translate(speed * Time.deltaTime * direction);
        }
    }

    public void SetDirection(Vector3 enemyPosition)
    {
        // Get Skill direction
        direction = (enemyPosition - transform.position).normalized;

        // Let Skill move
        startMove = true;
        
        trail.Play();
        trail2.Play();
    }

    public override void HitEnemy()
    {
        PlayExplosion();

        endMove = true;
        LeanTween.delayedCall(0.5f, () =>
        {
            Destroy(gameObject);
        });
    }
}
