using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CoinsCardSO : ScriptableObject
{
   
    public Sprite cardIcon;
    public CoinCardUI.CoinsCardType cardType;
    public int goldGainText;
    public string cardValueText;
    public string cardCost;
}
