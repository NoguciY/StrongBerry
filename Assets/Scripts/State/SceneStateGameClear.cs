using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager
{
    public class SceneStateGameClear : SceneStateBase
    {
        //�I�����ꂽ�{�^��
        private GameObject selectedButton;

        public override void OnEnter(GameManager gameManager, SceneStateBase preState)
        {
            //�Q�[���N���A��ʂ̕\��
            //gameManager.gameClearScreen.SetActive(true);
            //gameManager.uiManager.GetGameClearScreen.SetActive(true);

            //�Q�[�����~
            //gameManager.PauseGame();

            //gameManager.ClearGame();

            //�R���e�B�j���[�{�^����I����Ԃɂ���
            //gameManager.currentEventSystem.SetSelectedGameObject(gameManager.gameClearContinueButton);
        }

        public override void OnUpdate(GameManager gameManager)
        {
            //�I������Ă���{�^�����擾
            //GameObject selectButton = gameManager.currentEventSystem.currentSelectedGameObject;

            //��ɂǂꂩ�̃{�^���͑I����Ԃɂ���
            //gameManager.SelectAlwaysAnyButton(ref selectedButton);

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                //if (selectedButton == gameManager.gameClearContinueButton)
                //{
                //    gameManager.ContinueGame();

                //    //�`�F�b�N�|�C���g��
                //    gameManager.ChangeState(sceneStateMain);
                //}
                //else if (selectedButton == gameManager.gameClearResetButton)
                //{
                //    gameManager.ResetGame();

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
            //�Q�[���N���A��ʂ̔�\��
            //gameManager.gameClearScreen.SetActive(false);
            //gameManager.uiManager.GetGameClearScreen.SetActive(false);
        }
    }
}