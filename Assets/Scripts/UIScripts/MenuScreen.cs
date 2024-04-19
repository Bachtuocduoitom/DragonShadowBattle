using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScreen : MonoBehaviour, IScreen
{

    [SerializeField] private Image gokuImage;
    [SerializeField] private Image cellImage;
    [SerializeField] private Button spinButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button playButton;
    [SerializeField] private Button freeButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button coinsButton;
    [SerializeField] private SpinPopup spinPopup;
    [SerializeField] private SettingPopup settingPopup;

    private void Start()
    {
        //Show Spin Screen
        spinButton.onClick.AddListener(() =>
        {
            spinPopup.Show();

            AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        });

        //Show Setting Screen
        settingButton.onClick.AddListener(() =>
        {
            settingPopup.Show();

            AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        });

        //Show Play Scene
        playButton.onClick.AddListener(() =>
        {
            //SceneManager.LoadScene("GamePlayScene");
            SceneController.Instance.LoadGameplayScene();

            AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        });

        //Show Free Scene
        freeButton.onClick.AddListener(() =>
        {
            ScreenController.Instance.ShowScreenWithTransition(ScreenController.ScreenType.FreeScreen);

            AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        });

        //Show Shop Scene
        shopButton.onClick.AddListener(() =>
        {
            ScreenController.Instance.ShowScreenWithTransition(ScreenController.ScreenType.TransformScreen);

            AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        });

        //Show Coins Scene
        coinsButton.onClick.AddListener(() =>
        {
            ScreenController.Instance.ShowScreenWithTransition(ScreenController.ScreenType.CoinsScreen);

            AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        });


        ActionEffects();
    }

    private void ActionEffects()
    {
        gokuImage.transform.LeanMoveLocal(new Vector3(gokuImage.transform.localPosition.x + 30, gokuImage.transform.localPosition.y - 30, 0), 1.5f).setLoopPingPong();
        cellImage.transform.LeanMoveLocal(new Vector3(cellImage.transform.localPosition.x + 30, cellImage.transform.localPosition.y + 30, 0), 1.5f).setLoopPingPong();

        LeanTween.rotateAroundLocal(spinButton.gameObject, Vector3.forward, 360f, 2f)
        .setLoopClamp();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public bool IsActive()
    {
        return gameObject.activeInHierarchy;
    }

    public bool IsShowed()
    {
        return gameObject.activeSelf;
    }
}
