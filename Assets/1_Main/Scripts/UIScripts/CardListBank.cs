using AirFishLab.ScrollingList;
using AirFishLab.ScrollingList.ContentManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardListBank : BaseListBank
{

    private CardTransformData[] listCardTransformData;

    private List<TransformSO> listTransformSO;

    private void Awake()
    {
        UpdateCardListBank();
    }

    public override int GetContentCount()
    {
        return listCardTransformData.Length;
    }

    public override IListContent GetListContent(int index)
    {
        return listCardTransformData[index];
    }

    public void UpdateCardListBank()
    {
        listTransformSO = DataManager.Instance.GetCurrentTransformSOList();
        listCardTransformData = new CardTransformData[listTransformSO.Count];

        for (var i = 0; i < listTransformSO.Count; i++)
        {
            bool isLock = DataManager.Instance.IsTransformLocked(i);
            listCardTransformData[i] = new CardTransformData();
            listCardTransformData[i].SetCardTransformData(listTransformSO[i], isLock);
        }
    }
}

public class CardTransformData : IListContent
{
    public int id;
    public Sprite sprite;
    public bool isLock;

    public void SetCardTransformData(TransformSO transformSO, bool newIslock)
    {
        id = transformSO.transformName;
        sprite = transformSO.transformSprite;
        isLock = newIslock;
    }
}


