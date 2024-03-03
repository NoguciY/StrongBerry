using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GManager
{
    public class SceneStateStart : SceneStateBase
    {
        //�I�����ꂽ�{�^��
        private GameObject selectedButton;

        public override void OnEnter(GManager gameManager, SceneStateBase preState)
        {
            gameManager.PauseGame();

            //�X�^�[�g��ʂ̕\��
            gameManager.titleScreen.SetActive(true);
        }

        public override void OnUpdate(GManager gameManager)
        {
            //��ɂǂꂩ�̃{�^���͑I����Ԃɂ���
            gameManager.SelectAlwaysAnyButton(ref selectedButton);

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                //SE�Đ�
                gameManager.PlaySound(gameManager.startSound);

                //�Q�[���̊J�n
                gameManager.ChangeState(sceneStateMain);
            }

            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //�Q�[���̏I��
                gameManager.FinishGame();
            }
        }

        public override void OnExit(GManager gameManager, SceneStateBase nextState)
        {
            //�X�^�[�g��ʂ̔�\��
            gameManager.titleScreen.SetActive(false);;
        }
    }
}
