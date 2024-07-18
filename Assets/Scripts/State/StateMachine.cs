using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;
using UnityEditorInternal;
using UnityEngine;


    /// <summary>
    /// ステートパターンのclass定義の基底クラス切り出し版
    /// </summary>
    public class StateMachine<TOwner>
    {
        /// <summary>
        /// 各ステートクラスが継承する基底クラス
        /// </summary>
        public abstract class StateBase
        { 
            public StateMachine<TOwner> stateMachine;
            protected TOwner Owner => stateMachine.Owner;

            public virtual void OnEnter() { }
            public virtual void OnUpdate() { }
            public virtual void OnExit() { }
        }

        private TOwner Owner { get; }

        //現在のステート
        private StateBase currentState;

        //前のステート
        private StateBase preState;

        //全てのステートの定義
        private readonly Dictionary<int, StateBase> states = new Dictionary<int, StateBase>();  
    
        /// <summary>
        /// コンストラクタ
        /// onwerは読み取り専用のためコンストラクタで代入しておく
        /// </summary>
        /// <param name="owner">StateMachineのオーナー</param>
        public StateMachine(TOwner owner)
        {
            Owner = owner;
        }

        /// <summary>
        /// ステート定義を登録
        /// ステートマシン初期化後にこのメソッドを呼ぶ
        /// </summary>
        /// <typeparam name="T">ステート型</typeparam>
        /// <param name="stateID">ステートID</param>
        public void Add<T>(int stateID) where T : StateBase, new()
        {
            //states(ディクショナリー)に既に同じstateID(キー)が存在するか確認
            if (states.ContainsKey(stateID))
            {
                Debug.LogError("既にstateIDが登録されています:" + stateID);
                return;
            }

            //新しいステートの作成
            var newState = new T()
            { 
                //オブジェクト初期化子
                //変数とプロパティを割り当てられる
                stateMachine = this     
            };

            //ディクショナリーに新しいステートを登録
            states.Add(stateID, newState );
        }

        /// <summary>
        /// ステート開始時の処理
        /// </summary>
        /// <param name="stateId">ステートID</param>
        public void OnEnter(int stateID)
        {
            //outを使う場合、事前の変数宣言が不要
            //StateBase nextState; は不要

            //TryGetValue : 特定のキーが存在するか確認し、存在する場合はそのキーに結びつけられた値を取り出す
            //stateIDが存在しない場合、nextSateにはnullを返しエラーを表示する

            if (!states.TryGetValue(stateID, out var nextState))
            {
                Debug.LogError("stateIDが設定されていません : " + stateID);
                return;
            }
            // 現在のステートに設定して処理を開始
            currentState = nextState;
            currentState.OnEnter();
        }

        /// <summary>
        /// ステート更新時の処理
        /// </summary>
        public void OnUpdate()
        {
            currentState.OnUpdate();
        }

        /// <summary>
        /// 次のステートに切り替える
        /// </summary>
        /// <param name="stateId">切り替えるステートID</param>
        public void ChangeState(int stateId)
        {
            if (!states.TryGetValue(stateId, out var nextState))
            {
                Debug.LogError("stateIdが設定されていません : " + stateId);
                return;
            }
            // 前のステートを保持
            preState = currentState;
            // ステートを切り替える
            currentState.OnExit();
            currentState = nextState;
            currentState.OnEnter();
        }

        /// <summary>
        /// 前回のステートに切り替える
        /// </summary>
        public void ChangePrevState()
        {
            if (preState == null)
            {
                Debug.LogError("prevStateがnullです");
                return;
            }

            // 前のステートと現在のステートを入れ替える(タプルを使う)
            (preState, currentState) = (currentState, preState);
        }
    }