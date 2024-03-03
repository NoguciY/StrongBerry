using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GManager
{
    public class SceneStateGuide : SceneStateBase
    {
        public override void OnEnter(GManager gameManager, SceneStateBase preState)
        {
            //���������ʂ̕\��
            gameManager.guideScreen.SetActive(true);
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
            //���������ʂ̔�\��
            gameManager.guideScreen.SetActive(false);
        }
    }
}
