using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager
{
    public class SceneStateGuide : SceneStateBase
    {
        public override void OnEnter(GameManager gameManager, SceneStateBase preState)
        {
            //操作説明画面の表示
            //gameManager.guideScreen.SetActive(true);
            //gameManager.uiManager.GetGuideScreen.SetActive(true);
        }

        public override void OnUpdate(GameManager gameManager)
        {
            //オプション画面へ
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //gameManager.ChangeState(sceneStateOption);
            }
        }

        public override void OnExit(GameManager gameManager, SceneStateBase nextState)
        {
            //操作説明画面の非表示
            //gameManager.guideScreen.SetActive(false);
            //gameManager.uiManager.GetGuideScreen.SetActive(false);
        }
    }
}
