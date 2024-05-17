using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; }

    [SerializeField] private Image blackImage;

    private float fadeDurationIn = 0.5f;
    private float fadeDurationOut = 0.5f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadGameplayScene()
    {
        // Reset current character 
        DataManager.Instance.ResetCurrentCharacter();

        SceneManager.LoadScene("GamePlayScene");
    }

    public void LoadMenuScene(ScreenController.ScreenType screenType)
    {
        blackImage.gameObject.SetActive(true);

        LeanTween.alphaCanvas(blackImage.GetComponent<CanvasGroup>(), 1, fadeDurationIn)
            .setOnComplete(() =>
            {
                
                AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MenuScene");
                asyncLoad.completed += (AsyncOperation obj) =>
                {
                    if (screenType != ScreenController.ScreenType.MenuScreen)
                    {
                        ScreenController.Instance.ShowScreen(screenType);
                    }
                    

                    LeanTween.alphaCanvas(blackImage.GetComponent<CanvasGroup>(), 0, fadeDurationOut)
                    .setOnComplete(() =>
                    {
                        blackImage.gameObject.SetActive(false);
                    });
                };
            });
    }

    public void Update()
    {
        if (blackImage.IsDestroyed())
        {
            Debug.Log("Black Image is destroyed");
        }
    }
}
