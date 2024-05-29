using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GokuSpiritBoomSkill : PlayerSkillBase
{
    private bool startMove = false;
    private Vector3 direction;

    public override void HandleMovement()
    {
        if (startMove && direction != Vector3.zero)
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
    }

}
