using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnlockPopup : MonoBehaviour, IScreen
{

    [SerializeField] private TextMeshProUGUI unlockText;
    [SerializeField] private Button okButton;
    [SerializeField] private BlackBackroundTouchable blackBackgroundTouchable;

   

    private void Start()
    {
        okButton.onClick.AddListener(() =>
        {
            Hide();

            AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        });

        blackBackgroundTouchable.OnTouchBlackBackground += () =>
        {
            Hide();
        };
    }

    public void SetUnlockText(bool canUnlock, int levelToUnlock)
    {
        Show();
        if (canUnlock)
        {
            unlockText.text = "Cool! You are Super\nSaiyan " + levelToUnlock;
        } else
        {
            unlockText.text = "Level " + levelToUnlock + " is required to\nupgrade to previous\nlevel " + (levelToUnlock - 1);
        }
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
