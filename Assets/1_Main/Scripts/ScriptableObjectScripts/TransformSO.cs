using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class TransformSO : ScriptableObject
{
    public int transformName;
    public Sprite transformSprite;
    public float maxHealth;
    public float maxMana;
    public float powerScale;
    public int power;
    public int cost;
}
