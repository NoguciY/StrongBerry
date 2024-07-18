using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using static PlayerManager;

public class PlayerManager : MonoBehaviour
{
    [SerializeField, Header("�ړ����鑬��")]
    private float movingSpeed;

    [SerializeField, Header("�ō���")]
    private float maxMovingSpeed;

    [SerializeField, Header("�����x(m/s*s)")]
    private float accelPerSecond;

    [SerializeField, Header("�����(deg/s)")]
    private float turnPerSecond;

    [SerializeField, Header("��]�����p�̒l")]
    private float adjustedValueOfRotation;

    [SerializeField, Header("�v���C���[�{��")]
    private GameObject playerBody;

    //�Q�[���I�[�o�[�C�x���g
    public UnityEvent gameOverEvent;

    //�Q�[���N���A�C�x���g
    public UnityEvent gameClearEvent;

    //�X�s�[�h�\���C�x���g
    public class DisplaySpeedEvent : UnityEvent<float> { }
    private DisplaySpeedEvent displaySpeedEvent;

    //�v���C���[�̃��X�|�[���n�_
    public Vector3 RespawnPos { get; set; }

    //�v���C���[�̃��X�|�[����]�l
    public Vector3 RespawnRot { get; set; }

    //�O����
    private Vector3 forwardDirection;

    //�ړ��p��Rigidbody
    private Rigidbody myRigidbody;

    //�n��ɂ��邩
    private bool onGround;

    //�����x
    private Vector3 acceleration;

    //�n�ʂ̏����擾�p
    private GetGroundInformation getGroundInformation;

    //�n�ʂ̏��
    private RaycastHit groundInfo;

    //�v���C���[�̏����ʒu
    private Vector3 initPos;

    //�v���C���[�̏�����]�l
    private Vector3 initRot;

    //�O�ɐi��ł��邩
    private bool isMovingForward;

    //�ړ��L�[�̓��͒l
    private float verticalInputValue;

    //�O�t���[���̈ړ����͒l
    private float preVerticalInputValue;

    //���C���J����
    private Camera mainCamera;

    private Transform myTransform;

    //����\��
    [SerializeField]
    private bool isControllable;

    public bool IsControllable
    { 
        get { return isControllable; }
        set 
        { 
            isControllable = value;
            Debug.Log($"����\��:{isControllable}");
        }
    }


    //�A�N�Z�T
    public DisplaySpeedEvent GetDisplaySpeedEvent => displaySpeedEvent;
    public Vector3 GetInitPos => initPos;

    public Vector3 GetInitRot => initRot;

    public float GetMaxMovingSpeed => maxMovingSpeed;


