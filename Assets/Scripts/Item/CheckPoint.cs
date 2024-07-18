using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�`�F�b�N�|�C���g��ʂ�ƃv���C���[�̃��X�|�[���n�_���X�V�����

public class CheckPoint : MonoBehaviour, IItemController
{
    //�G�t�F�N�g
    private ParticleSystem checkPointEffect;

    //�`�F�b�N�|�C���g�̈ʒu
    private Vector3 checkPointPos;

    //�`�F�b�N�|�C���g�̉�]�l
    private Vector3 checkPointRot;

    //�`�F�b�N�|�C���g�ʉ߃t���O
    private bool passedCheckPoint;

    private SoundManager soundManager;

    //�Q�b�^�[
    public bool PassedCheckPoint
    {
        get { return passedCheckPoint; }
        set { 
            passedCheckPoint = value;
            Debug.Log($"�`�F�b�N�|�C���g�ʉ߁F{passedCheckPoint}");
        }
    }

    //������
    public void Init(SoundManager soundManager, ParticleSystem checkPointEffect)
    {
        passedCheckPoint = false;
        checkPointPos = this.transform.position;
        checkPointRot = this.transform.localEulerAngles;
        this.soundManager = soundManager;
        this.checkPointEffect = checkPointEffect;
    }

    //�`�F�b�N�|�C���g��ʉ߂����Ƃ��̏���
    public void ExcuteItemAbility(PlayerManager playerManager)
    {
        if (!passedCheckPoint)
        {
            //�`�F�b�N�|�C���g�ʉ߃t���O�I��
            passedCheckPoint = true;

            if (checkPointEffect != null)
            {
                //�G�t�F�N�g�̍Đ��ʒu�̎w��
                checkPointEffect.transform.position = playerManager.transform.position;
                //�G�t�F�N�g���Đ�
                checkPointEffect.Play();
            }

            //���ʉ��̍Đ�
            soundManager.Play("�`�F�b�N�|�C���g");

            //�`�F�b�N�|�C���g�����X�|�[���n�_�ɂ���
            playerManager.RespawnPos = checkPointPos;

            //�`�F�b�N�|�C���g�������Ă�����������X�|�[����]�l�ɂ���
            playerManager.RespawnRot = checkPointRot;
        }
    }
}
