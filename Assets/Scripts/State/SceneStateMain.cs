using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GManager
{
    public class SceneStateMain : SceneStateBase
    {
        //説明が表示されたか
        private bool isDisplayedExplanation = false;

        public override void OnEnter(GManager gameManager, SceneStateBase preState)
        {
            //メイン画面UIの表示
            gameManager.mainGameImages.SetActive(true);

            if (!isDisplayedExplanation)
            {
                //説明画面の表示
                isDisplayedExplanation = true;
                gameManager.StartCoroutine(gameManager.DisplayedExplanationCoroutine());
            }

            //チェックポイントから再開する場合
            //if (preState == sceneStateGameOver || )
            //{

            //}

            //ゲームを再開
            else gameManager.ResumeGame();
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
            //メイン画面UIの非表示
            gameManager.mainGameImages.SetActive(false);
        }
    }
}