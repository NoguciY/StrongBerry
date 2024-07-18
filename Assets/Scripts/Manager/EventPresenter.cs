using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�C�x���g�Ɋ֐���o�^����
//PlayerManager��UIManager�𒇉��

public class EventPresenter : MonoBehaviour
{
    [SerializeField]
    private PlayerManager playerManager;

    [SerializeField]
    private UIManager uiManager;

    [SerializeField]
    private SoundManager soundManager;

    [SerializeField]
    private ItemManager itemManager;

    private void Start()
    {
        //�Q�[���I�[�o�[�C�x���g
        playerManager.gameOverEvent.AddListener(
            () => {
                //�v���C���[�𑀍�s�ɂ���
                playerManager.IsControllable = false;

                //�p�l����ύX����
                uiManager.ChangePanel(uiManager.GetMainGameScreen, uiManager.GetGameOverScreen);
                
                //���ʉ���炷
                soundManager.Play("�Q�[���I�[�o�[");
                
                //�{�^������ɑI����Ԃɂ���
                uiManager.SetSelectedButton(uiManager.GetGameOverContinueButton);
                
                //�V�[����Ԃ�ύX
                GameDirector.uniqueInstance.ChangeSceneState(SceneState.GameOver);
            });

        //�Q�[���N���A�C�x���g
        playerManager.gameClearEvent.AddListener(
            () => {
                //�v���C���[�𑀍�s�ɂ���
                playerManager.IsControllable = false;

                //�p�l����ύX����
                uiManager.ChangePanel(uiManager.GetMainGameScreen, uiManager.GetGameClearScreen);

                //���ʉ���炷
                soundManager.Play("�Q�[���N���A");

                //�{�^������ɑI����Ԃɂ���
                uiManager.SetSelectedButton(uiManager.GetGameClearReturnTitleButton);

                //�V�[����Ԃ�ύX
                GameDirector.uniqueInstance.ChangeSceneState(SceneState.GameClear);
            });

        //�X�s�[�h�\���C�x���g
        playerManager.GetDisplaySpeedEvent.AddListener(
            (playerSpeed) => {
                uiManager.GetSpeedUI.DisplaySpeed(playerSpeed);
            });

        //�Q�[���X�^�[�g�{�^���C�x���g
        uiManager.GetTitleStartButton.onClick.AddListener(
            () => {
                //���ʉ��𗬂�
                soundManager.Play("�X�^�[�g");

                //�v���C���[�������n�_�ɖ߂�
                playerManager.ResetPlayer(playerManager.GetInitPos, playerManager.GetInitRot);

                //�`�F�b�N�|�C���g������������
                itemManager.Init();

                //�V�[����Ԃ����C���Q�[���ɂ���
                GameDirector.uniqueInstance.ChangeSceneState(SceneState.MainGame);

                //���b��Ƀ|�[�Y���~�߂āA����������\���A���C����ʂ�\������
                StartCoroutine(DisplayedExplanationCoroutine());
            });

        //�I�v�V�����ɑJ�ڂ���{�^���C�x���g
        uiManager.GetMainGameOptionButton.onClick.AddListener(
            () => {
                uiManager.ChangePanel(uiManager.GetMainGameScreen, uiManager.GetOptionScreen);
                
                soundManager.Play("�{�^��");

                //�v���C���[�𑀍�s�ɂ���
                playerManager.IsControllable = false;

                //�|�[�Y����
                Time.timeScale = 0;

                //��������{�^����I����Ԃɂ���
                uiManager.SetSelectedButton(uiManager.GetOptionGuideButton);
                
                GameDirector.uniqueInstance.ChangeSceneState(SceneState.Option);
            });

        //�`�F�b�N�|�C���g�ɖ߂�{�^���C�x���g(�I�v�V�������)
        uiManager.GetOptionReturnCheckPointButton.onClick.AddListener(
            () => {
                uiManager.ChangePanel(uiManager.GetOptionScreen, uiManager.GetMainGameScreen);
                
                soundManager.Play("�{�^��");

                //�v���C���[�����X�|�[���n�_�ɖ߂�
                playerManager.ResetPlayer(playerManager.RespawnPos, playerManager.RespawnRot);

                //�v���C���[�𑀍�\�ɂ���
                playerManager.IsControllable = true;

                //������悤�ɂ���
                Time.timeScale = 1;

                GameDirector.uniqueInstance.ChangeSceneState(SceneState.MainGame);
            });

        //�^�C�g���ɖ߂�{�^���C�x���g(�I�v�V�������)
        uiManager.GetOptionReturnTitleButton.onClick.AddListener(
            () => {
                uiManager.ChangePanel(uiManager.GetOptionScreen, uiManager.GetTitleScreen);
                
                soundManager.Play("�{�^��");
                
                uiManager.SetSelectedButton(uiManager.GetTitleStartButton);

                GameDirector.uniqueInstance.ChangeSceneState(SceneState.Title);
            });

        //�Q�[���ɖ߂�{�^���C�x���g(�I�v�V�������)
        uiManager.GetOptionReturnGameButton.onClick.AddListener(
            () => {
                uiManager.ChangePanel(uiManager.GetOptionScreen, uiManager.GetMainGameScreen);
                
                soundManager.Play("�{�^��");

                //�v���C���[�𑀍�\�ɂ���
                playerManager.IsControllable = true;
                
                //������悤�ɂ���
                Time.timeScale = 1;

                GameDirector.uniqueInstance.ChangeSceneState(SceneState.MainGame);
            });

        //�R���e�B�j���[����{�^���C�x���g(�Q�[���I�[�o�[���)
        uiManager.GetGameOverContinueButton.onClick.AddListener(
            () => {
                //���ʉ���炷
                soundManager.Play("�{�^��");

                //�Q�[���I�[�o�[��ʂ���āA���C����ʂ�\��
                uiManager.ChangePanel(uiManager.GetGameOverScreen, uiManager.GetMainGameScreen);          

                //�v���C���[�����X�|�[���n�_�ɖ߂�
                playerManager.ResetPlayer(playerManager.RespawnPos, playerManager.RespawnRot);

                //�v���C���[�𑀍�\�ɂ���
                playerManager.IsControllable = true;

                //�V�[����Ԃ�ύX
                GameDirector.uniqueInstance.ChangeSceneState(SceneState.MainGame);
            });

        //�^�C�g���ɖ߂�{�^���C�x���g(�Q�[���I�[�o�[���)
        uiManager.GetGameOverReturnTitleButton.onClick.AddListener(
            () => {
                uiManager.ChangePanel(uiManager.GetGameOverScreen, uiManager.GetTitleScreen);
                
                soundManager.Play("�{�^��");

                uiManager.SetSelectedButton(uiManager.GetTitleStartButton);
                
                GameDirector.uniqueInstance.ChangeSceneState(SceneState.Title);
            });

        //�Q�[���N���A�p�l���̃^�C�g���ɖ߂�{�^���Ɋ֐���o�^
        uiManager.GetGameClearReturnTitleButton.onClick.AddListener(
            () => {
                soundManager.Play("�{�^��");

                uiManager.ChangePanel(uiManager.GetGameClearScreen, uiManager.GetTitleScreen);
                
                uiManager.SetSelectedButton(uiManager.GetTitleStartButton);

                //�G�t�F�N�g�������āA�~�߂�
                itemManager.GetGoalPoint.GameClearEffect.Clear();
                itemManager.GetGoalPoint.GameClearEffect.Stop();

                GameDirector.uniqueInstance.ChangeSceneState(SceneState.Title);
            });

        //�X�s�[�hUI�̏�����
        //�����ł��̂͂��������C�����邯�ǁA���Ƀv���C���[��UI���Q�Ƃ����ꏊ���Ȃ�
        //�ʁX�ɂ���ׂ���
        uiManager.GetSpeedUI.Init(uiManager.GetSpeedText, uiManager.GetSpeedImage, playerManager.GetMaxMovingSpeed);
    }

    //�Q�[��������ʂ�\�����Đ��b��ɔ�\���ɂ���R���[�`��
    public IEnumerator DisplayedExplanationCoroutine()
    {
        //�^�C�g�����\���A���������\������
        uiManager.ChangePanel(uiManager.GetTitleScreen, uiManager.GetExplanationScreen);

        float waitingTime = 3.0f;
        
        //�ҋ@����
        //�|�[�Y���̂��� WaitForSecondsRealtime ���g��
        yield return new WaitForSecondsRealtime(waitingTime);

        //����������\���A���C����ʂ�\������
        uiManager.ChangePanel(uiManager.GetExplanationScreen, uiManager.GetMainGameScreen);

        //�v���C���[�𑀍�\�ɂ���
        playerManager.IsControllable = true;

        Time.timeScale = 1;
    }
}
