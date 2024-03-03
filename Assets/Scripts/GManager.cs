using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//ゲームのクリアやゲームオーバー
//UIの更新、シーン遷移を行う

public partial class GManager : MonoBehaviour
{
    public enum ButtonType
    {
        Start,      //ゲーム開始
        Finish,     //ゲーム終了
        Continue,   //コンティニュー
        Reset,      //タイトルに戻る
        Option,     //オプション
        Guide,      //操作説明
        Return,     //ゲームに戻る
    }

    //タイトル
    [SerializeField]
    private GameObject titleScreen;

    //ゲームオーバー
    [SerializeField]
    private GameObject gameOverScreen;

    //操作説明
    [SerializeField]
    private GameObject guideScreen;

    //メインゲームUI
    [SerializeField]
    private GameObject mainGameImages;

    //オプション
    [SerializeField]
    private GameObject optionScreen;

    //ゲームクリア
    [SerializeField]
    private GameObject gameClearScreen;

    //ゲーム説明
    [SerializeField]
    private GameObject explanationScreen;

    //コンティニューボタン
    [SerializeField]
    private Button continueButton;

    //スタートボタン
    [SerializeField]
    private Button startButton;

    //リスタートボタン
    [SerializeField]
    private Button restartButton;

    //プレイヤーの管理用
    [SerializeField]
    private PlayerManager playerManager;

    //チェックポイント
    [SerializeField]
    private CheckPoint[] checkPoints;

    //クリア時のエフェクト
    [SerializeField]
    private ParticleSystem clearEffect;

    //スタートボタン音
    [SerializeField]
    private AudioClip startSound;

    //その他のボタン音
    [SerializeField]
    private AudioClip buttonSound;

    //ゲームオーバー音
    [SerializeField]
    private AudioClip gameOverSound;

    //クリア音
    [SerializeField]
    private AudioClip gameClearSound;

    //プレイヤーの初期位置
    private Vector3 playerInitPos;

    //プレイヤーの初期回転値
    private Vector3 playerInitRot;

    //プレイヤーのリスポーン位置
    private Vector3 playerRespawnPos;

    //プレイヤーのリスポーン時の回転値
    private Vector3 playerRespawnRot;

    //クリアしているか
    private bool isClear = false;

    //ゲームが開始されたか
    private bool isStart = false;

    //説明が表示されたか
    //private bool isDisplayedExplanation = false;

    //オーディオソース
    private AudioSource seAudioSource;

    //現在のイベントシステムを取得
    [SerializeField]
    private EventSystem currentEventSystem = EventSystem.current;

    //各ボタン
    [SerializeField]
    private GameObject guideButton;

    [SerializeField]
    private GameObject returnGameButton;

    [SerializeField]
    private GameObject resetButton;

    [SerializeField]
    private GameObject returnCheckPointButton;

    [SerializeField]
    private GameObject gameOverContinueButton;

    [SerializeField]
    private GameObject gameOverResetButton;

    [SerializeField]
    private GameObject gameClearContinueButton;

    [SerializeField]
    private GameObject gameClearResetButton;

    //ステートのインスタンス
    private static readonly SceneStateStart sceneStateStart         = new SceneStateStart();
    private static readonly SceneStateMain sceneStateMain           = new SceneStateMain();
    private static readonly SceneStateOption sceneStateOption       = new SceneStateOption();
    private static readonly SceneStateGuide sceneStateGuide         = new SceneStateGuide();
    private static readonly SceneStateGameOver sceneStateGameOver   = new SceneStateGameOver();
    private static readonly SceneStateGameClear sceneStateGameClear = new SceneStateGameClear();

    //現在のステート
    private SceneStateBase currentState = sceneStateStart;


    /// <summary>
    /// ステートの変更
    /// </summary>
    /// <param name="nextState">次のステート</param>
    private void ChangeState(SceneStateBase nextState)
    {
        currentState.OnExit(this, nextState);
        nextState.OnEnter(this, currentState);
        currentState = nextState;
    }




    private void Awake()
    {
        //ゲームを停止
        PauseGame();
        //カーソルを中央に固定して、非表示
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentState.OnEnter(this, null);
    }

    // Start is called before the first frame update
    private void Start()
    {
        seAudioSource = GetComponent<AudioSource>();

        //初期位置を取得
        playerInitPos = playerManager.GetInitPos;
        //初期回転値を取得
        playerInitRot = playerManager.GetInitRot;
        //リスポーン位置を取得
        playerRespawnPos = playerInitPos;
        //リスポーン回転値を取得
        playerRespawnRot = playerInitRot;
    }

