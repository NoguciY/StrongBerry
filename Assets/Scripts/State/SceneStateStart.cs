using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GManager
{
    public class SceneStateStart : SceneStateBase
    {
        //選択されたボタン
        private GameObject selectedButton;

        public override void OnEnter(GManager gameManager, SceneStateBase preState)
        {
            gameManager.PauseGame();

            //スタート画面の表示
            gameManager.titleScreen.SetActive(true);
        }

        public override void OnUpdate(GManager gameManager)
        {
            //常にどれかのボタンは選択状態にする
            gameManager.SelectAlwaysAnyButton(ref selectedButton);

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                //SE再生
                gameManager.PlaySound(gameManager.startSound);

                //ゲームの開始
                gameManager.ChangeState(sceneStateMain);
            }

            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //ゲームの終了
                gameManager.FinishGame();
            }
        }

        public override void OnExit(GManager gameManager, SceneStateBase nextState)
        {
            //スタート画面の非表示
            gameManager.titleScreen.SetActive(false);;
        }
    }
}
