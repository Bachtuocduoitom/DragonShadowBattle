using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreStartGameplayScreen : MonoBehaviour, IScreen
{

    [SerializeField] private PlayerVsEnemyUI playerVsEnemyUI;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button playButton;

    private float menuButtonTargetX = 260f;
    private float shopButtonTargetX = -260f;
    private float playButtonTargetY = 175f;

    private float timeDuration = 0.5f;


    private void Awake()
    {
        menuButton.onClick.AddListener(() =>
        {
            SceneController.Instance.LoadMenuScene(ScreenController.ScreenType.MenuScreen);

            AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        });

        shopButton.onClick.AddListener(() =>
        {
            SceneController.Instance.LoadMenuScene(ScreenController.ScreenType.TransformScreen);


            AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        });


        playButton.onClick.AddListener(() =>
        {
           GameManager.Instance.UpdateGameState(GameManager.State.WaitingToStartGameplay);

            AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        });
    }
    private void Start()
    {
        playerVsEnemyUI.OnZoomInFinished += PlayerVsEnemyUI_OnZoomInFinished;
    }

    private void PlayerVsEnemyUI_OnZoomInFinished()
    {
        LeanTween.moveX(menuButton.GetComponent<RectTransform>(), menuButtonTargetX, timeDuration)
            .setEaseOutBack();

        LeanTween.moveX(shopButton.GetComponent<RectTransform>(), shopButtonTargetX, timeDuration)
            .setEaseOutBack();

        LeanTween.moveY(playButton.GetComponent<RectTransform>(), playButtonTargetY, timeDuration)
            .setEaseOutBack()
            .setDelay(0.2f);
        
    }

    public void Show()
    {
        gameObject.SetActive(true);
        playerVsEnemyUI.ZoomIn();
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
