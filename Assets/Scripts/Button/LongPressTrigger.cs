using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class LongPressTrigger : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    //�������Ɣ��肷�鎞��
    public float IntervalSecond = 1f;

    //���������ɔ��΂���A�N�V����
    public Action _onLongPointerDown;

    private float _executeTime;


    // Update is called once per frame
    void Update()
    {
        //_executeTime�����ݎ����𒴂����ꍇ�A
        //_onLongPointerDown����������
        if (_executeTime > 0f && 
            _executeTime <= Time.realtimeSinceStartup)
        {
            _onLongPointerDown();
            _executeTime = -1f;
        }
    }

    private void OnDestroy()
    {
        _onLongPointerDown = null;
    }

    //�v���X���ɏ��������
    public void OnPointerDown(PointerEventData evetData)
    {
        //�������ɒ����������΂��鎞�Ԃ�ݒ�
        _executeTime = Time.realtimeSinceStartup + IntervalSecond;
    }

    public void OnPointerUp(PointerEventData evetData)
    {
        _executeTime = -1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _executeTime -= 1f;
    }

    public void AddLongPressAction(Action action)
    {
        _onLongPointerDown = action;
    }
}
