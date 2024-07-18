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
    public virtual void OnEnter(GameManager gameManager, SceneStateBase preState){ }


    /// <summary>
    /// �t���[�����ƂɌĂ΂��
    /// </summary>
    /// <param name="gameManager">�V�[���X�e�[�g�̊Ǘ���</param>
    public virtual void OnUpdate(GameManager gameManager){ }

    /// <summary>
    /// �I�����ɌĂ΂��
    /// </summary>
    /// <param name="gameManager">�V�[���X�e�[�g�̊Ǘ���</param>
    /// <param name="nextState">���̃X�e�[�g</param>
    public virtual void OnExit(GameManager gameManager, SceneStateBase nextState){ }
}
