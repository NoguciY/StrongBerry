using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class LongPressTrigger : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    //長押しと判定する時間
    public float IntervalSecond = 1f;

    //長押し時に発火するアクション
    public Action _onLongPointerDown;

    private float _executeTime;


    // Update is called once per frame
    void Update()
    {
        //_executeTimeが現在時刻を超えた場合、
        //_onLongPointerDownを処理する
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

    //プレス時に処理される
    public void OnPointerDown(PointerEventData evetData)
    {
        //押下時に長押しが発火する時間を設定
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
