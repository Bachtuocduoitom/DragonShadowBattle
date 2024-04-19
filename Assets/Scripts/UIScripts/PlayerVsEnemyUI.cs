using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVsEnemyUI : MonoBehaviour
{

    public Action OnZoomInFinished;

    [SerializeField] private Image playerTwinkImage;
    [SerializeField] private Image enemyTwinkImage;

    private const string ZOOM_IN = "ZoomIn";
    private const string PLAYER_VS_ENEMY_ZOOMIN = "PlayerVsEnemyZoomIn";

    Animator animator;

    private bool checkZoomInFinished = false;
    private float timeDuration = 4f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    private void Start()
    {
        enemyTwinkImage.sprite = DataManager.Instance.GetSpriteForCurrentEnemy();
        enemyTwinkImage.SetNativeSize();
    }

    public void ZoomIn()
    {
        gameObject.SetActive(true);
        animator.SetTrigger(ZOOM_IN);

        // Play sound
        AudioManager.Instance.PlaySFX(AudioManager.Instance.vs);
    }

    private void Update()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName(PLAYER_VS_ENEMY_ZOOMIN) && !checkZoomInFinished)
        {
            checkZoomInFinished = true;
            TwinkTwoImage();
            OnZoomInFinished?.Invoke();
        }
    }

    private void TwinkTwoImage()
    {
        LeanTween.moveLocalY(playerTwinkImage.gameObject, -0.5f, timeDuration)
            .setEaseOutQuad()
            .setLoopPingPong();

        LeanTween.moveLocalY(enemyTwinkImage.gameObject, 0.5f, timeDuration)
            .setEaseOutQuad()
            .setLoopPingPong()
            .setDelay(0.1f);
    }
}
