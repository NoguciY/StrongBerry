using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;
using UnityEditorInternal;
using UnityEngine;


    /// <summary>
    /// �X�e�[�g�p�^�[����class��`�̊��N���X�؂�o����
    /// </summary>
    public class StateMachine<TOwner>
    {
        /// <summary>
        /// �e�X�e�[�g�N���X���p��������N���X
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

        //���݂̃X�e�[�g
        private StateBase currentState;

        //�O�̃X�e�[�g
        private StateBase preState;

        //�S�ẴX�e�[�g�̒�`
        private readonly Dictionary<int, StateBase> states = new Dictionary<int, StateBase>();  
    
        /// <summary>
        /// �R���X�g���N�^
        /// onwer�͓ǂݎ���p�̂��߃R���X�g���N�^�ő�����Ă���
        /// </summary>
        /// <param name="owner">StateMachine�̃I�[�i�[</param>
        public StateMachine(TOwner owner)
        {
            Owner = owner;
        }

        /// <summary>
        /// �X�e�[�g��`��o�^
        /// �X�e�[�g�}�V����������ɂ��̃��\�b�h���Ă�
        /// </summary>
        /// <typeparam name="T">�X�e�[�g�^</typeparam>
        /// <param name="stateID">�X�e�[�gID</param>
        public void Add<T>(int stateID) where T : StateBase, new()
        {
            //states(�f�B�N�V���i���[)�Ɋ��ɓ���stateID(�L�[)�����݂��邩�m�F
            if (states.ContainsKey(stateID))
            {
                Debug.LogError("����stateID���o�^����Ă��܂�:" + stateID);
                return;
            }

            //�V�����X�e�[�g�̍쐬
            var newState = new T()
            { 
                //�I�u�W�F�N�g�������q
                //�ϐ��ƃv���p�e�B�����蓖�Ă���
                stateMachine = this     
            };

            //�f�B�N�V���i���[�ɐV�����X�e�[�g��o�^
            states.Add(stateID, newState );
        }

        /// <summary>
        /// �X�e�[�g�J�n���̏���
        /// </summary>
        /// <param name="stateId">�X�e�[�gID</param>
        public void OnEnter(int stateID)
        {
            //out���g���ꍇ�A���O�̕ϐ��錾���s�v
            //StateBase nextState; �͕s�v

            //TryGetValue : ����̃L�[�����݂��邩�m�F���A���݂���ꍇ�͂��̃L�[�Ɍ��т���ꂽ�l�����o��
            //stateID�����݂��Ȃ��ꍇ�AnextSate�ɂ�null��Ԃ��G���[��\������

            if (!states.TryGetValue(stateID, out var nextState))
            {
                Debug.LogError("stateID���ݒ肳��Ă��܂��� : " + stateID);
                return;
            }
            // ���݂̃X�e�[�g�ɐݒ肵�ď������J�n
            currentState = nextState;
            currentState.OnEnter();
        }

        /// <summary>
        /// �X�e�[�g�X�V���̏���
        /// </summary>
        public void OnUpdate()
        {
            currentState.OnUpdate();
        }

        /// <summary>
        /// ���̃X�e�[�g�ɐ؂�ւ���
        /// </summary>
        /// <param name="stateId">�؂�ւ���X�e�[�gID</param>
        public void ChangeState(int stateId)
        {
            if (!states.TryGetValue(stateId, out var nextState))
            {
                Debug.LogError("stateId���ݒ肳��Ă��܂��� : " + stateId);
                return;
            }
            // �O�̃X�e�[�g��ێ�
            preState = currentState;
            // �X�e�[�g��؂�ւ���
            currentState.OnExit();
            currentState = nextState;
            currentState.OnEnter();
        }

        /// <summary>
        /// �O��̃X�e�[�g�ɐ؂�ւ���
        /// </summary>
        public void ChangePrevState()
        {
            if (preState == null)
            {
                Debug.LogError("prevState��null�ł�");
                return;
            }

            // �O�̃X�e�[�g�ƌ��݂̃X�e�[�g�����ւ���(�^�v�����g��)
            (preState, currentState) = (currentState, preState);
        }
    }