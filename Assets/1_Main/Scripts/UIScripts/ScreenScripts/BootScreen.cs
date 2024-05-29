using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootScreen : MonoBehaviour
{

    private void Start()
    {
        DOTween.To(() => 0, x => { }, 0, 1f).OnComplete(() =>
        {
            SceneController.Instance.LoadMenuScene(ScreenController.ScreenType.MenuScreen);
        });
    }
}
