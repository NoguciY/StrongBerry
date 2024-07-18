using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

//UI�̊Ǘ�������
//�p�l���̕\���A��\���A�e�L�X�g�̍X�V�Ȃ�
public class UIManager : MonoBehaviour
{
    //���݂̃C�x���g�V�X�e�����擾
    [SerializeField]
    private EventSystem currentEventSystem;

    //SE�𗬂��R���|�[�l���g
    [SerializeField]
    private SoundManager soundManager;

    [SerializeField,Header("�^�C�g���p�l��")]
    private GameObject titleScreen;

    [SerializeField, Header("�Q�[�������p�l��")]
    private GameObject explanationScreen;

    [SerializeField, Header("���C���Q�[���p�l��")]
    private GameObject mainGameScreen;

    [SerializeField, Header("�I�v�V�����p�l��")]
    private GameObject optionScreen;

    [SerializeField, Header("��������p�l��")]
    private GameObject guideScreen;

    [SerializeField, Header("�Q�[���I�[�o�[�p�l��")]
    private GameObject gameOverScreen;

    [SerializeField, Header("�Q�[���N���A�p�l��")]
    private GameObject gameClearScreen;

    [SerializeField, Header("�^�C�g���p�l���̃X�^�[�g�{�^��")]
    private Button titleStartButton;

    [SerializeField, Header("�^�C�g���p�l���̏I���{�^��")]
    private Button titleFinishButton;

    [SerializeField, Header("���C���Q�[���p�l���̃I�v�V�����{�^��")]
    private Button mainGameOptionButton;

    [SerializeField, Header("�I�v�V�����p�l���̑�������{�^��")]
    private Button optionGuideButton;

    [SerializeField, Header("�I�v�V�����p�l���̃`�F�b�N�|�C���g�ɖ߂�{�^��")]
    private Button optionReturnCheckPointButton;

    [SerializeField, Header("�I�v�V�����p�l���̃^�C�g���ɖ߂�{�^��")]
    private Button optionReturnTitleButton;

    [SerializeField, Header("�I�v�V�����p�l���̃Q�[���ɖ߂�{�^��")]
    private Button optionReturnGameButton;

    [SerializeField, Header("��������p�l���̃I�v�V�����ɖ߂�{�^��")]
    private Button guideReturnOptionButton;

    [SerializeField, Header("�Q�[���I�[�o�[�p�l���̃R���e�B�j���[�{�^��")]
    private Button gameOverContinueButton;

    [SerializeField, Header("�Q�[���I�[�o�[�p�l���̃^�C�g���ɖ߂�{�^��")]
    private Button gameOverReturnTitleButton;

    [SerializeField, Header("�Q�[���N���A�p�l���̃^�C�g���ɖ߂�{�^��")]
    private Button gameClearReturnTitleButton;

    [SerializeField, Header("�X�s�[�h�e�L�X�g")]
    private TextMeshProUGUI speedText;
    public TextMeshProUGUI GetSpeedText => speedText;

    [SerializeField, Header("�X�s�[�h�摜")]
    private Image speedImage;
    public Image GetSpeedImage => speedImage;

    [SerializeField]
    private SpeedUI speedUI;
    public SpeedUI GetSpeedUI => speedUI;

    //�I������Ă���{�^��
    private GameObject selectButton;

    //�Q�b�^�[
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
        //�^�C�g���p�l���̏I���{�^���ɃQ�[�����I������֐���o�^
        titleFinishButton.onClick.AddListener(() => GameDirector.uniqueInstance.FinishGame());

        //�I�v�V�����p�l���̑�������{�^���Ɋ֐���o�^
        optionGuideButton.onClick.AddListener(
            () => {
                ChangePanel(optionScreen, guideScreen);
                soundManager.Play("�{�^��");
                currentEventSystem.SetSelectedGameObject(guideReturnOptionButton.gameObject);
                GameDirector.uniqueInstance.ChangeSceneState(SceneState.Guide);
            });

