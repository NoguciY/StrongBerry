using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GManager
{
    public class SceneStateMain : SceneStateBase
    {
        //�������\�����ꂽ��
        private bool isDisplayedExplanation = false;

        public override void OnEnter(GManager gameManager, SceneStateBase preState)
        {
            //���C�����UI�̕\��
            gameManager.mainGameImages.SetActive(true);

            if (!isDisplayedExplanation)
            {
                //������ʂ̕\��
                isDisplayedExplanation = true;
                gameManager.StartCoroutine(gameManager.DisplayedExplanationCoroutine());
            }

            //�`�F�b�N�|�C���g����ĊJ����ꍇ
            //if (preState == sceneStateGameOver || )
            //{

            //}

            //�Q�[�����ĊJ
            else gameManager.ResumeGame();
        }

        public override void OnUpdate(GManager gameManager)
        {
            //�I�v�V������ʂ�
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gameManager.ChangeState(sceneStateOption);
            }
        }

        public override void OnExit(GManager gameManager, SceneStateBase nextState)
        {
            //���C�����UI�̔�\��
            gameManager.mainGameImages.SetActive(false);
        }
    }
}