using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager
{
    public class SceneStateTitle : SceneStateBase
    {
        //�I�����ꂽ�{�^��
        private GameObject selectedButton;

        public override void OnEnter(GameManager gameManager, SceneStateBase preState)
        {
            //gameManager.PauseGame();

            //�X�^�[�g��ʂ̕\��
            //gameManager.titleScreen.SetActive(true);
            //gameManager.uiManager.GetTitleScreen.SetActive(true);
        }

        public override void OnUpdate(GameManager gameManager)
        {
            //��ɂǂꂩ�̃{�^���͑I����Ԃɂ���
            //gameManager.SelectAlwaysAnyButton(ref selectedButton);

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                //SE�Đ�
                //gameManager.PlaySound(gameManager.startSound);

                //�Q�[���̊J�n
                //gameManager.ChangeState(sceneStateMain);
            }

            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //�Q�[���̏I��
                //gameManager.FinishGame();
            }
        }

        public override void OnExit(GameManager gameManager, SceneStateBase nextState)
        {
            //�X�^�[�g��ʂ̔�\��
            //gameManager.titleScreen.SetActive(false);
            //gameManager.uiManager.GetTitleScreen.SetActive(false);
            //gameManager.GetUIManager.GetTitleScreen.SetActive(false);
        }
    }
}
