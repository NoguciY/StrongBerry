using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using static PlayerManager;

public class PlayerManager : MonoBehaviour
{
    [SerializeField, Header("移動する速さ")]
    private float movingSpeed;

    [SerializeField, Header("最高速")]
    private float maxMovingSpeed;

    [SerializeField, Header("加速度(m/s*s)")]
    private float accelPerSecond;

    [SerializeField, Header("旋回力(deg/s)")]
    private float turnPerSecond;

    [SerializeField, Header("回転調整用の値")]
    private float adjustedValueOfRotation;

    [SerializeField, Header("プレイヤー本体")]
    private GameObject playerBody;

    //ゲームオーバーイベント
    public UnityEvent gameOverEvent;

    //ゲームクリアイベント
    public UnityEvent gameClearEvent;

    //スピード表示イベント
    public class DisplaySpeedEvent : UnityEvent<float> { }
    private DisplaySpeedEvent displaySpeedEvent;

    //プレイヤーのリスポーン地点
    public Vector3 RespawnPos { get; set; }

    //プレイヤーのリスポーン回転値
    public Vector3 RespawnRot { get; set; }

    //前方向
    private Vector3 forwardDirection;

    //移動用のRigidbody
    private Rigidbody myRigidbody;

    //地上にいるか
    private bool onGround;

    //加速度
    private Vector3 acceleration;

    //地面の情報を取得用
    private GetGroundInformation getGroundInformation;

    //地面の情報
    private RaycastHit groundInfo;

    //プレイヤーの初期位置
    private Vector3 initPos;

    //プレイヤーの初期回転値
    private Vector3 initRot;

    //前に進んでいるか
    private bool isMovingForward;

    //移動キーの入力値
    private float verticalInputValue;

    //前フレームの移動入力値
    private float preVerticalInputValue;

    //メインカメラ
    private Camera mainCamera;

    private Transform myTransform;

    //操作可能か
    [SerializeField]
    private bool isControllable;

    public bool IsControllable
    { 
        get { return isControllable; }
        set 
        { 
            isControllable = value;
            Debug.Log($"操作可能か:{isControllable}");
        }
    }


    //アクセサ
    public DisplaySpeedEvent GetDisplaySpeedEvent => displaySpeedEvent;
    public Vector3 GetInitPos => initPos;

    public Vector3 GetInitRot => initRot;

    public float GetMaxMovingSpeed => maxMovingSpeed;


    //GManagerのStart()より先に設定するため
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
            //地上判定
            onGround = groundInfo.collider;

            verticalInputValue = Input.GetAxisRaw("Vertical");

            float horizontalInputValue = Input.GetAxis("Horizontal");

            //旋回させる
            InputToTurn(horizontalInputValue);

            //回転させる
            VelocityToRotate();

