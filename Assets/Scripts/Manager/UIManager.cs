using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

//UIの管理をする
//パネルの表示、非表示、テキストの更新など
public class UIManager : MonoBehaviour
{
    //現在のイベントシステムを取得
    [SerializeField]
    private EventSystem currentEventSystem;

    //SEを流すコンポーネント
    [SerializeField]
    private SoundManager soundManager;

    [SerializeField,Header("タイトルパネル")]
    private GameObject titleScreen;

    [SerializeField, Header("ゲーム説明パネル")]
    private GameObject explanationScreen;

    [SerializeField, Header("メインゲームパネル")]
    private GameObject mainGameScreen;

    [SerializeField, Header("オプションパネル")]
    private GameObject optionScreen;

    [SerializeField, Header("操作説明パネル")]
    private GameObject guideScreen;

    [SerializeField, Header("ゲームオーバーパネル")]
    private GameObject gameOverScreen;

    [SerializeField, Header("ゲームクリアパネル")]
    private GameObject gameClearScreen;

    [SerializeField, Header("タイトルパネルのスタートボタン")]
    private Button titleStartButton;

    [SerializeField, Header("タイトルパネルの終了ボタン")]
    private Button titleFinishButton;

    [SerializeField, Header("メインゲームパネルのオプションボタン")]
    private Button mainGameOptionButton;

    [SerializeField, Header("オプションパネルの操作説明ボタン")]
    private Button optionGuideButton;

    [SerializeField, Header("オプションパネルのチェックポイントに戻るボタン")]
    private Button optionReturnCheckPointButton;

    [SerializeField, Header("オプションパネルのタイトルに戻るボタン")]
    private Button optionReturnTitleButton;

    [SerializeField, Header("オプションパネルのゲームに戻るボタン")]
    private Button optionReturnGameButton;

    [SerializeField, Header("操作説明パネルのオプションに戻るボタン")]
    private Button guideReturnOptionButton;

    [SerializeField, Header("ゲームオーバーパネルのコンティニューボタン")]
    private Button gameOverContinueButton;

    [SerializeField, Header("ゲームオーバーパネルのタイトルに戻るボタン")]
    private Button gameOverReturnTitleButton;

    [SerializeField, Header("ゲームクリアパネルのタイトルに戻るボタン")]
    private Button gameClearReturnTitleButton;

    [SerializeField, Header("スピードテキスト")]
    private TextMeshProUGUI speedText;
    public TextMeshProUGUI GetSpeedText => speedText;

    [SerializeField, Header("スピード画像")]
    private Image speedImage;
    public Image GetSpeedImage => speedImage;

    [SerializeField]
    private SpeedUI speedUI;
    public SpeedUI GetSpeedUI => speedUI;

    //選択されているボタン
    private GameObject selectButton;

    //ゲッター
    public GameObject GetTitleScreen => titleScreen;
    public GameObject GetExplanationScreen => explanationScreen;
    public GameObject GetMainGameScreen => mainGameScreen;
    public GameObject GetOptionScreen => optionScreen;
    public GameObject GetGuideScreen => guideScreen;
    public GameObject GetGameOverScreen => gameOverScreen;
    public GameObject GetGameClearScreen => gameClearScreen;

    public Button GetTitleStartButton => titleStartButton;
    public Button GetMainGameOptionButton => mainGameOptionButton;
    public Button GetOptionGuideButton => optionGuideButton;
    public Button GetOptionReturnTitleButton => optionReturnTitleButton;
    public Button GetOptionReturnCheckPointButton => optionReturnCheckPointButton;
    public Button GetOptionReturnGameButton => optionReturnGameButton;
    public Button GetGameOverReturnTitleButton => gameOverReturnTitleButton;
    public Button GetGameOverContinueButton => gameOverContinueButton;
    public Button GetGameClearReturnTitleButton => gameClearReturnTitleButton;

