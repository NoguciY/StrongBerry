using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGroundInformation : MonoBehaviour
{
    [SerializeField, Header("地面のレイヤー")] 
    private LayerMask groundMask;

    [SerializeField, Header("スフィアキャストの最大距離")]
    private float maxCastDistance;

    //プレイヤーのコライダー
    private CapsuleCollider capsuleCollider;

    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    //地面を検出する
    public RaycastHit CheckForGround()
    {
        //球状のレイが最初から地面に接している場合、衝突判定が行われないため
        //Rayの原点は中心座標より0.1上にする
        Vector3 origin = transform.position + Vector3.up * 0.1f;

        RaycastHit hitInfo;
        Ray sphereCastRay = new Ray(origin, Vector3.down);
        Physics.SphereCast(sphereCastRay, capsuleCollider.radius, out hitInfo, 
            maxCastDistance, groundMask);

        return hitInfo;
    }

    //スフィアキャストのスフィアを可視化する
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1, 0.5f);

        Gizmos.DrawSphere(transform.position + Vector3.up * 0.1f, 0.8f);
    }
}
