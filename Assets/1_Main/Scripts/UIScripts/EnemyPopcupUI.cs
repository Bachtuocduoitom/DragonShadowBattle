using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPopcupUI : MonoBehaviour
{
    [SerializeField] private Image enemyImage;
    [SerializeField] private TextMeshProUGUI numberText;

    public void SetEnemyImage(Sprite sprite)
    {
        enemyImage.sprite = sprite;
        enemyImage.SetNativeSize();
    }

    public void SetNumberText(int number)
    {
        numberText.text = number.ToString();
    }
}