    // Update is called once per frame
    private void Update()
    {
        currentState.OnUpdate(this);

        //ESCキー押下時
        //オプション、終了する、戻る
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    //タイトル画面 クリア画面 ゲームオーバー画面 の場合
        //    if (titleScreen.activeSelf || gameClearScreen.activeSelf ||gameOverScreen.activeSelf)
        //    {
        //        //ゲームを終了する
        //        FinishGame();
        //    }
        //    //メインゲーム画面の場合
        //    else if (mainGameImages.activeSelf)
        //    {
        //        //ゲームを停止
        //        PauseGame();
        //        //ボタン音を再生
        //        PlaySound(buttonSound);
        //        //オプションを開く
        //        OpenScreen(mainGameImages, optionScreen, guideButton);
        //    }
        //    //オプション画面の場合
        //    else if (optionScreen.activeSelf)
        //    {
        //        //ボタン音を再生
        //        PlaySound(startSound);
        //        //ゲームに戻る
        //        ReturnToGame(optionScreen);
        //    }
        //    //操作説明画面の場合
        //    else if (guideScreen.activeSelf)
        //    {
        //        //ボタン音を再生
        //        PlaySound(buttonSound);
        //        //オプションを開く
        //        OpenScreen(guideScreen, optionScreen, guideButton);
        //    }
        //}

        ////ゲーム開始時に１度だけ説明を表示する
        //if (isStart)
        //{
        //    if (!isDisplayedExplanation)
        //    {
        //        isDisplayedExplanation = true;
        //        StartCoroutine(DisplayedExplanationCoroutine());
        //    }
        //}

        //if (!isClear)
        //{
        //    //クリアした場合、クリア画面へ
        //    if (IsClearGame())
        //    {
        //        isClear = true;
        //        GameClear();
        //    }
        //}

        ////クリアエフェクトを再生
        //else { clearEffect.Simulate(Time.unscaledDeltaTime, false, false); }
    }


    //クリア判定
    private bool IsClearGame()
    {
        for (int i = 0; i < checkPoints.Length; i++)
        {
            if (!checkPoints[i].GetPassedCheckPoint)
            {
                return false;
            }
        }
        return true;
    }

    //ゲームクリア(開始時)
    private void GameClear()
    {
        //メインゲームUIを非表示
        HidScreen(mainGameImages);

        //ゲームクリア画面を表示
        DisplayedScreen(gameClearScreen);

        //ゲームを停止
        PauseGame();

        //リスタートボタンを選択
        restartButton.Select();

        //ゲームクリア音再生
        PlaySound(gameClearSound);
    }

    //ゲームオーバー(開始時)
    public void GameOver()
    {
        //メインゲームUIを非表示
        HidScreen(mainGameImages);

        //ゲームオーバー画面を表示
        DisplayedScreen(gameOverScreen);

        //ゲームを停止
        PauseGame();

        //コンティニューボタンを選択
        continueButton.Select();

        //ゲームオーバー音再生
        PlaySound(gameOverSound);
    }

