using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPopcupUI : MonoBehaviour
{
    [SerializeField] private Image enemyImagePrefab;
    [SerializeField] private TextMeshProUGUI numberText;
    [SerializeField] private List<Transform> avatarPositionList;

    public void SetEnemyImages(List<Sprite> sprites)
    {
        switch (sprites.Count) 
        {
            case 1:
                Image image = Instantiate(enemyImagePrefab, avatarPositionList[0]);
                image.sprite = sprites[0];
                image.SetNativeSize();
                break;
            case 2:
                Image image0 = Instantiate(enemyImagePrefab, avatarPositionList[1]);
                image0.sprite = sprites[0];
                image0.SetNativeSize();

                Image image1 = Instantiate(enemyImagePrefab, avatarPositionList[2]);
                image1.sprite = sprites[1];
                image1.SetNativeSize();
                break;
            case 3:
                Image image2 = Instantiate(enemyImagePrefab, avatarPositionList[1]);
                image2.sprite = sprites[0];
                image2.SetNativeSize();

                Image image3 = Instantiate(enemyImagePrefab, avatarPositionList[0]);
                image3.sprite = sprites[1];
                image3.SetNativeSize();

                Image image4 = Instantiate(enemyImagePrefab, avatarPositionList[2]);
                image4.sprite = sprites[2];
                image4.SetNativeSize();
                break;
            case 4:
                Image image5 = Instantiate(enemyImagePrefab, avatarPositionList[1]);
                image5.sprite = sprites[0];
                image5.SetNativeSize();

                Image image6 = Instantiate(enemyImagePrefab, avatarPositionList[3]);
                image6.sprite = sprites[1];
                image6.SetNativeSize();

                Image image7 = Instantiate(enemyImagePrefab, avatarPositionList[0]);
                image7.sprite = sprites[2];
                image7.SetNativeSize();

                Image image8 = Instantiate(enemyImagePrefab, avatarPositionList[2]);
                image8.sprite = sprites[3];
                image8.SetNativeSize();
                break;
        }
    }

    public void SetNumberText(int number)
    {
        numberText.text = number.ToString();
    }
}
