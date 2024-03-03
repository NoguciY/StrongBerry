using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//チェックポイントを通るとプレイヤーのリスポーン地点が更新される

public class CheckPoint : MonoBehaviour, IItemController
{
    //エフェクト
    [SerializeField]
    private ParticleSystem checkPointEffect;

    //チェックポイントの位置
    private Vector3 checkPointPos;

    //チェックポイントの回転値
    private Vector3 checkPointRot;

    //チェックポイント通過フラグ
    private bool passedCheckPoint;

    //オーディオソース
    private AudioSource checkPointAudioSource;

    //ゲッター
    public bool GetPassedCheckPoint => passedCheckPoint;


    // Start is called before the first frame update
    private void Start()
    {
        checkPointPos = this.transform.position;
        checkPointRot = this.transform.localEulerAngles;

        checkPointAudioSource = GetComponent<AudioSource>();
    }


    //チェックポイントを通過したときの処理(一度だけ)
    public void PassedCheckPoint(ref Vector3 respawnPos, ref Vector3 respawnRot, Vector3 playerPos)
    {
        if (!passedCheckPoint)
        {
            //チェックポイント通過フラグオン
            passedCheckPoint = true;

            if (checkPointEffect != null)
            {
                //エフェクトの再生位置の指定
                checkPointEffect.transform.position = playerPos;
                //エフェクトを再生
                checkPointEffect.Play();
            }

            //効果音の再生
            checkPointAudioSource.Play();

            //チェックポイントをリスポーン地点にする
            respawnPos = checkPointPos;

            //チェックポイントが向いている方向をリスポーン回転値にする
            respawnRot = checkPointRot;
        }
    }

    //チェックポイント通過フラグをfalseにする
    public void SetPassedCheckPointFlag()
    {
        passedCheckPoint = false;
    }

    //チェックポイントを通過したときの処理(インターフェース版)
    public void ExcuteItemAbility(PlayerManager mPlayerManager)
    {
        if (!passedCheckPoint)
        {
            //チェックポイント通過フラグオン
            passedCheckPoint = true;

            if (checkPointEffect != null)
            {
                //エフェクトの再生位置の指定
                checkPointEffect.transform.position = mPlayerManager.GetPos;
                //エフェクトを再生
                checkPointEffect.Play();
            }

            //効果音の再生
            checkPointAudioSource.Play();

            //チェックポイントをリスポーン地点にする
            mPlayerManager.respawnPos = checkPointPos;

            //チェックポイントが向いている方向をリスポーン回転値にする
            mPlayerManager.respawnRot = checkPointRot;
        }
    }
}
