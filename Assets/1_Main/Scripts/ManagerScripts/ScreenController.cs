using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static ScreenController;

public class ScreenController : MonoBehaviour
{

    public enum ScreenType
    {
        MenuScreen,
        TransformScreen,
        CharacterScreen,
        FreeScreen,
        CoinsScreen
    }

    public static ScreenController Instance { get; private set; }

    [SerializeField] private MenuScreen menuScreen;
    [SerializeField] private TransformScreen transformScreen;
    [SerializeField] private FreeScreen freeScreen;
    [SerializeField] private CoinsScreen coinsScreen;
    [SerializeField] private Image blackTransition;

    private float transitionDurartion = 0.5f;


    private ScreenType currentScreenType;
    private IScreen currentScreen;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        // Show Menu Screen 
        if (currentScreen == null)
        {
            ShowScreen(ScreenType.MenuScreen);
        }

        AudioManager.Instance.PlayMenuMusic();
    }

    public void ShowScreenWithTransition(ScreenType screenType) 
    {
        currentScreenType = screenType;

        blackTransition.gameObject.SetActive(true);
        LeanTween.alphaCanvas(blackTransition.GetComponent<CanvasGroup>(), 1, transitionDurartion)
            .setOnComplete(() =>
            {
                if (currentScreen != null)
                {
                    currentScreen.Hide();
                }

                currentScreen = GetScreen(screenType);
                currentScreen.Show();

                LeanTween.alphaCanvas(blackTransition.GetComponent<CanvasGroup>(), 0, transitionDurartion)
                .setOnComplete(() =>
                {
                    blackTransition.gameObject.SetActive(false);
                });
            });
    }

    public void ShowScreen(ScreenType screenType)
    {
        currentScreenType = screenType;

        if (currentScreen != null)
        {
            currentScreen.Hide();
        }

        currentScreen = GetScreen(screenType);
        currentScreen.Show();

    }

    private IScreen GetScreen(ScreenType screenType)
    {
        switch (screenType)
        {
            case ScreenType.MenuScreen:
                return menuScreen;
            case ScreenType.TransformScreen:
                return transformScreen;
            case ScreenType.FreeScreen:
                return freeScreen;
            case ScreenType.CoinsScreen:
                return coinsScreen;
            default:
                return null;
        }
    }
}
