using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGroundInformation : MonoBehaviour
{
    //�S�Ẵ��C���[
    [SerializeField] 
    private LayerMask groundMask = ~0;

    //�v���C���[�̃R���C�_�[
    private CapsuleCollider capsuleCollider;

    //�n�ʂƂ̋���(�X�t�B�A���L���X�g���鋗��)
    [SerializeField]
    private float groundDistance = 0.14f;

    // Start is called before the first frame update
    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    //�n�ʂ����o����
    public RaycastHit CheckForGround()
    {
        //Ray�̌��_�͒��S���W
        Vector3 origin = transform.position + Vector3.up * 0.1f;

        RaycastHit hitInfo;
        Ray sphereCastRay = new Ray(origin, Vector3.down);
        Physics.SphereCast(sphereCastRay, capsuleCollider.radius, out hitInfo, 
            groundDistance, groundMask);

        return hitInfo;
    }

    //�n�㔻�������
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

    //    //�X�t�B�A�L���X�g�̃X�t�B�A����������
    //    //0.8 = capsuleCollider.radius
    //    Gizmos.DrawSphere(transform.position, 1.34f);
    //}
}
