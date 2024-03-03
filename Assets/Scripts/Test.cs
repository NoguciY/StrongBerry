using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    //ゲームのシーン管理をする


}


/// <summary>
/// Stateの抽象クラス
/// </summary>
public abstract class State
{
    /// <summary>
    /// ステート開始時に呼ばれる
    /// </summary>
    /// <param name="gameManager">ステートのオーナー</param>
    /// <param name="preState">前のステート</param>
    public virtual void OnEnter(GManager gameManager, State preState) { }

    /// <summary>
    /// 毎フレーム呼ばれる
    /// </summary>
    /// <param name="gameManager">ステートのオーナー</param>
    public virtual void OnUpdate(GManager gameManager) { }

    /// <summary>
    /// ステート終了時に呼ばれる
    /// </summary>
    /// <param name="gameManager">ステートのオーナー</param>
    /// <param name="nextState">次のステート</param>
    public virtual void OnExit(GManager gameManager, State nextState) { }
}

