using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//シーン名
//各シーンを記述する(シーンが増えた場合書き足す)
public enum SceneState
{
    Title,      //タイトル
    MainGame,   //メイン
    Option,     //オプション
    Guide,      //操作説明
    GameOver,   //ゲームオーバー
    GameClear,  //ゲームクリア
}

//シングルトンを使ったクラス
//シーンの状態を管理する

public class GameDirector : MonoBehaviour
{
    //このクラスの唯一のインスタンス
    public static GameDirector uniqueInstance;

    //現在のシーン状態
    [SerializeField]
    private SceneState currentSceneState;

    //前のシーン状態
    private SceneState preSceneState;

    //ゲッター
    public SceneState GetCurrentSceneState => currentSceneState;
    public SceneState GetPreSceneState => preSceneState;

    private void Awake()
    {
        if (uniqueInstance == null)
        {
            uniqueInstance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        preSceneState = currentSceneState;

        //60fpsを目標に設定
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        //シーン状態が変化した場合
        if (preSceneState != currentSceneState)
            preSceneState = currentSceneState;

        Debug.Log($"現在のシーン{currentSceneState}");
    }

    //現在のシーン状態を変える
    public void ChangeSceneState(SceneState nextSceneName)
    {
        currentSceneState = nextSceneName;
    }

    //ゲームを終了する
    public void FinishGame()
    {
        //UnityEditorでプレイしている場合
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

        //ビルドしたゲームをプレイしいている場合
#else
        Application.Quit();
#endif
    }
}
