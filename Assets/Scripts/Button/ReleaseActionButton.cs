using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class ReleaseActionButton : MonoBehaviour, IPointerUpHandler
{
    public Action OnReleaseAction;

    //リリース時のアクション
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        if (enabled == false)
        {
            return;
        }
        if (OnReleaseAction != null)
        {
            OnReleaseAction();
        }
    }
}
