using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    //移動する向き
    private Vector3 forwardDirection;

    //移動する速さ
    [SerializeField]
    private float movingSpeed;

    //加速力(km/h*s)
    [SerializeField] 
    private float accelPerSecond;                  

    //最高速
    [SerializeField] 
    private float maxMovingSpeed;

    //旋回力(deg/s)
    [SerializeField]
    private float turnPerSecond;

    //回転調整用の値
    [SerializeField]
    private float adjustedValueOfRotation;

    //ゲームオーバー時イベント
    [SerializeField]
    private UnityEvent gameOverEvent = new UnityEvent();

    //プレイヤー本体
    [SerializeField]
    private GameObject playerBody;

    //移動用のRigidbody
    private Rigidbody rb;

    //地上にいるか
    private bool onGround = false;

    //加速度
    private Vector3 acceleration;

    //地面の情報を取得用
    private GetGroundInformation getGroundInformation;

    //地面の情報
    private RaycastHit groundInfo;

    //プレイヤーの初期位置
    private Vector3 initPos;

    //プレイヤーのリスポーン地点
    public Vector3 respawnPos;

    //プレイヤーの初期回転値
    private Vector3 initRot;

    //プレイヤーのリスポーン回転値
    public Vector3 respawnRot;

    //前に進んでいるか
    private bool isMovingForward;

    //移動キーの入力値
    private float verticalInput;

    //前フレームの移動入力値
    private float preVerticalInput;

    //メインカメラ
    private Camera mainCamera;


    //アクセサ
    public Vector3 GetInitPos => initPos;

    public Vector3 GetPos => transform.position;

    public Vector3 GetInitRot => initRot;


    //GManagerのStart()より先に設定するため
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
        //地上判定
        onGround = groundInfo.collider;

        verticalInput = Input.GetAxisRaw("Vertical");

        //旋回させる
        InputToTurn();

        //回転させる
        VelocityToRotate();

        //デバッグ用(必要なければ消す)
        DisyplayDebugLog();

        //前回の移動入力値として保存
        preVerticalInput = verticalInput;
    }


    private void FixedUpdate()
    {
        //地面の情報の取得
        groundInfo = getGroundInformation.CheckForGround();

        //前方向の取得(正規化済み)
        forwardDirection = Vector3.Cross(transform.right, groundInfo.normal);

        //カメラからプレイヤーのベクトル
        Vector3 playerFromMainCameraVec = this.transform.position - mainCamera.transform.position;

        //前方に進んでいるか判定
        isMovingForward = IsMovingForward(rb.velocity, playerFromMainCameraVec);

        //加速度の取得
        acceleration = InputToVelocity();

        if (onGround)
        {
            //速度へ加算
            //acceleration = InputToAcceleration();
            rb.AddForce(acceleration * 2, ForceMode.Acceleration);
        }
    }


    /// <summary>
    /// 速度に加算する加速度(ベクトル)を返す
    /// </summary>
    private Vector3 InputToAcceleration()
    {
        //進行方向
        Vector3 movingDirection = Vector3.zero;

        if (verticalInput != 0)
        {
            movingDirection = forwardDirection * verticalInput;
        }

        //1フレームごとの加速度 → 入力してない場合、現在の速度が最大速を超えた場合
        //                         マイナスの値になる
        float accelerationPerFrame = (maxMovingSpeed * movingDirection.magnitude - rb.velocity.magnitude)
                                    / Time.fixedDeltaTime;

        //加速度   (入力していない場合、加速度は０になってしまうので減速がいまいち)
        Vector3 acceleration = movingDirection * accelerationPerFrame;
        return acceleration;
    }


    //入力から速度を返す(速度ではなく、力を返したい、　最高速を設定しているが意味ない)
    private Vector3 InputToVelocity()
    {
        Vector3 direction = Vector3.zero;

        //WかSキー押下時
        if (verticalInput != 0)
        {
            direction = forwardDirection * verticalInput;

            movingSpeed += accelPerSecond * Time.deltaTime;

            //速さは最大速より大きくしない
            if (movingSpeed > maxMovingSpeed) movingSpeed = maxMovingSpeed;
        }
        //WかSキー押下時でない場合
        else
        {
            //減速
            movingSpeed -= accelPerSecond * Time.deltaTime;
            if (movingSpeed < 0) movingSpeed = 0;
        }

        Vector3 velocity = direction.normalized * movingSpeed;
        return velocity;
    }


    //入力から旋回させる
    private void InputToTurn()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            //プレイヤーのy軸を軸に旋回力と入力値に応じて旋回させる
            transform.Rotate(Vector3.up,
                turnPerSecond * Input.GetAxis("Horizontal") * Time.deltaTime);
        }
    }

    //速度に応じて回転させる
    private void VelocityToRotate()
    {
        //ルートの計算は負荷が高いためsqrMagnitudeを使う
        float sqrSpeed = rb.velocity.sqrMagnitude;

        //1フレームの回転角
        float rotationAnglePerFrame = sqrSpeed * Time.deltaTime * adjustedValueOfRotation;

        //後方に進んでいる場合は回転を逆にする
        if (!isMovingForward)
            rotationAnglePerFrame *= -1f;
        
        playerBody.transform.Rotate(Vector3.forward * rotationAnglePerFrame);
    }


    //当たり判定 
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
            //ゲームオーバーにする
            gameOverEvent.Invoke();
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            //プレイヤーにかかる力を0にする
            acceleration = Vector3.zero;
        }
    }


    private void DisyplayDebugLog()
    {
        //Debug.Log($"地上か？:{onGround}");
        //Debug.Log($"進行方向：{movingDirection}");
        //Debug.Log($"速さ：{movingSpeed}");
        //Debug.Log($"速度：{rb.velocity}");
        //Debug.Log($"速度の大きさ：{rb.velocity.magnitude}");
        //Debug.Log($"加速度：{acceleration}");
        //Debug.Log($"PM:RepawnPos:{respawnPos}");
        //Debug.Log($"x軸：{transform.right}");
        //if (groundInfo.collider != null)
        //{
        //    Debug.Log($"当たったオブジェクト名：{groundInfo.collider.name}");
        //}
        //else Debug.Log("当たったオブジェクト名：null");
        //Debug.Log($"リスポーン回転：{respawnRot}");
    }


    //プレイヤーの動きを止める
    public void StopPlayerMovement()
    {
        rb.velocity = Vector3.zero;
        acceleration = Vector3.zero;
    }


    /// <summary>
    /// カメラのプレイヤーを向いている方向と進行方向の内積から
    /// 前方に進んでいるか判別する
    /// </summary>
    /// <returns>true:前方に進んでいる, false:前方に進んでいない</returns>
    private bool IsMovingForward(Vector3 firstVec, Vector3 secondVec)
    {
        //前方に進んでいるか
        bool isMovingForward = true;

        //引数のベクトル同士で内積を求める
        float dot = Vector3.Dot(firstVec, secondVec);
        
        if (dot > 0)
        {
            //前方に進んでいる
            isMovingForward = true;
        }
        else if (dot < 0)
        {
            //前方に進んでいない
            isMovingForward = false;
        }

        return isMovingForward;
    }
}
