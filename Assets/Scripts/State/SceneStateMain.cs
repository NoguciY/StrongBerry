using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager
{
    public class SceneStateMain : SceneStateBase
    {
        //�������\�����ꂽ��
        private bool isDisplayedExplanation = false;

        public override void OnEnter(GameManager gameManager, SceneStateBase preState)
        {
            //���C�����UI�̕\��
            //gameManager.mainGameImages.SetActive(true);
            //gameManager.uiManager.GetMainGameScreen.SetActive(true);

            if (!isDisplayedExplanation)
            {
                //������ʂ̕\��
                isDisplayedExplanation = true;
                //gameManager.StartCoroutine(gameManager.DisplayedExplanationCoroutine());
            }

            //�`�F�b�N�|�C���g����ĊJ����ꍇ
            //if (preState == sceneStateGameOver || )
            //{

            //}
            
            //�Q�[�����ĊJ
            //else gameManager.ResumeGame();
        }

        public override void OnUpdate(GameManager gameManager)
        {
            //�I�v�V������ʂ�
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //gameManager.ChangeState(sceneStateOption);
            }
        }

        public override void OnExit(GameManager gameManager, SceneStateBase nextState)
        {
            //���C�����UI�̔�\��
            //gameManager.mainGameImages.SetActive(false);
            //gameManager.uiManager.GetMainGameScreen.SetActive(false);
        }
    }
}