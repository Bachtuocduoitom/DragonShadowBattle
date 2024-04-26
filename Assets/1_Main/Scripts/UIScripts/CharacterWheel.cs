using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class CharacterWheel : MonoBehaviour
{

    [SerializeField] private Transform pentagonLine;
    [SerializeField] private RectTransform parentRectTransform;
    [SerializeField] private CharacterAndBaseUI CharacterAndBaseUIPrefab;

    private CharacterAndBaseUI[] characterAndBaseUIList;
    private int characterAndBaseUIListLength = 5;

    private Sprite preCharacterSpriteLevel0;

    private float zoomInDuration = 1f;

    private void Awake()
    {
        characterAndBaseUIList = GetComponentsInChildren<CharacterAndBaseUI>();


        // Set characterTag for each character and base UI
        SetTagForCharacterAndBaseUIList();
       
    }

    private void Start()
    {
        // Tween Move up down for front character
        PlayMoveUpAndDownForCurrentTransform();
    }

    public void RotateLeft()
    {
        for (int i = 0; i < characterAndBaseUIListLength; i++)
        {
            switch (i)
            {
                case 0:
                    characterAndBaseUIList[i].MoveRightOnEdgeOfElipse(113);
                    parentRectTransform.GetChild(2).SetSiblingIndex(0);
                    break;
                case 1:
                    characterAndBaseUIList[i].MoveRightOnEdgeOfElipse(180);
                    break;
                case 2:
                    characterAndBaseUIList[i].MoveRightOnEdgeOfElipse(66);
                    parentRectTransform.GetChild(4).SetSiblingIndex(2);
                    break;
                case 3:
                    characterAndBaseUIList[i].MoveRightOnEdgeOfElipse(270);
                    break;
                case 4:
                    characterAndBaseUIList[i].MoveRightOnEdgeOfElipse(360);
                    
                    break;

            }
        }

        // Rotate pentagon line
        LeanTween.rotateZ(pentagonLine.gameObject, pentagonLine.eulerAngles.z + 72, 1f);

        // Stop move up and down for current character
        StopMoveUpAndDownForCurrentTransform();

        // Re-assign characterAndBaseUIList
        characterAndBaseUIList = GetComponentsInChildren<CharacterAndBaseUI>();

        // Update previous Charracter to level 0
        preCharacterSpriteLevel0 = DataManager.Instance.GetSpriteForCurrentTransform(0);
        LeanTween.delayedCall(0.5f, () =>
        {

            characterAndBaseUIList[2].SetTransformSprite(preCharacterSpriteLevel0);
        });


        // Change current character
        DataManager.Instance.SetCurrentCharacter(characterAndBaseUIList[4].GetCharacterTag());
    }

    public void RotateRight()
    {
        for (int i = 0; i < characterAndBaseUIListLength; i++)
        {
            switch (i)
            {
                case 0:
                    characterAndBaseUIList[i].MoveLeftOnEdgeOfElipse(0);
                    parentRectTransform.GetChild(0).SetSiblingIndex(2);
                    break;
                case 1:
                    characterAndBaseUIList[i].MoveLeftOnEdgeOfElipse(66);

                    break;
                case 2:
                    characterAndBaseUIList[i].MoveLeftOnEdgeOfElipse(-90);
                    parentRectTransform.GetChild(1).SetSiblingIndex(4);
                    break;
                case 3:
                    characterAndBaseUIList[i].MoveLeftOnEdgeOfElipse(113);

                    break;
                case 4:
                    characterAndBaseUIList[i].MoveLeftOnEdgeOfElipse(180);
                    parentRectTransform.GetChild(2).SetSiblingIndex(1);
                    break;

            }

        }

        // Rotate pentagon line
        LeanTween.rotateZ(pentagonLine.gameObject, pentagonLine.eulerAngles.z - 72, 1f);

        // Stop move up and down for current character
        StopMoveUpAndDownForCurrentTransform();

        // Re-assign characterAndBaseUIList
        characterAndBaseUIList = GetComponentsInChildren<CharacterAndBaseUI>();

        // Update previous Charracter to level 0
        preCharacterSpriteLevel0 = DataManager.Instance.GetSpriteForCurrentTransform(0);
        LeanTween.delayedCall(0.5f, () =>
        {
            
            characterAndBaseUIList[3].SetTransformSprite(preCharacterSpriteLevel0);
        });

 
        // Change current Character
        DataManager.Instance.SetCurrentCharacter(characterAndBaseUIList[4].GetCharacterTag());
    }   

    private void SetTagForCharacterAndBaseUIList()
    {
        for (int i = 0; i < characterAndBaseUIListLength; i++)
        {
            switch (i)
            {
                case 0:
                    characterAndBaseUIList[i].SetCharacterTag(DataManager.Instance.GetCharacterByIndex(2));
                    break;
                case 1:
                    characterAndBaseUIList[i].SetCharacterTag(DataManager.Instance.GetCharacterByIndex(3));
                    break;
                case 2:
                    characterAndBaseUIList[i].SetCharacterTag(DataManager.Instance.GetCharacterByIndex(1));
                    break;
                case 3:
                    characterAndBaseUIList[i].SetCharacterTag(DataManager.Instance.GetCharacterByIndex(4));
                    break;
                case 4:
                    characterAndBaseUIList[i].SetCharacterTag(DataManager.Instance.GetCurrentCharacter());
                    break;
            }
        }
    }

    public void UpdateCurrentCharacterImage(Sprite sprite)
    {
        characterAndBaseUIList[4].SetTransformSprite(sprite);
    }

    public void PlayMoveUpAndDownForCurrentTransform()
    {
        characterAndBaseUIList[4].StartCharacterMoveUpAndDown();
    }

    public void StopMoveUpAndDownForCurrentTransform()
    {
        characterAndBaseUIList[4].StopCharacterMoveUpAndDown();
    }

    public void PlayMoveInTween()
    {
        // Character wheel Zoom in
        LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), zoomInDuration)
            .setFrom(new Vector3(0.8f, 0.8f, 0.8f));

        

        // Pentagon line Zoom in
       // LeanTween.scale(pentagonLine.gameObject, new Vector3(50f, 50f, 50f), zoomInDuration)
        //    .setFrom(new Vector3(45f, 45f, 45f));
    }
}
