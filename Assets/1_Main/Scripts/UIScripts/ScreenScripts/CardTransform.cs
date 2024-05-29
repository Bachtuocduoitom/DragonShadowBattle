
using AirFishLab.ScrollingList;
using AirFishLab.ScrollingList.ContentManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CardTransform : ListBox
{

    [SerializeField] private Image spriteTransform;
    [SerializeField] private TextMeshProUGUI id;
    [SerializeField] private Image lockImage;

    public int Content { get; private set; }
    protected override void UpdateDisplayContent(IListContent content)
    {
        var data = (CardTransformData)content;
        spriteTransform.sprite = data.sprite;
        spriteTransform.SetNativeSize();
        this.id.text = data.id.ToString();
        lockImage.gameObject.SetActive(data.isLock);

        Content = data.id;
    }

    public void UnlockTransform()
    {
        lockImage.gameObject.SetActive(false);
    }

}


