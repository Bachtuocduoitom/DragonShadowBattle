using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        // Set default screen
        currentScreenType = ScreenType.MenuScreen;
    }

    private void Start()
    {
        ShowScreen(currentScreenType);
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
                return (IScreen) menuScreen;
            case ScreenType.TransformScreen:
                return (IScreen) transformScreen;
            case ScreenType.FreeScreen:
                return (IScreen) freeScreen;
            case ScreenType.CoinsScreen:
                return (IScreen) coinsScreen;
            default:
                return null;
        }
    }


}
