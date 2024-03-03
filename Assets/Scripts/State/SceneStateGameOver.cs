using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GManager
{
    public class SceneStateGameOver : SceneStateBase
    {
        //選択されたボタン
        private GameObject selectedButton;

        public override void OnEnter(GManager gameManager, SceneStateBase preState)
        {
            //ゲームオーバー画面の表示
            gameManager.gameOverScreen.SetActive(true);

            //ゲームを停止
            gameManager.PauseGame();

            //コンティニューボタンを選択状態にする
            gameManager.currentEventSystem.SetSelectedGameObject(gameManager.gameOverContinueButton);
        }

        public override void OnUpdate(GManager gameManager)
        {
            //選択されているボタンを取得
            //GameObject selectButton = gameManager.currentEventSystem.currentSelectedGameObject;

            //常にどれかのボタンは選択状態にする
            gameManager.SelectAlwaysAnyButton(ref selectedButton);

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                if (selectedButton == gameManager.gameOverContinueButton)
                {
                    //チェックポイントへ
                    gameManager.ChangeState(sceneStateMain);
                }
                else if (selectedButton == gameManager.gameOverResetButton)
                {
                    //スタート画面へ
                    gameManager.ChangeState(sceneStateStart);
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //ゲームの終了
                gameManager.FinishGame();
            }
        }

        public override void OnExit(GManager gameManager, SceneStateBase nextState)
        {
            //ゲームオーバー画面の非表示
            gameManager.gameOverScreen.SetActive(false);
        }
    }
}
