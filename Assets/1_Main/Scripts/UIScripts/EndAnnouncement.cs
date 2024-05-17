using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndAnnouncement : MonoBehaviour, IScreen
{


    [SerializeField] private TextMeshProUGUI victoryText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Image underline;


    private void Start()
    {
        Hide();
    }

    
    public void ShowVictory()
    {
        Show();

        victoryText.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        underline.gameObject.SetActive(true);
    }

    public void ShowGameOver()
    {
        Show();

        victoryText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        underline.gameObject.SetActive(true);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public bool IsShowed()
    {
        return gameObject.activeSelf;
    }
}
