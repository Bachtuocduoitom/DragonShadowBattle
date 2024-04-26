using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GoldAmountTouchable : MonoBehaviour, IPointerClickHandler
{

    public Action OnTouchGoldAmount;
    public void OnPointerClick(PointerEventData eventData)
    {
        OnTouchGoldAmount?.Invoke();
    }

}