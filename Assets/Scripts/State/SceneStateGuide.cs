using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager
{
    public class SceneStateGuide : SceneStateBase
    {
        public override void OnEnter(GameManager gameManager, SceneStateBase preState)
        {
            //���������ʂ̕\��
            //gameManager.guideScreen.SetActive(true);
            //gameManager.uiManager.GetGuideScreen.SetActive(true);
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
            //���������ʂ̔�\��
            //gameManager.guideScreen.SetActive(false);
            //gameManager.uiManager.GetGuideScreen.SetActive(false);
        }
    }
}
