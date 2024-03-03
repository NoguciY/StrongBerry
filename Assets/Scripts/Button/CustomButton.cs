using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

[RequireComponent(typeof(LongPressTrigger))]
public class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public Action OnClickAction;
    public Action OnPressAction;
    public Action OnReleaseAction;
    public Action OnLongPressAction;

    private bool _isLongPress;

    private void Awake()
    {
        //LongPressTrigge��OnLongPress�̏�����o�^���A
        //���������莞��OnLongPress�����s�����悤�ɂ���
        var longPressTrigger = gameObject.GetComponent<LongPressTrigger>();
        longPressTrigger.AddLongPressAction(OnLongPress);
    }

    private void OnDestroy()
    {
        OnClickAction = null;
        OnPressAction = null;
        OnReleaseAction = null;
        OnLongPressAction = null;
    }

    //�v���X���̃A�N�V����
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
        _isLongPress = false;
    }

    //�N���b�N���̃A�N�V����
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (enabled == false)
        {
            return;
        }
        if (_isLongPress == false && OnClickAction != null)
        {
            OnClickAction();
        }
    }

    //���������̃A�N�V����
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        if (enabled == false)
        {
            return;
        }
        if (_isLongPress == false && OnReleaseAction != null)
        {
            OnReleaseAction();
        }
    }

    //���������̃A�N�V����
    public virtual void OnLongPress()
    {
        if (enabled == false)
        {
            return;
        }
        if (OnLongPressAction != null)
        {
            OnLongPressAction();
        }

        //�N���b�N���̃A�N�V���������s�����Ȃ��悤�ɂ���
        _isLongPress = true;
    }
}
