using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public class GoalPoint : MonoBehaviour, IItemController
{
    //�G�t�F�N�g
    public ParticleSystem GameClearEffect;

    public void ExcuteItemAbility(PlayerManager playerManager)
    {
        Debug.Log("�S�[���I");

        //�S�[�������ꍇ�A�N���A�C�x���g���J�n
        playerManager.gameClearEvent.Invoke();

        if (GameClearEffect != null)
            //�G�t�F�N�g���Đ�
            GameClearEffect.Play();
    }
}
