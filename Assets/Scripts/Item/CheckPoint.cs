using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//チェックポイントを通るとプレイヤーのリスポーン地点が更新される

public class CheckPoint : MonoBehaviour, IItemController
{
    //エフェクト
    private ParticleSystem checkPointEffect;

    //チェックポイントの位置
    private Vector3 checkPointPos;

    //チェックポイントの回転値
    private Vector3 checkPointRot;

    //チェックポイント通過フラグ
    private bool passedCheckPoint;

    private SoundManager soundManager;

    //ゲッター
    public bool PassedCheckPoint
    {
        get { return passedCheckPoint; }
        set { 
            passedCheckPoint = value;
            Debug.Log($"チェックポイント通過：{passedCheckPoint}");
        }
    }

    //初期化
    public void Init(SoundManager soundManager, ParticleSystem checkPointEffect)
    {
        passedCheckPoint = false;
        checkPointPos = this.transform.position;
        checkPointRot = this.transform.localEulerAngles;
        this.soundManager = soundManager;
        this.checkPointEffect = checkPointEffect;
    }

    //チェックポイントを通過したときの処理
    public void ExcuteItemAbility(PlayerManager playerManager)
    {
        if (!passedCheckPoint)
        {
            //チェックポイント通過フラグオン
            passedCheckPoint = true;

            if (checkPointEffect != null)
            {
                //エフェクトの再生位置の指定
                checkPointEffect.transform.position = playerManager.transform.position;
                //エフェクトを再生
                checkPointEffect.Play();
            }

            //効果音の再生
            soundManager.Play("チェックポイント");

            //チェックポイントをリスポーン地点にする
            playerManager.RespawnPos = checkPointPos;

            //チェックポイントが向いている方向をリスポーン回転値にする
            playerManager.RespawnRot = checkPointRot;
        }
    }
}
