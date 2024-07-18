using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public class GoalPoint : MonoBehaviour, IItemController
{
    //エフェクト
    public ParticleSystem GameClearEffect;

    public void ExcuteItemAbility(PlayerManager playerManager)
    {
        Debug.Log("ゴール！");

        //ゴールした場合、クリアイベントを開始
        playerManager.gameClearEvent.Invoke();

        if (GameClearEffect != null)
            //エフェクトを再生
            GameClearEffect.Play();
    }
}