    //ゲームを停止
    private void PauseGame()
    {
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;
        }
    }


    //ゲームを再開
    private void ResumeGame()
    {
        if (Time.timeScale != 1)
        {
            Time.timeScale = 1;
        }
    }

    //ゲームオブジェクトによって指定した画面を表示する
    private void DisplayedScreen(GameObject screen)
    {
        screen.SetActive(true);
    }


    //ボタンによって指定した画面を表示する
    private void DisplayedScreen(ButtonType buttonType)
    {
        switch (buttonType)
        {
            //タイトルに戻る
            case ButtonType.Reset:
                titleScreen.SetActive(true);
                break;
            //操作説明
            case ButtonType.Guide:
                guideScreen.SetActive(true);
                break;
            //オプション
            case ButtonType.Option:
                optionScreen.SetActive(true);
                break;
        }
    }

    //ゲームオブジェクトによって指定した画面を非表示にする
    private void HidScreen(GameObject screen)
    {
        screen.SetActive(false);
    }

    //ボタンによって指定した画面を非表示にする
    private void HidScreen(ButtonType buttonType)
    {
        switch (buttonType)
        {
            //スタート
            case ButtonType.Start:
                titleScreen.SetActive(false);
                break;
            //コンティニュー
            case ButtonType.Continue:
                gameOverScreen.SetActive(false);
                break;
            //ゲームに戻る または 操作説明
            case ButtonType.Return:
            case ButtonType.Option:
                optionScreen.SetActive(false);
                break;
            //タイトルに戻る
            case ButtonType.Reset:
                if (optionScreen.activeSelf)
                {
                    optionScreen.SetActive(false);
                }
                else if (gameOverScreen.activeSelf)
                {
                    gameOverScreen.SetActive(false);
                }
                else gameClearScreen.SetActive(false);
                break;
            case ButtonType.Guide:
                guideScreen.SetActive(false);
                break;
        }
    }

    //画面を開く
    private void OpenScreen(GameObject closedScreen, GameObject openedScreen, Button selectButton = default)
    {
        //現在の画面を非表示
        HidScreen(closedScreen);

        //画面を表示
        DisplayedScreen(openedScreen);

        if (selectButton != null)
        {
            //ボタンを選択
            selectButton.Select();
        }
    }

    

    //AudioClipを元にSEを再生
    private void PlaySound(AudioClip audioClip)
    {
        seAudioSource.clip = audioClip;
        seAudioSource.Play();
    }


    //プレイヤーを再設定する(コンティニュー、タイトルに戻った時)
    private void ResetPlayer(Vector3 resetPos, Vector3 resetRot)
    {
        //プレイヤーの位置と回転値を再設定する
        playerManager.transform.position = resetPos;
        playerManager.transform.eulerAngles = resetRot;
    }
    
    //ゲームスタート時に説明を表示するためのコルーチン
    private IEnumerator DisplayedExplanationCoroutine()
    {
        //説明を表示
        explanationScreen.SetActive(true);

        //待機する
        //ポーズ中のため WaitForSecondsRealtime を使う
        yield return new WaitForSecondsRealtime(3.0f);
        
        //説明を非表示
        explanationScreen.SetActive(false);

        //ゲームを停止する
        ResumeGame();
    }

    //ゲームを開始する
    public void StartGame()
    {   
        //メインゲーム画面を開く
        OpenScreen(titleScreen, mainGameImages);

        //スタート音再生
        PlaySound(startSound);

        if (isStart)
        {
            //タイムスケールを1にする
            ResumeGame();
        }

        //ゲーム開始フラグオン
        isStart = true;
    }


    //操作説明を開く
    public void OpenGuide()
    {
        //オプションを非表示
        HidScreen(ButtonType.Option);

        //操作説明を表示
        DisplayedScreen(ButtonType.Guide);

        //ボタン音を再生
        PlaySound(buttonSound);
    }


    //操作説明を閉じる
    public void CloseGuide() 
    {
        //操作説明を非表示
        HidScreen(ButtonType.Guide);

        //オプションを表示
        DisplayedScreen(ButtonType.Option);

        //ボタン音を再生
        PlaySound(buttonSound);
    }

    //ゲームに戻る,やり直す(コンティニュー)
    public void ReturnToGame(GameObject screen)
    {
        //ゲームオーバー画面 または オプション画面の場合
        if (screen == gameOverScreen || optionScreen)
        {
            //リスポーン地点と回転値を取得
            playerRespawnPos = playerManager.respawnPos;
            playerRespawnRot = playerManager.respawnRot;

            //プレイヤーの動きを止める
            playerManager.StopPlayerMovement();

            //チェックポイントから始める
            ResetPlayer(playerRespawnPos, playerRespawnRot);
        }
        //クリア画面の場合
        else if (screen == gameClearScreen)
        {
            for (int i = 0; i < checkPoints.Length; i++)
            {
                //チェックポイント通過フラグをオフ
                checkPoints[i].SetPassedCheckPointFlag();
            }
            //クリアフラグをオフ
            isClear = false;
            //クリアエフェクトを削除
            clearEffect.Clear();

            //プレイヤーを初期値にリセットする
            ResetPlayer(playerInitPos, playerInitRot);
        }

        //現在の画面を非表示
        screen.SetActive(false);

        //メインゲームUIを表示する
        mainGameImages.SetActive(true);

        //ボタン音を再生
        PlaySound(startSound);

        //タイムスケールを1にする
        ResumeGame();
    }


    /// <summary>
    /// タイトルに戻る
    /// </summary>
    /// <param name="screen"></param>
    /// <retval></retval>
    public void ReturnToTitle(GameObject screen)
    {
        //現在の画面を非表示
        screen.SetActive(false);

        //チェックポイント通過フラグをオフ
        for (int i = 0; i < checkPoints.Length; i++)
        {
            checkPoints[i].SetPassedCheckPointFlag();
        }

        //クリア画面の場合
        if (screen == gameClearScreen)
        {
            //クリアフラグをオフ
            isClear = false;
            //クリアエフェクトを削除
            clearEffect.Clear();
        }

        //プレイヤーの動きを止める
        playerManager.StopPlayerMovement();

        //タイトル画面を表示する
        DisplayedScreen(ButtonType.Reset);

        //プレイヤーを初期値にリセットする
        ResetPlayer(playerInitPos, playerInitRot);

        //ボタン音を再生
        PlaySound(buttonSound);

        //スタートボタンを選択
        startButton.Select();
    }


    //ゲームを終了する
    private void FinishGame()
    {
//UnityEditorでプレイしている場合
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

//ビルドしたゲームをプレイしいている場合
#else
        Application.Quit();
#endif
    }

   
    /// <summary>
    /// 常に何かのボタンを選択状態にしておきたい場合に使用する
    /// </summary>
    /// <param name="selectedButton">選択されていたボタン</param>
    private void SelectAlwaysAnyButton(ref GameObject selectedButton)
    {
        //現在選択中のボタンを取得
        GameObject currentButton = currentEventSystem.currentSelectedGameObject;

        if (currentButton)
        {
            selectedButton = currentButton;
        }
        else
        {
            //ボタンが選択されてない場合、前回選択されたボタンを選択状態にする
            if (selectedButton != null)
            {
                currentEventSystem.SetSelectedGameObject(selectedButton);
            }
        }
    }
}
