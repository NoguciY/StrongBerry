using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�V�[����
//�e�V�[�����L�q����(�V�[�����������ꍇ��������)
public enum SceneState
{
    Title,      //�^�C�g��
    MainGame,   //���C��
    Option,     //�I�v�V����
    Guide,      //�������
    GameOver,   //�Q�[���I�[�o�[
    GameClear,  //�Q�[���N���A
}

//�V���O���g�����g�����N���X
//�V�[���̏�Ԃ��Ǘ�����

public class GameDirector : MonoBehaviour
{
    //���̃N���X�̗B��̃C���X�^���X
    public static GameDirector uniqueInstance;

    //���݂̃V�[�����
    [SerializeField]
    private SceneState currentSceneState;

    //�O�̃V�[�����
    private SceneState preSceneState;

    //�Q�b�^�[
    public SceneState GetCurrentSceneState => currentSceneState;
    public SceneState GetPreSceneState => preSceneState;

    private void Awake()
    {
        if (uniqueInstance == null)
        {
            uniqueInstance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        preSceneState = currentSceneState;

        //60fps��ڕW�ɐݒ�
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        //�V�[����Ԃ��ω������ꍇ
        if (preSceneState != currentSceneState)
            preSceneState = currentSceneState;

        Debug.Log($"���݂̃V�[��{currentSceneState}");
    }

    //���݂̃V�[����Ԃ�ς���
    public void ChangeSceneState(SceneState nextSceneName)
    {
        currentSceneState = nextSceneName;
    }

    //�Q�[�����I������
    public void FinishGame()
    {
        //UnityEditor�Ńv���C���Ă���ꍇ
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

        //�r���h�����Q�[�����v���C�����Ă���ꍇ
#else
        Application.Quit();
#endif
    }
}
