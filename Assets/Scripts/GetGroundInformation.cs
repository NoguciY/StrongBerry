using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGroundInformation : MonoBehaviour
{
    //全てのレイヤー
    [SerializeField] 
    private LayerMask groundMask = ~0;

    //プレイヤーのコライダー
    private CapsuleCollider capsuleCollider;

    //地面との距離(スフィアをキャストする距離)
    [SerializeField]
    private float groundDistance = 0.14f;

    // Start is called before the first frame update
    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    //地面を検出する
    public RaycastHit CheckForGround()
    {
        //Rayの原点は中心座標
        Vector3 origin = transform.position + Vector3.up * 0.1f;

        RaycastHit hitInfo;
        Ray sphereCastRay = new Ray(origin, Vector3.down);
        Physics.SphereCast(sphereCastRay, capsuleCollider.radius, out hitInfo, 
            groundDistance, groundMask);

        return hitInfo;
    }

    //地上判定をする
    public bool IsOnGround()
    {
        RaycastHit hitInfo;
        return Physics.SphereCast(transform.position + Vector3.up * 0.1f, 
            capsuleCollider.radius, Vector3.down, out hitInfo, groundDistance, groundMask);
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = new Color(0, 0, 1, 0.5f);
    //    //Gizmos.DrawRay(capsuleCollider.center, Vector3.down);

    //    //スフィアキャストのスフィアを可視化する
    //    //0.8 = capsuleCollider.radius
    //    Gizmos.DrawSphere(transform.position, 1.34f);
    //}
}
