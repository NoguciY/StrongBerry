using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneStateBase
{
    /// <summary>
    /// 開始時に呼ばれる
    /// </summary>
    /// <param name="gameManager">シーンステートの管理者</param>
    /// <param name="preState">前のステート</param>
    public virtual void OnEnter(GManager gameManager, SceneStateBase preState){ }


    /// <summary>
    /// フレームごとに呼ばれる
    /// </summary>
    /// <param name="gameManager">シーンステートの管理者</param>
    public virtual void OnUpdate(GManager gameManager){ }

    /// <summary>
    /// 終了時に呼ばれる
    /// </summary>
    /// <param name="gameManager">シーンステートの管理者</param>
    /// <param name="nextState">次のステート</param>
    public virtual void OnExit(GManager gameManager, SceneStateBase nextState){ }
}
