using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//チェックポイント、コインを管理する
public class ItemManager : MonoBehaviour
{
    [SerializeField]
    private CheckPoint[] checkPoints;

    [SerializeField]
    private GoalPoint goalPoint;

    //エフェクト
    [SerializeField]
    private ParticleSystem[] effects;

    //サウンドマネージャー
    [SerializeField]
    SoundManager soundManager;

    //ゲッター
    public GoalPoint GetGoalPoint => goalPoint;

    //エフェクトタイプ
    private enum EffectType
    { 
        CheckPoint,     //チェックポイン
        GameClear,      //ゲームクリア
    }


    private void Awake()
    {
        foreach (CheckPoint checkPoint in checkPoints)
        {
            //チェックポイントの初期化
            checkPoint.Init(soundManager, effects[(int)EffectType.CheckPoint]);
        }

        //ゴールポイントの初期化
        goalPoint.GameClearEffect = effects[(int)EffectType.GameClear];
    }

    //アイテムの初期化
    public void Init()
    {
        //全てのチェックポイントのフラグを下げる
        for(int i = 0; i < checkPoints.Length; i++)
        {
            checkPoints[i].PassedCheckPoint = false;
        }
    }
}
