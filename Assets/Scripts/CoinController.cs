using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour, IItemController
{
    public void ExcuteItemAbility(PlayerManager mPlayerManager)
    {
        //エフェクト
        //コインの取得枚数更新
        //非表示
        Destroy(this.gameObject);
    }
}
