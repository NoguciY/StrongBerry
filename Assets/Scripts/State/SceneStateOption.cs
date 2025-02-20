using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager
{
    public class SceneStateOption : SceneStateBase
    {
        //選択されたボタン
        private GameObject selectedButton;

        public override void OnEnter(GameManager gameManager, SceneStateBase preState)
        {
            //オプション画面の表示
            //gameManager.optionScreen.SetActive(true);
            //gameManager.uiManager.GetOptionScreen.SetActive(true);

            //ゲームを停止
            //gameManager.PauseGame();

            //操作説明ボタンを選択状態にする
            //gameManager.currentEventSystem.SetSelectedGameObject(gameManager.guideButton);
        }

        public override void OnUpdate(GameManager gameManager)
        {
            //選択されているボタンを取得
            //GameObject selectButton = gameManager.currentEventSystem.currentSelectedGameObject;

            //常にどれかのボタンは選択状態にする
            //gameManager.SelectAlwaysAnyButton(ref selectedButton);

            //オプション項目の選択
            //if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            //{
            //    if (selectedButton == gameManager.guideButton)
            //    {
            //        //操作説明画面へ
            //        gameManager.ChangeState(sceneStateGuide);
            //    }
            //    else if (selectedButton == gameManager.returnCheckPointButton)
            //    {
            //        gameManager.ContinueGame();

            //        //チェックポイントへ
            //        gameManager.ChangeState(sceneStateMain);
            //    }
            //    else if (selectedButton == gameManager.resetButton)
            //    {
            //        gameManager.ResetGame();

            //        //スタート画面へ
            //        gameManager.ChangeState(sceneStateStart);
            //    }
                //else if (selectButton == gameManager.returnGameButton)
                //{
                //    //メイン画面へ
                //    gameManager.ChangeState(sceneStateMain);
                //}
            //}

            //メイン画面へ
            //if (Input.GetKeyDown(KeyCode.Escape))
            //{
            //    gameManager.ChangeState(sceneStateMain);
            //}
        }

        public override void OnExit(GameManager gameManager, SceneStateBase nextState)
        {
            //オプション画面の非表示
            //gameManager.optionScreen.SetActive(false);
            //gameManager.uiManager.GetOptionScreen.SetActive(false);
        }
    }
}