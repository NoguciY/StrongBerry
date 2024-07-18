using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGroundInformation : MonoBehaviour
{
    [SerializeField, Header("�n�ʂ̃��C���[")] 
    private LayerMask groundMask;

    [SerializeField, Header("�X�t�B�A�L���X�g�̍ő勗��")]
    private float maxCastDistance;

    //�v���C���[�̃R���C�_�[
    private CapsuleCollider capsuleCollider;

    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    //�n�ʂ����o����
    public RaycastHit CheckForGround()
    {
        //����̃��C���ŏ�����n�ʂɐڂ��Ă���ꍇ�A�Փ˔��肪�s���Ȃ�����
        //Ray�̌��_�͒��S���W���0.1��ɂ���
        Vector3 origin = transform.position + Vector3.up * 0.1f;

        RaycastHit hitInfo;
        Ray sphereCastRay = new Ray(origin, Vector3.down);
        Physics.SphereCast(sphereCastRay, capsuleCollider.radius, out hitInfo, 
            maxCastDistance, groundMask);

        return hitInfo;
    }

    //�X�t�B�A�L���X�g�̃X�t�B�A����������
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1, 0.5f);

        Gizmos.DrawSphere(transform.position + Vector3.up * 0.1f, 0.8f);
    }
}
