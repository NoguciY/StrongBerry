using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//�Q�[���̃N���A��Q�[���I�[�o�[
//UI�̍X�V�A�V�[���J�ڂ��s��

public partial class GManager : MonoBehaviour
{
    public enum ButtonType
    {
        Start,      //�Q�[���J�n
        Finish,     //�Q�[���I��
        Continue,   //�R���e�B�j���[
        Reset,      //�^�C�g���ɖ߂�
        Option,     //�I�v�V����
        Guide,      //�������
        Return,     //�Q�[���ɖ߂�
    }

    //�^�C�g��
    [SerializeField]
    private GameObject titleScreen;

    //�Q�[���I�[�o�[
    [SerializeField]
    private GameObject gameOverScreen;

    //�������
    [SerializeField]
    private GameObject guideScreen;

    //���C���Q�[��UI
    [SerializeField]
    private GameObject mainGameImages;

    //�I�v�V����
    [SerializeField]
    private GameObject optionScreen;

    //�Q�[���N���A
    [SerializeField]
    private GameObject gameClearScreen;

    //�Q�[������
    [SerializeField]
    private GameObject explanationScreen;

    //�R���e�B�j���[�{�^��
    [SerializeField]
    private Button continueButton;

    //�X�^�[�g�{�^��
    [SerializeField]
    private Button startButton;

    //���X�^�[�g�{�^��
    [SerializeField]
    private Button restartButton;

    //�v���C���[�̊Ǘ��p
    [SerializeField]
    private PlayerManager playerManager;

    //�`�F�b�N�|�C���g
    [SerializeField]
    private CheckPoint[] checkPoints;

    //�N���A���̃G�t�F�N�g
    [SerializeField]
    private ParticleSystem clearEffect;

    //�X�^�[�g�{�^����
    [SerializeField]
    private AudioClip startSound;

    //���̑��̃{�^����
    [SerializeField]
    private AudioClip buttonSound;

    //�Q�[���I�[�o�[��
    [SerializeField]
    private AudioClip gameOverSound;

    //�N���A��
    [SerializeField]
    private AudioClip gameClearSound;

    //�v���C���[�̏����ʒu
    private Vector3 playerInitPos;

    //�v���C���[�̏�����]�l
    private Vector3 playerInitRot;

    //�v���C���[�̃��X�|�[���ʒu
    private Vector3 playerRespawnPos;

    //�v���C���[�̃��X�|�[�����̉�]�l
    private Vector3 playerRespawnRot;

    //�N���A���Ă��邩
    private bool isClear = false;

    //�Q�[�����J�n���ꂽ��
    private bool isStart = false;

    //�������\�����ꂽ��
    //private bool isDisplayedExplanation = false;

    //�I�[�f�B�I�\�[�X
    private AudioSource seAudioSource;

    //���݂̃C�x���g�V�X�e�����擾
    [SerializeField]
    private EventSystem currentEventSystem = EventSystem.current;

    //�e�{�^��
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

    //�X�e�[�g�̃C���X�^���X
    private static readonly SceneStateStart sceneStateStart         = new SceneStateStart();
    private static readonly SceneStateMain sceneStateMain           = new SceneStateMain();
    private static readonly SceneStateOption sceneStateOption       = new SceneStateOption();
    private static readonly SceneStateGuide sceneStateGuide         = new SceneStateGuide();
    private static readonly SceneStateGameOver sceneStateGameOver   = new SceneStateGameOver();
    private static readonly SceneStateGameClear sceneStateGameClear = new SceneStateGameClear();

    //���݂̃X�e�[�g
    private SceneStateBase currentState = sceneStateStart;


    /// <summary>
    /// �X�e�[�g�̕ύX
    /// </summary>
    /// <param name="nextState">���̃X�e�[�g</param>
    private void ChangeState(SceneStateBase nextState)
    {
        currentState.OnExit(this, nextState);
        nextState.OnEnter(this, currentState);
        currentState = nextState;
    }




