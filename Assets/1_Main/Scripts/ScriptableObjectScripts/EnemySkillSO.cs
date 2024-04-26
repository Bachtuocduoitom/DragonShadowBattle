using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class EnemySkillSO : ScriptableObject
{
    public string skillName;
    public float speed;
    public float damage;
    public EnemySkill.SizeType sizeType;
}
