using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlackBackroundTouchable : MonoBehaviour, IPointerClickHandler
{

    public Action OnTouchBlackBackground;
    public void OnPointerClick(PointerEventData eventData)
    {
        OnTouchBlackBackground?.Invoke();
    }

}
