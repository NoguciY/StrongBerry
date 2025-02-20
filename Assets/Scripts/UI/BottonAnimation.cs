using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//アニメーションさせたいUIオブジェクトにアタッチする
//ボタンをアニメーションさせる

public class BottonAnimation : MonoBehaviour
{
    private void Start()
    {
        ChangeUISize();
    }

    private void ChangeUISize()
    {
        transform.DOScale(0.1f, 1f)
        .SetRelative(true)
        .SetEase(Ease.OutQuart)
        .SetLoops(-1, LoopType.Restart);
    }
}
