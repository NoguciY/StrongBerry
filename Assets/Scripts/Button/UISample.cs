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
        //�N���b�N�C�x���g��ݒ肷��
        _button.OnClickAction = () => { Debug.Log("Click"); };

        //�������C�x���g��ݒ肷��
        _button.OnLongPressAction = () => { Debug.Log("LongPress"); };
    }
}