    private void Start()
    {
        //タイトルパネルの終了ボタンにゲームを終了する関数を登録
        titleFinishButton.onClick.AddListener(() => GameDirector.uniqueInstance.FinishGame());

        //オプションパネルの操作説明ボタンに関数を登録
        optionGuideButton.onClick.AddListener(
            () => {
                ChangePanel(optionScreen, guideScreen);
                soundManager.Play("ボタン");
                currentEventSystem.SetSelectedGameObject(guideReturnOptionButton.gameObject);
                GameDirector.uniqueInstance.ChangeSceneState(SceneState.Guide);
            });

        //操作説明パネルのオプションに戻るボタンに関数を登録
        guideReturnOptionButton.onClick.AddListener(
            () => {
                ChangePanel(guideScreen, optionScreen);
                soundManager.Play("ボタン");
                currentEventSystem.SetSelectedGameObject(optionGuideButton.gameObject);
                GameDirector.uniqueInstance.ChangeSceneState(SceneState.Option);
            });
    }

    private void Update()
    {
        //シーンごとのUIの更新
        switch (GameDirector.uniqueInstance.GetCurrentSceneState)
        {
            //タイトル
            case SceneState.Title:
                //エスケープキーを押下時
                if (Input.GetKeyDown(KeyCode.Escape))
                    //終了ボタンを押す
                    titleFinishButton.onClick.Invoke();
                break;

            //メインゲーム
            case SceneState.MainGame:
                //エスケープキーを押下時
                if (Input.GetKeyDown(KeyCode.Escape))
                    //オプションボタンを押す
                    mainGameOptionButton.onClick.Invoke();
                break;

            //オプション
            case SceneState.Option:
                //常に何かのボタンを選択状態にしておく
                SelectAlwaysAnyButton(ref selectButton);
                //エスケープキーを押下時
                if (Input.GetKeyDown(KeyCode.Escape))
                    //オプションボタンを押す
                    optionReturnGameButton.onClick.Invoke();
                break;

            //操作説明
            case SceneState.Guide:
                //常に何かのボタンを選択状態にしておく
                SelectAlwaysAnyButton(ref selectButton);
                //エスケープキーを押下時
                if (Input.GetKeyDown(KeyCode.Escape))
                    //戻るボタンを押す
                    guideReturnOptionButton.onClick.Invoke();
                break;

            //ゲームオーバー
            case SceneState.GameOver:
                //常に何かのボタンを選択状態にしておく
                SelectAlwaysAnyButton(ref selectButton);
                break;

            //ゲームクリア
            case SceneState.GameClear:
                //常に何かのボタンを選択状態にしておく
                SelectAlwaysAnyButton(ref selectButton);
                break;
        }
    }

    /// <summary>
    /// 常に何かのボタンを選択状態にしておきたい場合に使用する
    /// </summary>
    /// <param name="selectedButton">選択されていたボタン</param>
    private void SelectAlwaysAnyButton(ref GameObject selectedButton)
    {
        //現在選択中のボタンを取得
        GameObject currentButton = currentEventSystem.currentSelectedGameObject;

        Debug.Log($"選択中のボタン:{selectButton}");

        if (currentButton != null)
            selectedButton = currentButton;
        else
            //ボタンが選択されてない場合、前回選択されたボタンを選択状態にする
            if (selectedButton != null)
                currentEventSystem.SetSelectedGameObject(selectedButton);
    }

    /// <summary>
    /// パネルを変更する
    /// </summary>
    /// <param name="currentPanel">非表示にするパネル</param>
    /// <param name="nextPanel">表示するパネル</param>
    public void ChangePanel(GameObject currentPanel, GameObject nextPanel)
    {
        currentPanel.SetActive(false);
        nextPanel.SetActive(true);
    }

    /// <summary>
    /// ボタンを選択状態にする
    /// </summary>
    /// <param name="selectButtonObject">選択状態にするボタン</param>
    public void SetSelectedButton(Button selectedButton)
    {
        //選択状態にするゲームオブジェクト
        GameObject selectedGameObject = selectedButton.gameObject;

        currentEventSystem.SetSelectedGameObject(selectedGameObject);
    }
}