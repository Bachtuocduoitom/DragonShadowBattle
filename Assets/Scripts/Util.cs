using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static string GetCurrencyFormat(int amount)
    {
        string formattedM = string.Format("{0:#,0}", amount);
        return formattedM.Replace(",", ".");
    }
    
}
