using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatePopup : MonoBehaviour, IScreen
{
    [SerializeField] private Button rateButton;
    [SerializeField] private Button cancelButton;

    private void Start()
    {
        rateButton.onClick.AddListener(() =>
        {
            Hide();

            AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        });

        cancelButton.onClick.AddListener(() =>
        {
            Hide();

            AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
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
