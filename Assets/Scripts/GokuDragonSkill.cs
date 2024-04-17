using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GokuDragonSkill : PlayerSkillBase
{
    public override void HandleMovement()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
    }
}
