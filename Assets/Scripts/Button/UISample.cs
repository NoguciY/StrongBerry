using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISample : MonoBehaviour
{
    [SerializeField]
    private CustomButton _button;

    // Start is called before the first frame update
    void Start()
    {
        //クリックイベントを設定する
        _button.OnClickAction = () => { Debug.Log("Click"); };

        //長押しイベントを設定する
        _button.OnLongPressAction = () => { Debug.Log("LongPress"); };
    }
}
