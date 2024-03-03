using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GManager
{
    public class SceneStateGuide : SceneStateBase
    {
        public override void OnEnter(GManager gameManager, SceneStateBase preState)
        {
            //操作説明画面の表示
            gameManager.guideScreen.SetActive(true);
        }

        public override void OnUpdate(GManager gameManager)
        {
            //オプション画面へ
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gameManager.ChangeState(sceneStateOption);
            }
        }

        public override void OnExit(GManager gameManager, SceneStateBase nextState)
        {
            //操作説明画面の非表示
            gameManager.guideScreen.SetActive(false);
        }
    }
}
