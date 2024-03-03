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
        //アニメーション
    }

    public void ExcuteItemAbility(PlayerManager mPlayerManager)
    {
        //エフェクト
        //コインの取得枚数更新
        //非表示
        Destroy(this.gameObject);
    }
}
