using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickActionButton : MonoBehaviour, IPointerClickHandler
{
    public Action OnClickAction;

    //�N���b�N���̃A�N�V����
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (enabled == false)
        {
            return;
        }
        if (OnClickAction != null)
        {
            OnClickAction();
        }
    }
}