            //デバッグ用(必要なければ消す)
            DisyplayDebugLog();  
        }
    }


    private void FixedUpdate()
    {
        if (IsControllable)
        {
            //地面の情報の取得
            groundInfo = getGroundInformation.CheckForGround();

            //前方向の取得(正規化済み)
            forwardDirection = Vector3.Cross(myTransform.right, groundInfo.normal);

            //カメラからプレイヤーのベクトル
            Vector3 playerFromMainCameraVec = myTransform.position - mainCamera.transform.position;

            //前方に進んでいるか判定
            isMovingForward = IsMovingForward(myRigidbody.velocity, playerFromMainCameraVec);

            //加速度の取得
            acceleration = InputToVelocity(verticalInputValue) / Time.fixedDeltaTime;

            //速度へ加算
            myRigidbody.AddForce(acceleration, ForceMode.Acceleration);

            //速度には上限を設ける
            if (myRigidbody.velocity.magnitude >= maxMovingSpeed)
            myRigidbody.velocity = myRigidbody.velocity.normalized * maxMovingSpeed;

            //前回の移動入力値として保存
            preVerticalInputValue = verticalInputValue;
        }

        //スピードを表示する
        displaySpeedEvent.Invoke(myRigidbody.velocity.magnitude);
    }

    /// <summary>
    /// 入力から旋回させる
    /// </summary>
    /// <param name="horizontalInputValue">水平方向のキー入力値</param>
    private void InputToTurn(float horizontalInputValue)
    {
        //プレイヤーのy軸を軸に旋回力と入力値に応じて旋回させる
        myTransform.Rotate(Vector3.up, turnPerSecond * horizontalInputValue * Time.deltaTime);
    }

    /// <summary>
    /// 速度に応じて回転させる
    /// </summary>
    private void VelocityToRotate()
    {
        //ルートの計算は負荷が高いためsqrMagnitudeを使う
        float sqrSpeed = myRigidbody.velocity.sqrMagnitude;

        //1フレームの回転角度
        float rotationAnglePerFrame = sqrSpeed * Time.deltaTime * adjustedValueOfRotation;

        //後方に進んでいる場合は逆回転する
        if (!isMovingForward)
            rotationAnglePerFrame *= -1f;

        playerBody.transform.Rotate(Vector3.forward * rotationAnglePerFrame);
    }

    //入力から速度を返す
    private Vector3 InputToVelocity(float verticalInputValue)
    {
        Vector3 movingDirection = Vector3.zero;

        //WかSキー押下時
        if (verticalInputValue != 0)
        {
            //Wキー押下時、前方に移動
            if (verticalInputValue > 0)
            {
                movingDirection = forwardDirection;
            }
            //Sキー押下時、後方に移動
            else if (verticalInputValue < 0)
            {
                movingDirection = -forwardDirection;
            }

            //前から後ろ、後ろから前に移動する場合、
            if (verticalInputValue != preVerticalInputValue)
                movingSpeed = 0;

            //速さを上げる
            movingSpeed += accelPerSecond * Time.fixedDeltaTime;
        }
        //WかSキー押下時でない場合
        else
        {
            //速さを下げる
            movingSpeed -= accelPerSecond * Time.fixedDeltaTime;
            if (movingSpeed < 0) movingSpeed = 0;
        }

        Vector3 velocity = movingDirection * movingSpeed;
        return velocity;
    }

    //当たり判定 
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
            //ゲームオーバーにする
            gameOverEvent.Invoke();
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            //プレイヤーにかかる力を0にする
            acceleration = Vector3.zero;
        }
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
            //前方に進んでいる
            isMovingForward = true;

        else if (dot < 0)
            //前方に進んでいない
            isMovingForward = false;

        return isMovingForward;
    }

    private void DisyplayDebugLog()
    {
        //Debug.Log($"地上か？:{onGround}");
        //Debug.Log($"速さ：{movingSpeed}");
        //Debug.Log($"速度：{myRigidbody.velocity}");
        //Debug.Log($"速度の大きさ：{myRigidbody.velocity.magnitude}");
        //Debug.Log($"加速度：{acceleration}");
        //Debug.Log($"PM:RepawnPos:{respawnPos}");
        //Debug.Log($"x軸：{transform.right}");
        //if (groundInfo.collider != null)
        //{
        //    Debug.Log($"当たったオブジェクト名：{groundInfo.collider.name}");
        //}
        //else Debug.Log("当たったオブジェクト名：null");
        //Debug.Log($"リスポーン回転：{respawnRot}");
        //Debug.Log($"操作可能か:{IsControllable}");
        //Debug.Log($"前方向:{forwardDirection}");
    }


    //プレイヤーを再設定する(コンティニュー、タイトルに戻った時)
    public void ResetPlayer(Vector3 resetPosition, Vector3 resetRotation)
    {
        //プレイヤーの動きを止める
        movingSpeed = 0;
        myRigidbody.velocity = Vector3.zero;
        acceleration = Vector3.zero;

        //プレイヤーの位置と回転値を再設定する
        myTransform.position = resetPosition;
        myTransform.eulerAngles = resetRotation;

        //リスポーン地点を再設定
        RespawnPos = resetPosition;
        RespawnRot = resetRotation;
    }
}
