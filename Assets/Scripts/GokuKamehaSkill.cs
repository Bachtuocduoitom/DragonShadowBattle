using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GokuKamehaSkill : PlayerSkillBase
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
        // get Skill direction
        direction = (enemyPosition - transform.position).normalized;

        // let Skill move
        startMove = true;
        
    }
}
