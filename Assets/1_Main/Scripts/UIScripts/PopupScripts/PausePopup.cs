using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePopup : MonoBehaviour, IScreen
{
    public Action OnResume;
    public Action OnBackToMenu;

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button menuButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            Hide();
            OnResume?.Invoke();
        });

        menuButton.onClick.AddListener(() =>
        {
            OnBackToMenu?.Invoke();
        });
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public bool IsShowed()
    {
        return gameObject.activeSelf;
    }
}
