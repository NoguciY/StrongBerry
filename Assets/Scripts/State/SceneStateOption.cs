using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GManager
{
    public class SceneStateOption : SceneStateBase
    {
        //�I�����ꂽ�{�^��
        private GameObject selectedButton;

        public override void OnEnter(GManager gameManager, SceneStateBase preState)
        {
            //�I�v�V������ʂ̕\��
            gameManager.optionScreen.SetActive(true);

            //�Q�[�����~
            gameManager.PauseGame();

            //��������{�^����I����Ԃɂ���
            gameManager.currentEventSystem.SetSelectedGameObject(gameManager.guideButton);
        }

        public override void OnUpdate(GManager gameManager)
        {
            //�I������Ă���{�^�����擾
            //GameObject selectButton = gameManager.currentEventSystem.currentSelectedGameObject;

            //��ɂǂꂩ�̃{�^���͑I����Ԃɂ���
            gameManager.SelectAlwaysAnyButton(ref selectedButton);

            //�I�v�V�������ڂ̑I��
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                if (selectedButton == gameManager.guideButton)
                {
                    //���������ʂ�
                    gameManager.ChangeState(sceneStateGuide);
                }
                else if (selectedButton == gameManager.returnCheckPointButton)
                {
                    //�`�F�b�N�|�C���g��
                    gameManager.ChangeState(sceneStateMain);
                }
                else if (selectedButton == gameManager.resetButton)
                {
                    //�X�^�[�g��ʂ�
                    gameManager.ChangeState(sceneStateStart);
                }
                //else if (selectButton == gameManager.returnGameButton)
                //{
                //    //���C����ʂ�
                //    gameManager.ChangeState(sceneStateMain);
                //}
            }

            //���C����ʂ�
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gameManager.ChangeState(sceneStateMain);
            }
        }

        public override void OnExit(GManager gameManager, SceneStateBase nextState)
        {
            //�I�v�V������ʂ̔�\��
            gameManager.optionScreen.SetActive(false);
        }
    }
}