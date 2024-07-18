using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

//現在のプレイヤーの速さを表示するUI

public class SpeedUI : MonoBehaviour
{
    private TextMeshProUGUI speedText;

    private Image speedImage;

    private float maxSpeed;

    private bool isMaxSpeed;

    private Tween tweener;

    public void Init(TextMeshProUGUI speedText, Image speedImage, float maxSpeed)
    {
        this.speedText = speedText;

        this.speedImage = speedImage;

        this.maxSpeed = maxSpeed;

        isMaxSpeed = false;

        tweener = DOTween.Sequence()
            //1秒で赤色に変化し元の色に戻るのをずっと繰り返す
            .Join(speedImage.DOColor(Color.red, 1f)
                        .SetLoops(-1, LoopType.Yoyo))
            
            .Join(transform.DOScale(0.1f, 1f)
                        .SetRelative(true)
                        .SetEase(Ease.OutQuart)
                        .SetLoops(-1, LoopType.Restart));
    }

    /// <summary>
    /// プレイヤーの速さを表示する
    /// </summary>
    /// <param name="playerSpeed">プレイヤーの速さ</param>
    public void DisplaySpeed(float playerSpeed)
    {
        //速度(値)の表示
        speedText.text = playerSpeed.ToString("00")+ "Km";

        //速度(量)の表示 
        //最大が1
        float speedAmount = playerSpeed / maxSpeed;

        speedImage.fillAmount = speedAmount;

        //最大値以上の場合
        if (speedImage.fillAmount >= 1)
        {
            if (!isMaxSpeed)
                isMaxSpeed = true;
        }
        else
        {
            if (isMaxSpeed)
                isMaxSpeed = false;
        }
    }
}