    //GManager��Start()����ɐݒ肷�邽��
    private void Awake()
    {
        RespawnPos = transform.position;
        initPos = RespawnPos;

        RespawnRot = transform.localEulerAngles;
        initRot = RespawnRot;

        displaySpeedEvent = new DisplaySpeedEvent();
        //gameOverEvent = new UnityEvent();
    }

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        getGroundInformation = GetComponent<GetGroundInformation>();
        mainCamera = Camera.main;
        myTransform = transform;
        onGround = false;
        isControllable = false;
    }


    private void Update()
    {
        if (IsControllable)
        {
            //�n�㔻��
            onGround = groundInfo.collider;

            verticalInputValue = Input.GetAxisRaw("Vertical");

            float horizontalInputValue = Input.GetAxis("Horizontal");

            //���񂳂���
            InputToTurn(horizontalInputValue);

            //��]������
            VelocityToRotate();

            //�f�o�b�O�p(�K�v�Ȃ���Ώ���)
            DisyplayDebugLog();  
        }
    }


    private void FixedUpdate()
    {
        if (IsControllable)
        {
            //�n�ʂ̏��̎擾
            groundInfo = getGroundInformation.CheckForGround();

            //�O�����̎擾(���K���ς�)
            forwardDirection = Vector3.Cross(myTransform.right, groundInfo.normal);

            //�J��������v���C���[�̃x�N�g��
            Vector3 playerFromMainCameraVec = myTransform.position - mainCamera.transform.position;

            //�O���ɐi��ł��邩����
            isMovingForward = IsMovingForward(myRigidbody.velocity, playerFromMainCameraVec);

            //�����x�̎擾
            acceleration = InputToVelocity(verticalInputValue) / Time.fixedDeltaTime;

            //���x�։��Z
            myRigidbody.AddForce(acceleration, ForceMode.Acceleration);

            //���x�ɂ͏����݂���
            if (myRigidbody.velocity.magnitude >= maxMovingSpeed)
            myRigidbody.velocity = myRigidbody.velocity.normalized * maxMovingSpeed;

            //�O��̈ړ����͒l�Ƃ��ĕۑ�
            preVerticalInputValue = verticalInputValue;
        }

        //�X�s�[�h��\������
        displaySpeedEvent.Invoke(myRigidbody.velocity.magnitude);
    }

    /// <summary>
    /// ���͂�����񂳂���
    /// </summary>
    /// <param name="horizontalInputValue">���������̃L�[���͒l</param>
    private void InputToTurn(float horizontalInputValue)
    {
        //�v���C���[��y�������ɐ���͂Ɠ��͒l�ɉ����Đ��񂳂���
        myTransform.Rotate(Vector3.up, turnPerSecond * horizontalInputValue * Time.deltaTime);
    }

    /// <summary>
    /// ���x�ɉ����ĉ�]������
    /// </summary>
    private void VelocityToRotate()
    {
        //���[�g�̌v�Z�͕��ׂ���������sqrMagnitude���g��
        float sqrSpeed = myRigidbody.velocity.sqrMagnitude;

        //1�t���[���̉�]�p�x
        float rotationAnglePerFrame = sqrSpeed * Time.deltaTime * adjustedValueOfRotation;

        //����ɐi��ł���ꍇ�͋t��]����
        if (!isMovingForward)
            rotationAnglePerFrame *= -1f;

        playerBody.transform.Rotate(Vector3.forward * rotationAnglePerFrame);
    }

    //���͂��瑬�x��Ԃ�
    private Vector3 InputToVelocity(float verticalInputValue)
    {
        Vector3 movingDirection = Vector3.zero;

        //W��S�L�[������
        if (verticalInputValue != 0)
        {
            //W�L�[�������A�O���Ɉړ�
            if (verticalInputValue > 0)
            {
                movingDirection = forwardDirection;
            }
            //S�L�[�������A����Ɉړ�
            else if (verticalInputValue < 0)
            {
                movingDirection = -forwardDirection;
            }

            //�O������A��납��O�Ɉړ�����ꍇ�A
            if (verticalInputValue != preVerticalInputValue)
                movingSpeed = 0;

            //�������グ��
            movingSpeed += accelPerSecond * Time.fixedDeltaTime;
        }
        //W��S�L�[�������łȂ��ꍇ
        else
        {
            //������������
            movingSpeed -= accelPerSecond * Time.fixedDeltaTime;
            if (movingSpeed < 0) movingSpeed = 0;
        }

        Vector3 velocity = movingDirection * movingSpeed;
        return velocity;
    }

    //�����蔻�� 
    private void OnTriggerEnter(Collider other)
    {
        var item = other.gameObject.GetComponent<IItemController>();
        if(item != null)
            item.ExcuteItemAbility(this);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DeadObj"))
        {
            if(isControllable)
            //�Q�[���I�[�o�[�ɂ���
            gameOverEvent.Invoke();
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            //�v���C���[�ɂ�����͂�0�ɂ���
            acceleration = Vector3.zero;
        }
    }

    /// <summary>
    /// �J�����̃v���C���[�������Ă�������Ɛi�s�����̓��ς���
    /// �O���ɐi��ł��邩���ʂ���
    /// </summary>
    /// <returns>true:�O���ɐi��ł���, false:�O���ɐi��ł��Ȃ�</returns>
    private bool IsMovingForward(Vector3 firstVec, Vector3 secondVec)
    {
        //�O���ɐi��ł��邩
        bool isMovingForward = true;

        //�����̃x�N�g�����m�œ��ς����߂�
        float dot = Vector3.Dot(firstVec, secondVec);

        if (dot > 0)
            //�O���ɐi��ł���
            isMovingForward = true;

        else if (dot < 0)
            //�O���ɐi��ł��Ȃ�
            isMovingForward = false;

        return isMovingForward;
    }

    private void DisyplayDebugLog()
    {
        //Debug.Log($"�n�ォ�H:{onGround}");
        //Debug.Log($"�����F{movingSpeed}");
        //Debug.Log($"���x�F{myRigidbody.velocity}");
        //Debug.Log($"���x�̑傫���F{myRigidbody.velocity.magnitude}");
        //Debug.Log($"�����x�F{acceleration}");
        //Debug.Log($"PM:RepawnPos:{respawnPos}");
        //Debug.Log($"x���F{transform.right}");
        //if (groundInfo.collider != null)
        //{
        //    Debug.Log($"���������I�u�W�F�N�g���F{groundInfo.collider.name}");
        //}
        //else Debug.Log("���������I�u�W�F�N�g���Fnull");
        //Debug.Log($"���X�|�[����]�F{respawnRot}");
        //Debug.Log($"����\��:{IsControllable}");
        //Debug.Log($"�O����:{forwardDirection}");
    }


    //�v���C���[���Đݒ肷��(�R���e�B�j���[�A�^�C�g���ɖ߂�����)
    public void ResetPlayer(Vector3 resetPosition, Vector3 resetRotation)
    {
        //�v���C���[�̓������~�߂�
        movingSpeed = 0;
        myRigidbody.velocity = Vector3.zero;
        acceleration = Vector3.zero;

        //�v���C���[�̈ʒu�Ɖ�]�l���Đݒ肷��
        myTransform.position = resetPosition;
        myTransform.eulerAngles = resetRotation;

        //���X�|�[���n�_���Đݒ�
        RespawnPos = resetPosition;
        RespawnRot = resetRotation;
    }
}
