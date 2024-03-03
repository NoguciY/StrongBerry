using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//プレイヤーに触れたらこのクラスをGetComponentして
//このクラスのメソッドを実行することで、楽にしたい
//しかし、実行したい処理の引数が異なる場合はどうするか？
//同じアイテムでも「コイン」と「チェックポイント」でExcuteItemAbility()の引数が異なる
//引数とは、外から受け取りたい値(データ)
//プレイヤーからのデータを受け取りたいから、プレイヤーのコンポーネントを引数にすればいい

public interface IItemController
{
    //プレイヤーと衝突した時の処理
    void ExcuteItemAbility(PlayerManager playerManager);
}
