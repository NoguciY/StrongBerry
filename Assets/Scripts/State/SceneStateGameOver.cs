using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager
{
    public class SceneStateGameOver : SceneStateBase
    {
        //�I�����ꂽ�{�^��
        private GameObject selectedButton;

        public override void OnEnter(GameManager gameManager, SceneStateBase preState)
        {
            //�Q�[���I�[�o�[��ʂ̕\��
            //gameManager.gameOverScreen.SetActive(true);
            //gameManager.uiManager.GetGameOverScreen.SetActive(true);

            //�Q�[�����~
            //gameManager.PauseGame();

            //�R���e�B�j���[�{�^����I����Ԃɂ���
            //gameManager.currentEventSystem.SetSelectedGameObject(gameManager.gameOverContinueButton);
        }

        public override void OnUpdate(GameManager gameManager)
        {
            //�I������Ă���{�^�����擾
            //GameObject selectButton = gameManager.currentEventSystem.currentSelectedGameObject;

            //��ɂǂꂩ�̃{�^���͑I����Ԃɂ���
            //gameManager.SelectAlwaysAnyButton(ref selectedButton);

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                //if (selectedButton == gameManager.gameOverContinueButton)
                //{
                //    //�`�F�b�N�|�C���g��
                //    gameManager.ChangeState(sceneStateMain);
                //}
                //else if (selectedButton == gameManager.gameOverResetButton)
                //{
                //    //�X�^�[�g��ʂ�
                //    gameManager.ChangeState(sceneStateStart);
                //}
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //�Q�[���̏I��
                //gameManager.FinishGame();
            }
        }

        public override void OnExit(GameManager gameManager, SceneStateBase nextState)
        {
            //�Q�[���I�[�o�[��ʂ̔�\��
            //gameManager.gameOverScreen.SetActive(false);
            //gameManager.uiManager.GetGameOverScreen.SetActive(false);
        }
    }
}
