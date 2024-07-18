using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonInputKey : MonoBehaviour
{
    /// <summary>このキーを押すとボタンが反応する</summary>
    [SerializeField] KeyCode _key = default;
    Button _button = default;

    //ゲームマネージャー
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
            // クリックは離した時に成立するが、ボタンからの場合は押した時点で成立させる
            _button.onClick.Invoke();
            // ボタンを押した時の見た目の変化を起こす
            ExecuteEvents.Execute(_button.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
        }
        else if (Input.GetKeyUp(_key))
        {
            ExecuteEvents.Execute(_button.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerUpHandler);
        }
    }
}
