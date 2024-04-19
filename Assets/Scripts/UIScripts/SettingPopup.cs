using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPopup : MonoBehaviour, IScreen
{
    private const string CLICKSETTING = "ClickSetting";

    [SerializeField] private Button settingButton;
    [SerializeField] private Button soundButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button moreGameButton;
    [SerializeField] private BlackBackroundTouchable blackBackroundTouchable;

    [SerializeField] private Sprite soundButtonTurnOnSprite;
    [SerializeField] private Sprite soundButtonTurnOffSprite;
    [SerializeField] private Sprite musicButtonTurnOnSprite;
    [SerializeField] private Sprite musicButtonTurnOffSprite;

   private Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        musicButton.image.sprite = DataManager.Instance.IsMusicOn() ? musicButtonTurnOnSprite : musicButtonTurnOffSprite;
        soundButton.image.sprite = DataManager.Instance.IsSoundOn() ? soundButtonTurnOnSprite : soundButtonTurnOffSprite;
    }

    private void Start()
    {
        settingButton.onClick.AddListener(() =>
        {
            animator.SetTrigger(CLICKSETTING);

            AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        });

        soundButton.onClick.AddListener(() =>
        {
            Debug.Log("Sound");
            if (soundButton.image.sprite == soundButtonTurnOnSprite)
            {
                AudioManager.Instance.TurnOffSFX();
                soundButton.image.sprite = soundButtonTurnOffSprite;

                DataManager.Instance.TurnOffSound();
            }
            else
            {
                AudioManager.Instance.TurnOnSFX();
                soundButton.image.sprite = soundButtonTurnOnSprite;
                
                DataManager.Instance.TurnOnSound();
            }

            AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        });

        musicButton.onClick.AddListener(() =>
        {
            Debug.Log("Music");
            if (musicButton.image.sprite == musicButtonTurnOnSprite)
            {
                AudioManager.Instance.TurnOffMusic();
                musicButton.image.sprite = musicButtonTurnOffSprite;

                DataManager.Instance.TurnOffMusic();
            }
            else
            {
                AudioManager.Instance.TurnOnMusic();
                musicButton.image.sprite = musicButtonTurnOnSprite;

                DataManager.Instance.TurnOnMusic();
            }

            AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        });

        moreGameButton.onClick.AddListener(() =>
        {
            Debug.Log("More Game");

            AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        });

        blackBackroundTouchable.OnTouchBlackBackground += () =>
        {
            Hide();
        };

        
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
