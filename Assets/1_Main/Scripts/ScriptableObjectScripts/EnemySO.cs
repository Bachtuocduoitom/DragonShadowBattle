using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class EnemySO : ScriptableObject
{

    public string enemyName;
    public Sprite sprite;
    public float maxHealth;
    public float maxMana;
    public float powerScale;
    public List<Transform> skillPrefabList;
}
