using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour, IItemController
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //�A�j���[�V����
    }

    public void ExcuteItemAbility(PlayerManager mPlayerManager)
    {
        //�G�t�F�N�g
        //�R�C���̎擾�����X�V
        //��\��
        Destroy(this.gameObject);
    }
}
