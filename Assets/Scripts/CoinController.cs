using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour, IItemController
{
    public void ExcuteItemAbility(PlayerManager mPlayerManager)
    {
        //�G�t�F�N�g
        //�R�C���̎擾�����X�V
        //��\��
        Destroy(this.gameObject);
    }
}
