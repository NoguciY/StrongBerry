using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

//���݂̃v���C���[�̑�����\������UI

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
            //1�b�ŐԐF�ɕω������̐F�ɖ߂�̂������ƌJ��Ԃ�
            .Join(speedImage.DOColor(Color.red, 1f)
                        .SetLoops(-1, LoopType.Yoyo))
            
            .Join(transform.DOScale(0.1f, 1f)
                        .SetRelative(true)
                        .SetEase(Ease.OutQuart)
                        .SetLoops(-1, LoopType.Restart));
    }

    /// <summary>
    /// �v���C���[�̑�����\������
    /// </summary>
    /// <param name="playerSpeed">�v���C���[�̑���</param>
    public void DisplaySpeed(float playerSpeed)
    {
        //���x(�l)�̕\��
        speedText.text = playerSpeed.ToString("00")+ "Km";

        //���x(��)�̕\�� 
        //�ő傪1
        float speedAmount = playerSpeed / maxSpeed;

        speedImage.fillAmount = speedAmount;

        //�ő�l�ȏ�̏ꍇ
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
