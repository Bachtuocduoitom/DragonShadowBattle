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
        musicButton.image.sprite = musicButtonTurnOnSprite;
        soundButton.image.sprite = soundButtonTurnOnSprite;
    }

    private void Start()
    {
        settingButton.onClick.AddListener(() =>
        {
            animator.SetTrigger(CLICKSETTING);
        });

        soundButton.onClick.AddListener(() =>
        {
            Debug.Log("Sound");
            soundButton.image.sprite = soundButton.image.sprite == soundButtonTurnOnSprite ? soundButtonTurnOffSprite : soundButtonTurnOnSprite;
        });

        musicButton.onClick.AddListener(() =>
        {
            Debug.Log("Music");
            musicButton.image.sprite = musicButton.image.sprite == musicButtonTurnOnSprite ? musicButtonTurnOffSprite : musicButtonTurnOnSprite;
        });

        moreGameButton.onClick.AddListener(() =>
        {
            Debug.Log("More Game");
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
}