    private void Awake()
    {
        //�Q�[�����~
        PauseGame();
        //�J�[�\���𒆉��ɌŒ肵�āA��\��
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentState.OnEnter(this, null);
    }

    // Start is called before the first frame update
    private void Start()
    {
        seAudioSource = GetComponent<AudioSource>();

        //�����ʒu���擾
        playerInitPos = playerManager.GetInitPos;
        //������]�l���擾
        playerInitRot = playerManager.GetInitRot;
        //���X�|�[���ʒu���擾
        playerRespawnPos = playerInitPos;
        //���X�|�[����]�l���擾
        playerRespawnRot = playerInitRot;
    }

    // Update is called once per frame
    private void Update()
    {
        currentState.OnUpdate(this);

        //ESC�L�[������
        //�I�v�V�����A�I������A�߂�
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    //�^�C�g����� �N���A��� �Q�[���I�[�o�[��� �̏ꍇ
        //    if (titleScreen.activeSelf || gameClearScreen.activeSelf ||gameOverScreen.activeSelf)
        //    {
        //        //�Q�[�����I������
        //        FinishGame();
        //    }
        //    //���C���Q�[����ʂ̏ꍇ
        //    else if (mainGameImages.activeSelf)
        //    {
        //        //�Q�[�����~
        //        PauseGame();
        //        //�{�^�������Đ�
        //        PlaySound(buttonSound);
        //        //�I�v�V�������J��
        //        OpenScreen(mainGameImages, optionScreen, guideButton);
        //    }
        //    //�I�v�V������ʂ̏ꍇ
        //    else if (optionScreen.activeSelf)
        //    {
        //        //�{�^�������Đ�
        //        PlaySound(startSound);
        //        //�Q�[���ɖ߂�
        //        ReturnToGame(optionScreen);
        //    }
        //    //���������ʂ̏ꍇ
        //    else if (guideScreen.activeSelf)
        //    {
        //        //�{�^�������Đ�
        //        PlaySound(buttonSound);
        //        //�I�v�V�������J��
        //        OpenScreen(guideScreen, optionScreen, guideButton);
        //    }
        //}

        ////�Q�[���J�n���ɂP�x����������\������
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
        //    //�N���A�����ꍇ�A�N���A��ʂ�
        //    if (IsClearGame())
        //    {
        //        isClear = true;
        //        GameClear();
        //    }
        //}

        ////�N���A�G�t�F�N�g���Đ�
        //else { clearEffect.Simulate(Time.unscaledDeltaTime, false, false); }
    }


    //�N���A����
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

    //�Q�[���N���A(�J�n��)
    private void GameClear()
    {
        //���C���Q�[��UI���\��
        HidScreen(mainGameImages);

        //�Q�[���N���A��ʂ�\��
        DisplayedScreen(gameClearScreen);

        //�Q�[�����~
        PauseGame();

        //���X�^�[�g�{�^����I��
        restartButton.Select();

        //�Q�[���N���A���Đ�
        PlaySound(gameClearSound);
    }

    //�Q�[���I�[�o�[(�J�n��)
    public void GameOver()
    {
        //���C���Q�[��UI���\��
        HidScreen(mainGameImages);

        //�Q�[���I�[�o�[��ʂ�\��
        DisplayedScreen(gameOverScreen);

        //�Q�[�����~
        PauseGame();

        //�R���e�B�j���[�{�^����I��
        continueButton.Select();

        //�Q�[���I�[�o�[���Đ�
        PlaySound(gameOverSound);
    }

