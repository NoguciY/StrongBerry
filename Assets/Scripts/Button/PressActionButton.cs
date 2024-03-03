using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PressActionButton : MonoBehaviour, IPointerDownHandler
{
    public Action OnPressAction;

    //プレス時のアクション
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (enabled == false)
        {
            return;
        }
        if (OnPressAction != null)
        {
            OnPressAction();
        }
    }
}
