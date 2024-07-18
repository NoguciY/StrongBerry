using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager
{
    public class SceneStateGameClear : SceneStateBase
    {
        //選択されたボタン
        private GameObject selectedButton;

        public override void OnEnter(GameManager gameManager, SceneStateBase preState)
        {
            //ゲームクリア画面の表示
            //gameManager.gameClearScreen.SetActive(true);
            //gameManager.uiManager.GetGameClearScreen.SetActive(true);

            //ゲームを停止
            //gameManager.PauseGame();

            //gameManager.ClearGame();

            //コンティニューボタンを選択状態にする
            //gameManager.currentEventSystem.SetSelectedGameObject(gameManager.gameClearContinueButton);
        }

        public override void OnUpdate(GameManager gameManager)
        {
            //選択されているボタンを取得
            //GameObject selectButton = gameManager.currentEventSystem.currentSelectedGameObject;

            //常にどれかのボタンは選択状態にする
            //gameManager.SelectAlwaysAnyButton(ref selectedButton);

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                //if (selectedButton == gameManager.gameClearContinueButton)
                //{
                //    gameManager.ContinueGame();

                //    //チェックポイントへ
                //    gameManager.ChangeState(sceneStateMain);
                //}
                //else if (selectedButton == gameManager.gameClearResetButton)
                //{
                //    gameManager.ResetGame();

                //    //スタート画面へ
                //    gameManager.ChangeState(sceneStateStart);
                //}
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //ゲームの終了
                //gameManager.FinishGame();
            }
        }

        public override void OnExit(GameManager gameManager, SceneStateBase nextState)
        {
            //ゲームクリア画面の非表示
            //gameManager.gameClearScreen.SetActive(false);
            //gameManager.uiManager.GetGameClearScreen.SetActive(false);
        }
    }
}