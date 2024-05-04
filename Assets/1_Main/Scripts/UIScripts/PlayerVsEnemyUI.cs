using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVsEnemyUI : MonoBehaviour
{

    public Action OnZoomInFinished;

    [SerializeField] private Image playerTwinkImage;
    [SerializeField] private List<Image> enemyTwinkImageList;

    private const string ZOOM_IN = "ZoomIn";
    private const string PLAYER_VS_ENEMY_ZOOMIN = "PlayerVsEnemyZoomIn";

    Animator animator;

    private bool checkZoomInFinished = false;
    private float timeDuration = 4f;
    private List<Sprite> enemySpriteList;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    private void Start()
    {
        playerTwinkImage.sprite = DataManager.Instance.GetSpriteForCurrentTransform(0);
        playerTwinkImage.SetNativeSize();

        enemySpriteList = DataManager.Instance.GetEnemySpritesForCurrentLevel();
        switch (enemySpriteList.Count)
        {
            case 1:
                enemyTwinkImageList[0].gameObject.SetActive(true);
                enemyTwinkImageList[0].sprite = enemySpriteList[0];
                enemyTwinkImageList[0].SetNativeSize();
                break;
            case 2:
                enemyTwinkImageList[2].gameObject.SetActive(true);
                enemyTwinkImageList[2].sprite = enemySpriteList[0];
                enemyTwinkImageList[2].SetNativeSize();

                enemyTwinkImageList[1].gameObject.SetActive(true);
                enemyTwinkImageList[1].sprite = enemySpriteList[1];
                enemyTwinkImageList[1].SetNativeSize();
                break;
            case 3:
                enemyTwinkImageList[2].gameObject.SetActive(true);
                enemyTwinkImageList[2].sprite = enemySpriteList[0];
                enemyTwinkImageList[2].SetNativeSize();

                enemyTwinkImageList[0].gameObject.SetActive(true);
                enemyTwinkImageList[0].sprite = enemySpriteList[1];
                enemyTwinkImageList[0].SetNativeSize();

                enemyTwinkImageList[1].gameObject.SetActive(true);
                enemyTwinkImageList[1].sprite = enemySpriteList[2];
                enemyTwinkImageList[1].SetNativeSize();
                break;
            case 4:
                enemyTwinkImageList[2].gameObject.SetActive(true);
                enemyTwinkImageList[2].sprite = enemySpriteList[0];
                enemyTwinkImageList[2].SetNativeSize();

                enemyTwinkImageList[3].gameObject.SetActive(true);
                enemyTwinkImageList[3].sprite = enemySpriteList[1];
                enemyTwinkImageList[3].SetNativeSize();

                enemyTwinkImageList[0].gameObject.SetActive(true);
                enemyTwinkImageList[0].sprite = enemySpriteList[2];
                enemyTwinkImageList[0].SetNativeSize();

                enemyTwinkImageList[1].gameObject.SetActive(true);
                enemyTwinkImageList[1].sprite = enemySpriteList[3];
                enemyTwinkImageList[1].SetNativeSize();
                break;
        }

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
            //TwinkTwoImage();
            OnZoomInFinished?.Invoke();
        }
    }

    private void TwinkTwoImage()
    {
        // Player Twink
        LeanTween.moveLocalY(playerTwinkImage.gameObject, -0.5f, timeDuration)
            .setEaseOutQuad()
            .setLoopPingPong();

        // Enemies Twink
        LeanTween.moveLocalY(enemyTwinkImageList[0].gameObject, 0.5f, timeDuration)
            .setEaseOutQuad()
            .setLoopPingPong()
            .setDelay(0.1f);
        LeanTween.moveLocalY(enemyTwinkImageList[1].gameObject, 220f, 3f)
            .setEaseOutQuad()
            .setLoopPingPong()
            .setDelay(0.1f);
        LeanTween.moveLocalY(enemyTwinkImageList[2].gameObject, -520f, 4f)
            .setEaseOutQuad()
            .setLoopPingPong()
            .setDelay(0.1f);
        LeanTween.moveLocalY(enemyTwinkImageList[3].gameObject, -300f, 3.5f)
            .setEaseOutQuad()
            .setLoopPingPong()
            .setDelay(0.1f);
    }
}