    //�Q�[�����~
    private void PauseGame()
    {
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;
        }
    }


    //�Q�[�����ĊJ
    private void ResumeGame()
    {
        if (Time.timeScale != 1)
        {
            Time.timeScale = 1;
        }
    }

    //�Q�[���I�u�W�F�N�g�ɂ���Ďw�肵����ʂ�\������
    private void DisplayedScreen(GameObject screen)
    {
        screen.SetActive(true);
    }


    //�{�^���ɂ���Ďw�肵����ʂ�\������
    private void DisplayedScreen(ButtonType buttonType)
    {
        switch (buttonType)
        {
            //�^�C�g���ɖ߂�
            case ButtonType.Reset:
                titleScreen.SetActive(true);
                break;
            //�������
            case ButtonType.Guide:
                guideScreen.SetActive(true);
                break;
            //�I�v�V����
            case ButtonType.Option:
                optionScreen.SetActive(true);
                break;
        }
    }

    //�Q�[���I�u�W�F�N�g�ɂ���Ďw�肵����ʂ��\���ɂ���
    private void HidScreen(GameObject screen)
    {
        screen.SetActive(false);
    }

    //�{�^���ɂ���Ďw�肵����ʂ��\���ɂ���
    private void HidScreen(ButtonType buttonType)
    {
        switch (buttonType)
        {
            //�X�^�[�g
            case ButtonType.Start:
                titleScreen.SetActive(false);
                break;
            //�R���e�B�j���[
            case ButtonType.Continue:
                gameOverScreen.SetActive(false);
                break;
            //�Q�[���ɖ߂� �܂��� �������
            case ButtonType.Return:
            case ButtonType.Option:
                optionScreen.SetActive(false);
                break;
            //�^�C�g���ɖ߂�
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

    //��ʂ��J��
    private void OpenScreen(GameObject closedScreen, GameObject openedScreen, Button selectButton = default)
    {
        //���݂̉�ʂ��\��
        HidScreen(closedScreen);

        //��ʂ�\��
        DisplayedScreen(openedScreen);

        if (selectButton != null)
        {
            //�{�^����I��
            selectButton.Select();
        }
    }

    

    //AudioClip������SE���Đ�
    private void PlaySound(AudioClip audioClip)
    {
        seAudioSource.clip = audioClip;
        seAudioSource.Play();
    }


    //�v���C���[���Đݒ肷��(�R���e�B�j���[�A�^�C�g���ɖ߂�����)
    private void ResetPlayer(Vector3 resetPos, Vector3 resetRot)
    {
        //�v���C���[�̈ʒu�Ɖ�]�l���Đݒ肷��
        playerManager.transform.position = resetPos;
        playerManager.transform.eulerAngles = resetRot;
    }
    
    //�Q�[���X�^�[�g���ɐ�����\�����邽�߂̃R���[�`��
    private IEnumerator DisplayedExplanationCoroutine()
    {
        //������\��
        explanationScreen.SetActive(true);

        //�ҋ@����
        //�|�[�Y���̂��� WaitForSecondsRealtime ���g��
        yield return new WaitForSecondsRealtime(3.0f);
        
        //�������\��
        explanationScreen.SetActive(false);

        //�Q�[�����~����
        ResumeGame();
    }

    //�Q�[�����J�n����
    public void StartGame()
    {   
        //���C���Q�[����ʂ��J��
        OpenScreen(titleScreen, mainGameImages);

        //�X�^�[�g���Đ�
        PlaySound(startSound);

        if (isStart)
        {
            //�^�C���X�P�[����1�ɂ���
            ResumeGame();
        }

        //�Q�[���J�n�t���O�I��
        isStart = true;
    }


    //����������J��
    public void OpenGuide()
    {
        //�I�v�V�������\��
        HidScreen(ButtonType.Option);

        //���������\��
        DisplayedScreen(ButtonType.Guide);

        //�{�^�������Đ�
        PlaySound(buttonSound);
    }


    //������������
    public void CloseGuide() 
    {
        //����������\��
        HidScreen(ButtonType.Guide);

        //�I�v�V������\��
        DisplayedScreen(ButtonType.Option);

        //�{�^�������Đ�
        PlaySound(buttonSound);
    }

    //�Q�[���ɖ߂�,��蒼��(�R���e�B�j���[)
    public void ReturnToGame(GameObject screen)
    {
        //�Q�[���I�[�o�[��� �܂��� �I�v�V������ʂ̏ꍇ
        if (screen == gameOverScreen || optionScreen)
        {
            //���X�|�[���n�_�Ɖ�]�l���擾
            playerRespawnPos = playerManager.respawnPos;
            playerRespawnRot = playerManager.respawnRot;

            //�v���C���[�̓������~�߂�
            playerManager.StopPlayerMovement();

            //�`�F�b�N�|�C���g����n�߂�
            ResetPlayer(playerRespawnPos, playerRespawnRot);
        }
        //�N���A��ʂ̏ꍇ
        else if (screen == gameClearScreen)
        {
            for (int i = 0; i < checkPoints.Length; i++)
            {
                //�`�F�b�N�|�C���g�ʉ߃t���O���I�t
                checkPoints[i].SetPassedCheckPointFlag();
            }
            //�N���A�t���O���I�t
            isClear = false;
            //�N���A�G�t�F�N�g���폜
            clearEffect.Clear();

            //�v���C���[�������l�Ƀ��Z�b�g����
            ResetPlayer(playerInitPos, playerInitRot);
        }

        //���݂̉�ʂ��\��
        screen.SetActive(false);

        //���C���Q�[��UI��\������
        mainGameImages.SetActive(true);

        //�{�^�������Đ�
        PlaySound(startSound);

        //�^�C���X�P�[����1�ɂ���
        ResumeGame();
    }


    /// <summary>
    /// �^�C�g���ɖ߂�
    /// </summary>
    /// <param name="screen"></param>
    /// <retval></retval>
    public void ReturnToTitle(GameObject screen)
    {
        //���݂̉�ʂ��\��
        screen.SetActive(false);

        //�`�F�b�N�|�C���g�ʉ߃t���O���I�t
        for (int i = 0; i < checkPoints.Length; i++)
        {
            checkPoints[i].SetPassedCheckPointFlag();
        }

        //�N���A��ʂ̏ꍇ
        if (screen == gameClearScreen)
        {
            //�N���A�t���O���I�t
            isClear = false;
            //�N���A�G�t�F�N�g���폜
            clearEffect.Clear();
        }

        //�v���C���[�̓������~�߂�
        playerManager.StopPlayerMovement();

        //�^�C�g����ʂ�\������
        DisplayedScreen(ButtonType.Reset);

        //�v���C���[�������l�Ƀ��Z�b�g����
        ResetPlayer(playerInitPos, playerInitRot);

        //�{�^�������Đ�
        PlaySound(buttonSound);

        //�X�^�[�g�{�^����I��
        startButton.Select();
    }


    //�Q�[�����I������
    private void FinishGame()
    {
//UnityEditor�Ńv���C���Ă���ꍇ
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

//�r���h�����Q�[�����v���C�����Ă���ꍇ
#else
        Application.Quit();
#endif
    }

   
    /// <summary>
    /// ��ɉ����̃{�^����I����Ԃɂ��Ă��������ꍇ�Ɏg�p����
    /// </summary>
    /// <param name="selectedButton">�I������Ă����{�^��</param>
    private void SelectAlwaysAnyButton(ref GameObject selectedButton)
    {
        //���ݑI�𒆂̃{�^�����擾
        GameObject currentButton = currentEventSystem.currentSelectedGameObject;

        if (currentButton)
        {
            selectedButton = currentButton;
        }
        else
        {
            //�{�^�����I������ĂȂ��ꍇ�A�O��I�����ꂽ�{�^����I����Ԃɂ���
            if (selectedButton != null)
            {
                currentEventSystem.SetSelectedGameObject(selectedButton);
            }
        }
    }
}
