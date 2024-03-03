using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�`�F�b�N�|�C���g��ʂ�ƃv���C���[�̃��X�|�[���n�_���X�V�����

public class CheckPoint : MonoBehaviour, IItemController
{
    //�G�t�F�N�g
    [SerializeField]
    private ParticleSystem checkPointEffect;

    //�`�F�b�N�|�C���g�̈ʒu
    private Vector3 checkPointPos;

    //�`�F�b�N�|�C���g�̉�]�l
    private Vector3 checkPointRot;

    //�`�F�b�N�|�C���g�ʉ߃t���O
    private bool passedCheckPoint;

    //�I�[�f�B�I�\�[�X
    private AudioSource checkPointAudioSource;

    //�Q�b�^�[
    public bool GetPassedCheckPoint => passedCheckPoint;


    // Start is called before the first frame update
    private void Start()
    {
        checkPointPos = this.transform.position;
        checkPointRot = this.transform.localEulerAngles;

        checkPointAudioSource = GetComponent<AudioSource>();
    }


    //�`�F�b�N�|�C���g��ʉ߂����Ƃ��̏���(��x����)
    public void PassedCheckPoint(ref Vector3 respawnPos, ref Vector3 respawnRot, Vector3 playerPos)
    {
        if (!passedCheckPoint)
        {
            //�`�F�b�N�|�C���g�ʉ߃t���O�I��
            passedCheckPoint = true;

            if (checkPointEffect != null)
            {
                //�G�t�F�N�g�̍Đ��ʒu�̎w��
                checkPointEffect.transform.position = playerPos;
                //�G�t�F�N�g���Đ�
                checkPointEffect.Play();
            }

            //���ʉ��̍Đ�
            checkPointAudioSource.Play();

            //�`�F�b�N�|�C���g�����X�|�[���n�_�ɂ���
            respawnPos = checkPointPos;

            //�`�F�b�N�|�C���g�������Ă�����������X�|�[����]�l�ɂ���
            respawnRot = checkPointRot;
        }
    }

    //�`�F�b�N�|�C���g�ʉ߃t���O��false�ɂ���
    public void SetPassedCheckPointFlag()
    {
        passedCheckPoint = false;
    }

    //�`�F�b�N�|�C���g��ʉ߂����Ƃ��̏���(�C���^�[�t�F�[�X��)
    public void ExcuteItemAbility(PlayerManager mPlayerManager)
    {
        if (!passedCheckPoint)
        {
            //�`�F�b�N�|�C���g�ʉ߃t���O�I��
            passedCheckPoint = true;

            if (checkPointEffect != null)
            {
                //�G�t�F�N�g�̍Đ��ʒu�̎w��
                checkPointEffect.transform.position = mPlayerManager.GetPos;
                //�G�t�F�N�g���Đ�
                checkPointEffect.Play();
            }

            //���ʉ��̍Đ�
            checkPointAudioSource.Play();

            //�`�F�b�N�|�C���g�����X�|�[���n�_�ɂ���
            mPlayerManager.respawnPos = checkPointPos;

            //�`�F�b�N�|�C���g�������Ă�����������X�|�[����]�l�ɂ���
            mPlayerManager.respawnRot = checkPointRot;
        }
    }
}
