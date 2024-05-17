using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BoxItemSO : ScriptableObject
{
    public Sprite sprite;
    public string describe;
    public BoxItem.BoxItemTypes boxItemType;
}
