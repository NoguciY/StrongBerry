using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//イベントに関数を登録する
//PlayerManagerとUIManagerを仲介する

public class EventPresenter : MonoBehaviour
{
    [SerializeField]
    private PlayerManager playerManager;

    [SerializeField]
    private UIManager uiManager;

    [SerializeField]
    private SoundManager soundManager;

    [SerializeField]
    private ItemManager itemManager;

    private void Start()
    {
        //ゲームオーバーイベント
        playerManager.gameOverEvent.AddListener(
            () => {
                //プレイヤーを操作不可にする
                playerManager.IsControllable = false;

                //パネルを変更する
                uiManager.ChangePanel(uiManager.GetMainGameScreen, uiManager.GetGameOverScreen);
                
                //効果音を鳴らす
                soundManager.Play("ゲームオーバー");
                
                //ボタンを常に選択状態にする
                uiManager.SetSelectedButton(uiManager.GetGameOverContinueButton);
                
                //シーン状態を変更
                GameDirector.uniqueInstance.ChangeSceneState(SceneState.GameOver);
            });

        //ゲームクリアイベント
        playerManager.gameClearEvent.AddListener(
            () => {
                //プレイヤーを操作不可にする
                playerManager.IsControllable = false;

                //パネルを変更する
                uiManager.ChangePanel(uiManager.GetMainGameScreen, uiManager.GetGameClearScreen);

                //効果音を鳴らす
                soundManager.Play("ゲームクリア");

                //ボタンを常に選択状態にする
                uiManager.SetSelectedButton(uiManager.GetGameClearReturnTitleButton);

                //シーン状態を変更
                GameDirector.uniqueInstance.ChangeSceneState(SceneState.GameClear);
            });

        //スピード表示イベント
        playerManager.GetDisplaySpeedEvent.AddListener(
            (playerSpeed) => {
                uiManager.GetSpeedUI.DisplaySpeed(playerSpeed);
            });

        //ゲームスタートボタンイベント
        uiManager.GetTitleStartButton.onClick.AddListener(
            () => {
                //効果音を流す
                soundManager.Play("スタート");

                //プレイヤーを初期地点に戻す
                playerManager.ResetPlayer(playerManager.GetInitPos, playerManager.GetInitRot);

                //チェックポイントを初期化する
                itemManager.Init();

                //シーン状態をメインゲームにする
                GameDirector.uniqueInstance.ChangeSceneState(SceneState.MainGame);

                //数秒後にポーズを止めて、操作説明を非表示、メイン画面を表示する
                StartCoroutine(DisplayedExplanationCoroutine());
            });

        //オプションに遷移するボタンイベント
        uiManager.GetMainGameOptionButton.onClick.AddListener(
            () => {
                uiManager.ChangePanel(uiManager.GetMainGameScreen, uiManager.GetOptionScreen);
                
                soundManager.Play("ボタン");

                //プレイヤーを操作不可にする
                playerManager.IsControllable = false;

                //ポーズする
                Time.timeScale = 0;

                //操作説明ボタンを選択状態にする
                uiManager.SetSelectedButton(uiManager.GetOptionGuideButton);
                
                GameDirector.uniqueInstance.ChangeSceneState(SceneState.Option);
            });

        //チェックポイントに戻るボタンイベント(オプション画面)
        uiManager.GetOptionReturnCheckPointButton.onClick.AddListener(
            () => {
                uiManager.ChangePanel(uiManager.GetOptionScreen, uiManager.GetMainGameScreen);
                
                soundManager.Play("ボタン");

                //プレイヤーをリスポーン地点に戻す
                playerManager.ResetPlayer(playerManager.RespawnPos, playerManager.RespawnRot);

                //プレイヤーを操作可能にする
                playerManager.IsControllable = true;

                //動けるようにする
                Time.timeScale = 1;

                GameDirector.uniqueInstance.ChangeSceneState(SceneState.MainGame);
            });

        //タイトルに戻るボタンイベント(オプション画面)
        uiManager.GetOptionReturnTitleButton.onClick.AddListener(
            () => {
                uiManager.ChangePanel(uiManager.GetOptionScreen, uiManager.GetTitleScreen);
                
                soundManager.Play("ボタン");
                
                uiManager.SetSelectedButton(uiManager.GetTitleStartButton);

                GameDirector.uniqueInstance.ChangeSceneState(SceneState.Title);
            });

        //ゲームに戻るボタンイベント(オプション画面)
        uiManager.GetOptionReturnGameButton.onClick.AddListener(
            () => {
                uiManager.ChangePanel(uiManager.GetOptionScreen, uiManager.GetMainGameScreen);
                
                soundManager.Play("ボタン");

                //プレイヤーを操作可能にする
                playerManager.IsControllable = true;
                
                //動けるようにする
                Time.timeScale = 1;

                GameDirector.uniqueInstance.ChangeSceneState(SceneState.MainGame);
            });

        //コンティニューするボタンイベント(ゲームオーバー画面)
        uiManager.GetGameOverContinueButton.onClick.AddListener(
            () => {
                //効果音を鳴らす
                soundManager.Play("ボタン");

                //ゲームオーバー画面を閉じて、メイン画面を表示
                uiManager.ChangePanel(uiManager.GetGameOverScreen, uiManager.GetMainGameScreen);          

                //プレイヤーをリスポーン地点に戻す
                playerManager.ResetPlayer(playerManager.RespawnPos, playerManager.RespawnRot);

                //プレイヤーを操作可能にする
                playerManager.IsControllable = true;

                //シーン状態を変更
                GameDirector.uniqueInstance.ChangeSceneState(SceneState.MainGame);
            });

        //タイトルに戻るボタンイベント(ゲームオーバー画面)
        uiManager.GetGameOverReturnTitleButton.onClick.AddListener(
            () => {
                uiManager.ChangePanel(uiManager.GetGameOverScreen, uiManager.GetTitleScreen);
                
                soundManager.Play("ボタン");

                uiManager.SetSelectedButton(uiManager.GetTitleStartButton);
                
                GameDirector.uniqueInstance.ChangeSceneState(SceneState.Title);
            });

        //ゲームクリアパネルのタイトルに戻るボタンに関数を登録
        uiManager.GetGameClearReturnTitleButton.onClick.AddListener(
            () => {
                soundManager.Play("ボタン");

                uiManager.ChangePanel(uiManager.GetGameClearScreen, uiManager.GetTitleScreen);
                
                uiManager.SetSelectedButton(uiManager.GetTitleStartButton);

                //エフェクトを消して、止める
                itemManager.GetGoalPoint.GameClearEffect.Clear();
                itemManager.GetGoalPoint.GameClearEffect.Stop();

                GameDirector.uniqueInstance.ChangeSceneState(SceneState.Title);
            });

        //スピードUIの初期化
        //ここでやるのはおかしい気がするけど、他にプレイヤーとUIを参照した場所がない
        //別々にするべきか
        uiManager.GetSpeedUI.Init(uiManager.GetSpeedText, uiManager.GetSpeedImage, playerManager.GetMaxMovingSpeed);
    }

    //ゲーム説明画面を表示して数秒後に非表示にするコルーチン
    public IEnumerator DisplayedExplanationCoroutine()
    {
        //タイトルを非表示、操作説明を表示する
        uiManager.ChangePanel(uiManager.GetTitleScreen, uiManager.GetExplanationScreen);

        float waitingTime = 3.0f;
        
        //待機する
        //ポーズ中のため WaitForSecondsRealtime を使う
        yield return new WaitForSecondsRealtime(waitingTime);

        //操作説明を非表示、メイン画面を表示する
        uiManager.ChangePanel(uiManager.GetExplanationScreen, uiManager.GetMainGameScreen);

        //プレイヤーを操作可能にする
        playerManager.IsControllable = true;

        Time.timeScale = 1;
    }
}