        //��������p�l���̃I�v�V�����ɖ߂�{�^���Ɋ֐���o�^
        guideReturnOptionButton.onClick.AddListener(
            () => {
                ChangePanel(guideScreen, optionScreen);
                soundManager.Play("�{�^��");
                currentEventSystem.SetSelectedGameObject(optionGuideButton.gameObject);
                GameDirector.uniqueInstance.ChangeSceneState(SceneState.Option);
            });
    }

    private void Update()
    {
        //�V�[�����Ƃ�UI�̍X�V
        switch (GameDirector.uniqueInstance.GetCurrentSceneState)
        {
            //�^�C�g��
            case SceneState.Title:
                //�G�X�P�[�v�L�[��������
                if (Input.GetKeyDown(KeyCode.Escape))
                    //�I���{�^��������
                    titleFinishButton.onClick.Invoke();
                break;

            //���C���Q�[��
            case SceneState.MainGame:
                //�G�X�P�[�v�L�[��������
                if (Input.GetKeyDown(KeyCode.Escape))
                    //�I�v�V�����{�^��������
                    mainGameOptionButton.onClick.Invoke();
                break;

            //�I�v�V����
            case SceneState.Option:
                //��ɉ����̃{�^����I����Ԃɂ��Ă���
                SelectAlwaysAnyButton(ref selectButton);
                //�G�X�P�[�v�L�[��������
                if (Input.GetKeyDown(KeyCode.Escape))
                    //�I�v�V�����{�^��������
                    optionReturnGameButton.onClick.Invoke();
                break;

            //�������
            case SceneState.Guide:
                //��ɉ����̃{�^����I����Ԃɂ��Ă���
                SelectAlwaysAnyButton(ref selectButton);
                //�G�X�P�[�v�L�[��������
                if (Input.GetKeyDown(KeyCode.Escape))
                    //�߂�{�^��������
                    guideReturnOptionButton.onClick.Invoke();
                break;

            //�Q�[���I�[�o�[
            case SceneState.GameOver:
                //��ɉ����̃{�^����I����Ԃɂ��Ă���
                SelectAlwaysAnyButton(ref selectButton);
                break;

            //�Q�[���N���A
            case SceneState.GameClear:
                //��ɉ����̃{�^����I����Ԃɂ��Ă���
                SelectAlwaysAnyButton(ref selectButton);
                break;
        }
    }

    /// <summary>
    /// ��ɉ����̃{�^����I����Ԃɂ��Ă��������ꍇ�Ɏg�p����
    /// </summary>
    /// <param name="selectedButton">�I������Ă����{�^��</param>
    private void SelectAlwaysAnyButton(ref GameObject selectedButton)
    {
        //���ݑI�𒆂̃{�^�����擾
        GameObject currentButton = currentEventSystem.currentSelectedGameObject;

        Debug.Log($"�I�𒆂̃{�^��:{selectButton}");

        if (currentButton != null)
            selectedButton = currentButton;
        else
            //�{�^�����I������ĂȂ��ꍇ�A�O��I�����ꂽ�{�^����I����Ԃɂ���
            if (selectedButton != null)
                currentEventSystem.SetSelectedGameObject(selectedButton);
    }

    /// <summary>
    /// �p�l����ύX����
    /// </summary>
    /// <param name="currentPanel">��\���ɂ���p�l��</param>
    /// <param name="nextPanel">�\������p�l��</param>
    public void ChangePanel(GameObject currentPanel, GameObject nextPanel)
    {
        currentPanel.SetActive(false);
        nextPanel.SetActive(true);
    }

    /// <summary>
    /// �{�^����I����Ԃɂ���
    /// </summary>
    /// <param name="selectButtonObject">�I����Ԃɂ���{�^��</param>
    public void SetSelectedButton(Button selectedButton)
    {
        //�I����Ԃɂ���Q�[���I�u�W�F�N�g
        GameObject selectedGameObject = selectedButton.gameObject;

        currentEventSystem.SetSelectedGameObject(selectedGameObject);
    }
}