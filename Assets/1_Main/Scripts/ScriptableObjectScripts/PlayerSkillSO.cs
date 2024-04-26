using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class PlayerSkillSO : ScriptableObject
{
    public string skillName;
    public float speed;
    public float damage;
    public float manaCost;
    public PlayerSkillBase.SizeType sizeType;
}
