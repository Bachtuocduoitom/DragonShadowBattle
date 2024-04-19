using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackScreen : MonoBehaviour, IScreen
{


    public void Hide()
    {
        LeanTween.alphaCanvas(GetComponent<CanvasGroup>(), 0, 0.2f).setOnComplete(() =>
        {
            gameObject.SetActive(false);
        });
        
    }

    public bool IsShowed()
    {
        return gameObject.activeSelf;
    }

    public void Show()
    {
        gameObject.SetActive(true);
        LeanTween.alphaCanvas(GetComponent<CanvasGroup>(), 1, 0.5f).setOnComplete(() => 
        {
            GameManager.Instance.UpdateGameState(GameManager.State.WaitingForEnemyMoveIn);
        });
    }
}
