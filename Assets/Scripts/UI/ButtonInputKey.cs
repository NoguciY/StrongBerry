using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonInputKey : MonoBehaviour
{
    /// <summary>���̃L�[�������ƃ{�^������������</summary>
    [SerializeField] KeyCode _key = default;
    Button _button = default;

    //�Q�[���}�l�[�W���[
    [SerializeField]
    private GameDirector gameDirector;

    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => gameDirector.FinishGame());
    }

    void Update()
    {
        if (Input.GetKeyDown(_key))
        {
            // �N���b�N�͗��������ɐ������邪�A�{�^������̏ꍇ�͉��������_�Ő���������
            _button.onClick.Invoke();
            // �{�^�������������̌����ڂ̕ω����N����
            ExecuteEvents.Execute(_button.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
        }
        else if (Input.GetKeyUp(_key))
        {
            ExecuteEvents.Execute(_button.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerUpHandler);
        }
    }
}
