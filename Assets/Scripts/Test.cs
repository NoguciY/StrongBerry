using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    //�Q�[���̃V�[���Ǘ�������


}


/// <summary>
/// State�̒��ۃN���X
/// </summary>
public abstract class State
{
    /// <summary>
    /// �X�e�[�g�J�n���ɌĂ΂��
    /// </summary>
    /// <param name="gameManager">�X�e�[�g�̃I�[�i�[</param>
    /// <param name="preState">�O�̃X�e�[�g</param>
    public virtual void OnEnter(GManager gameManager, State preState) { }

    /// <summary>
    /// ���t���[���Ă΂��
    /// </summary>
    /// <param name="gameManager">�X�e�[�g�̃I�[�i�[</param>
    public virtual void OnUpdate(GManager gameManager) { }

    /// <summary>
    /// �X�e�[�g�I�����ɌĂ΂��
    /// </summary>
    /// <param name="gameManager">�X�e�[�g�̃I�[�i�[</param>
    /// <param name="nextState">���̃X�e�[�g</param>
    public virtual void OnExit(GManager gameManager, State nextState) { }
}

