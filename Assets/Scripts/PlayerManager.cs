using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    //�ړ��������
    private Vector3 forwardDirection;

    //�ړ����鑬��
    [SerializeField]
    private float movingSpeed;

    //������(km/h*s)
    [SerializeField] 
    private float accelPerSecond;                  

    //�ō���
    [SerializeField] 
    private float maxMovingSpeed;

    //�����(deg/s)
    [SerializeField]
    private float turnPerSecond;

    //��]�����p�̒l
    [SerializeField]
    private float adjustedValueOfRotation;

    //�Q�[���I�[�o�[���C�x���g
    [SerializeField]
    private UnityEvent gameOverEvent = new UnityEvent();

    //�v���C���[�{��
    [SerializeField]
    private GameObject playerBody;

    //�ړ��p��Rigidbody
    private Rigidbody rb;

    //�n��ɂ��邩
    private bool onGround = false;

    //�����x
    private Vector3 acceleration;

    //�n�ʂ̏����擾�p
    private GetGroundInformation getGroundInformation;

    //�n�ʂ̏��
    private RaycastHit groundInfo;

    //�v���C���[�̏����ʒu
    private Vector3 initPos;

    //�v���C���[�̃��X�|�[���n�_
    public Vector3 respawnPos;

    //�v���C���[�̏�����]�l
    private Vector3 initRot;

    //�v���C���[�̃��X�|�[����]�l
    public Vector3 respawnRot;

    //�O�ɐi��ł��邩
    private bool isMovingForward;

    //�ړ��L�[�̓��͒l
    private float verticalInput;

    //�O�t���[���̈ړ����͒l
    private float preVerticalInput;

    //���C���J����
    private Camera mainCamera;


    //�A�N�Z�T
    public Vector3 GetInitPos => initPos;

    public Vector3 GetPos => transform.position;

    public Vector3 GetInitRot => initRot;


    //GManager��Start()����ɐݒ肷�邽��
    private void Awake()
    {
        initPos = transform.position;
        respawnPos = initPos;
        initRot = transform.localEulerAngles;
        respawnRot = initRot;
    }


    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        getGroundInformation = GetComponent<GetGroundInformation>();
        mainCamera = Camera.main;
    }


    // Update is called once per frame
    private void Update()
    {
        //�n�㔻��
        onGround = groundInfo.collider;

        verticalInput = Input.GetAxisRaw("Vertical");

        //���񂳂���
        InputToTurn();

        //��]������
        VelocityToRotate();

        //�f�o�b�O�p(�K�v�Ȃ���Ώ���)
        DisyplayDebugLog();

        //�O��̈ړ����͒l�Ƃ��ĕۑ�
        preVerticalInput = verticalInput;
    }


    private void FixedUpdate()
    {
        //�n�ʂ̏��̎擾
        groundInfo = getGroundInformation.CheckForGround();

        //�O�����̎擾(���K���ς�)
        forwardDirection = Vector3.Cross(transform.right, groundInfo.normal);

        //�J��������v���C���[�̃x�N�g��
        Vector3 playerFromMainCameraVec = this.transform.position - mainCamera.transform.position;

        //�O���ɐi��ł��邩����
        isMovingForward = IsMovingForward(rb.velocity, playerFromMainCameraVec);

        //�����x�̎擾
        acceleration = InputToVelocity();

        if (onGround)
        {
            //���x�։��Z
            //acceleration = InputToAcceleration();
            rb.AddForce(acceleration * 2, ForceMode.Acceleration);
        }
    }


    /// <summary>
    /// ���x�ɉ��Z��������x(�x�N�g��)��Ԃ�
    /// </summary>
    private Vector3 InputToAcceleration()
    {
        //�i�s����
        Vector3 movingDirection = Vector3.zero;

        if (verticalInput != 0)
        {
            movingDirection = forwardDirection * verticalInput;
        }

        //1�t���[�����Ƃ̉����x �� ���͂��ĂȂ��ꍇ�A���݂̑��x���ő呬�𒴂����ꍇ
        //                         �}�C�i�X�̒l�ɂȂ�
        float accelerationPerFrame = (maxMovingSpeed * movingDirection.magnitude - rb.velocity.magnitude)
                                    / Time.fixedDeltaTime;

        //�����x   (���͂��Ă��Ȃ��ꍇ�A�����x�͂O�ɂȂ��Ă��܂��̂Ō��������܂���)
        Vector3 acceleration = movingDirection * accelerationPerFrame;
        return acceleration;
    }


    //���͂��瑬�x��Ԃ�(���x�ł͂Ȃ��A�͂�Ԃ������A�@�ō�����ݒ肵�Ă��邪�Ӗ��Ȃ�)
    private Vector3 InputToVelocity()
    {
        Vector3 direction = Vector3.zero;

        //W��S�L�[������
        if (verticalInput != 0)
        {
            direction = forwardDirection * verticalInput;

            movingSpeed += accelPerSecond * Time.deltaTime;

            //�����͍ő呬���傫�����Ȃ�
            if (movingSpeed > maxMovingSpeed) movingSpeed = maxMovingSpeed;
        }
        //W��S�L�[�������łȂ��ꍇ
        else
        {
            //����
            movingSpeed -= accelPerSecond * Time.deltaTime;
            if (movingSpeed < 0) movingSpeed = 0;
        }

        Vector3 velocity = direction.normalized * movingSpeed;
        return velocity;
    }


    //���͂�����񂳂���
    private void InputToTurn()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            //�v���C���[��y�������ɐ���͂Ɠ��͒l�ɉ����Đ��񂳂���
            transform.Rotate(Vector3.up,
                turnPerSecond * Input.GetAxis("Horizontal") * Time.deltaTime);
        }
    }

    //���x�ɉ����ĉ�]������
    private void VelocityToRotate()
    {
        //���[�g�̌v�Z�͕��ׂ���������sqrMagnitude���g��
        float sqrSpeed = rb.velocity.sqrMagnitude;

        //1�t���[���̉�]�p
        float rotationAnglePerFrame = sqrSpeed * Time.deltaTime * adjustedValueOfRotation;

        //����ɐi��ł���ꍇ�͉�]���t�ɂ���
        if (!isMovingForward)
            rotationAnglePerFrame *= -1f;
        
        playerBody.transform.Rotate(Vector3.forward * rotationAnglePerFrame);
    }


    //�����蔻�� 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            var item = other.gameObject.GetComponent<IItemController>();
            item.ExcuteItemAbility(this);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DeadObj"))
        {
            //�Q�[���I�[�o�[�ɂ���
            gameOverEvent.Invoke();
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            //�v���C���[�ɂ�����͂�0�ɂ���
            acceleration = Vector3.zero;
        }
    }


    private void DisyplayDebugLog()
    {
        //Debug.Log($"�n�ォ�H:{onGround}");
        //Debug.Log($"�i�s�����F{movingDirection}");
        //Debug.Log($"�����F{movingSpeed}");
        //Debug.Log($"���x�F{rb.velocity}");
        //Debug.Log($"���x�̑傫���F{rb.velocity.magnitude}");
        //Debug.Log($"�����x�F{acceleration}");
        //Debug.Log($"PM:RepawnPos:{respawnPos}");
        //Debug.Log($"x���F{transform.right}");
        //if (groundInfo.collider != null)
        //{
        //    Debug.Log($"���������I�u�W�F�N�g���F{groundInfo.collider.name}");
        //}
        //else Debug.Log("���������I�u�W�F�N�g���Fnull");
        //Debug.Log($"���X�|�[����]�F{respawnRot}");
    }


    //�v���C���[�̓������~�߂�
    public void StopPlayerMovement()
    {
        rb.velocity = Vector3.zero;
        acceleration = Vector3.zero;
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
        {
            //�O���ɐi��ł���
            isMovingForward = true;
        }
        else if (dot < 0)
        {
            //�O���ɐi��ł��Ȃ�
            isMovingForward = false;
        }

        return isMovingForward;
    }
}
