using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneStateBase
{
    /// <summary>
    /// �J�n���ɌĂ΂��
    /// </summary>
    /// <param name="gameManager">�V�[���X�e�[�g�̊Ǘ���</param>
    /// <param name="preState">�O�̃X�e�[�g</param>
    public virtual void OnEnter(GManager gameManager, SceneStateBase preState){ }


    /// <summary>
    /// �t���[�����ƂɌĂ΂��
    /// </summary>
    /// <param name="gameManager">�V�[���X�e�[�g�̊Ǘ���</param>
    public virtual void OnUpdate(GManager gameManager){ }

    /// <summary>
    /// �I�����ɌĂ΂��
    /// </summary>
    /// <param name="gameManager">�V�[���X�e�[�g�̊Ǘ���</param>
    /// <param name="nextState">���̃X�e�[�g</param>
    public virtual void OnExit(GManager gameManager, SceneStateBase nextState){ }
}
