using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�`�F�b�N�|�C���g�A�R�C�����Ǘ�����
public class ItemManager : MonoBehaviour
{
    [SerializeField]
    private CheckPoint[] checkPoints;

    [SerializeField]
    private GoalPoint goalPoint;

    //�G�t�F�N�g
    [SerializeField]
    private ParticleSystem[] effects;

    //�T�E���h�}�l�[�W���[
    [SerializeField]
    SoundManager soundManager;

    //�Q�b�^�[
    public GoalPoint GetGoalPoint => goalPoint;

    //�G�t�F�N�g�^�C�v
    private enum EffectType
    { 
        CheckPoint,     //�`�F�b�N�|�C��
        GameClear,      //�Q�[���N���A
    }


    private void Awake()
    {
        foreach (CheckPoint checkPoint in checkPoints)
        {
            //�`�F�b�N�|�C���g�̏�����
            checkPoint.Init(soundManager, effects[(int)EffectType.CheckPoint]);
        }

        //�S�[���|�C���g�̏�����
        goalPoint.GameClearEffect = effects[(int)EffectType.GameClear];
    }

    //�A�C�e���̏�����
    public void Init()
    {
        //�S�Ẵ`�F�b�N�|�C���g�̃t���O��������
        for(int i = 0; i < checkPoints.Length; i++)
        {
            checkPoints[i].PassedCheckPoint = false;
        }
    }
}
