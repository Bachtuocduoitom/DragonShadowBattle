using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAndBaseUI : MonoBehaviour
{

    [SerializeField] private Image characterImage;


    private float a = 420f;
    private float b = 110f;
    private float duration = 1f;
    private float currentAngle;
    private float centerX = 0f;
    private float centerY = -240f;
    private RectTransform rectTransform;

    private string characterTag;
    private LTDescr moveUpDownTween;


    private void Awake()
    {
        InitCharacterMoveUpAndDown();
    }


    private void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();

        // Set current angle to 270 degree if the angle == 0
        if (rectTransform.anchoredPosition.x == 0)
        {
            currentAngle = 270;
        }
        else
        {
            currentAngle = Mathf.Acos((rectTransform.anchoredPosition.x - centerX) / a) * Mathf.Rad2Deg;
        }

        // Scale the character depending on the angle
        if (currentAngle >= 90)
        {
            float scale = 0.0045f * currentAngle - 0.215f;
            rectTransform.localScale = new Vector3(scale, scale, 1);
        }
        else
        {
            float scale = 0.0045f * (180 - currentAngle) - 0.215f;
            rectTransform.localScale = new Vector3(scale, scale, 1);
        }

        
    }

    public void MoveLeftOnEdgeOfElipse(float targetAngle)
    {
        StartCoroutine(MoveLeftOnEdgeOfElipseCoroutine(targetAngle));    
    }

    IEnumerator MoveLeftOnEdgeOfElipseCoroutine(float targetAngle)
    {
        float time = 0;
        float startAngle = currentAngle;
        while (time < duration)
        {
            time += Time.deltaTime;
            float angle = Mathf.Lerp(startAngle, targetAngle, time / duration);

            // Scale the character depending on the angle
            if (angle >= 90)
            {
                float scale = 0.0045f * angle - 0.215f;
                rectTransform.localScale = new Vector3(scale, scale, 1);
            } else
            {
                float scale = 0.0045f *  ( 180 - angle) - 0.215f;
                rectTransform.localScale = new Vector3(scale, scale, 1);
            }

            float x = centerX + a * Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = centerY + b * Mathf.Sin(angle * Mathf.Deg2Rad);
            rectTransform.anchoredPosition = new Vector2(x, y);
            yield return null;
        }
        currentAngle = targetAngle;
        if (currentAngle < 0)
        {
            currentAngle += 360;
        }   
    }

    public void MoveRightOnEdgeOfElipse(float targetAngle)
    {
        StartCoroutine(MoveRightOnEdgeOfElipseCoroutine(targetAngle));
    }

    IEnumerator MoveRightOnEdgeOfElipseCoroutine(float targetAngle)
    {    
        float time = 0;
        float startAngle = currentAngle;
        while (time < duration)
        {
            time += Time.deltaTime;
            float angle = Mathf.Lerp(startAngle, targetAngle, time / duration);

            // Scale the character depending on the angle
            if (angle >= 270)
            {
                float scale = 0.0045f * (540 - angle) - 0.215f;
                rectTransform.localScale = new Vector3(scale, scale, 1);
            }
            else if (angle >= 90)
            {
                float scale = 0.0045f * angle - 0.215f;
                rectTransform.localScale = new Vector3(scale, scale, 1);
            }
            else
            {
                float scale = 0.0045f * (180 - angle) - 0.215f;
                rectTransform.localScale = new Vector3(scale, scale, 1);
            }

            float x = centerX + a * Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = centerY + b * Mathf.Sin(angle * Mathf.Deg2Rad);
            rectTransform.anchoredPosition = new Vector2(x, y);
            yield return null;
        }
        currentAngle = targetAngle;
        if (currentAngle >= 360)
        {
            currentAngle -= 360;
        }
    }

    public string GetCharacterTag()
    {
        return characterTag;
    }

    public void SetCharacterTag(string tag)
    {
        this.characterTag = tag;
    }
    
    public void SetTransformSprite(Sprite sprite)
    {
        characterImage.sprite = sprite;
        characterImage.SetNativeSize(); 
    }

    private void InitCharacterMoveUpAndDown()
    {
        RectTransform chatacterImageRectTransform = characterImage.GetComponent<RectTransform>();
        moveUpDownTween =  LeanTween.moveLocalY(chatacterImageRectTransform.gameObject, chatacterImageRectTransform.localPosition.y + 30, 2f)
            .setLoopPingPong();

        StopCharacterMoveUpAndDown();
    }

    public void StartCharacterMoveUpAndDown()
    {
        moveUpDownTween?.resume();
    }

    public void StopCharacterMoveUpAndDown()
    {
        
        moveUpDownTween?.pause();
    }
}
