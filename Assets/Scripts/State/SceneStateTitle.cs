using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager
{
    public class SceneStateTitle : SceneStateBase
    {
        //選択されたボタン
        private GameObject selectedButton;

        public override void OnEnter(GameManager gameManager, SceneStateBase preState)
        {
            //gameManager.PauseGame();

            //スタート画面の表示
            //gameManager.titleScreen.SetActive(true);
            //gameManager.uiManager.GetTitleScreen.SetActive(true);
        }

        public override void OnUpdate(GameManager gameManager)
        {
            //常にどれかのボタンは選択状態にする
            //gameManager.SelectAlwaysAnyButton(ref selectedButton);

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                //SE再生
                //gameManager.PlaySound(gameManager.startSound);

                //ゲームの開始
                //gameManager.ChangeState(sceneStateMain);
            }

            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //ゲームの終了
                //gameManager.FinishGame();
            }
        }

        public override void OnExit(GameManager gameManager, SceneStateBase nextState)
        {
            //スタート画面の非表示
            //gameManager.titleScreen.SetActive(false);
            //gameManager.uiManager.GetTitleScreen.SetActive(false);
            //gameManager.GetUIManager.GetTitleScreen.SetActive(false);
        }
    }
}
