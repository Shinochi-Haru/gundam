using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    LineRenderer _line = default;
    [SerializeField] Transform _muzzle = null;
    Vector3 _rayCastHitPosition;
    [SerializeField] float _maxFireDistance = 100f;
    /// <summary>�����̔��a </summary>
    [SerializeField] float radius = 5.0F;
    /// <summary>�����̏�ɓ������� </summary>
    [SerializeField] float uppower = 10.0F;
    /// <summary>�悱�ɓ����� </summary>
    [SerializeField] float _widthpower = 0;
    /// <summary> ���j�̉e�����󂯂Ȃ�/// </summary>
    [SerializeField] int _wallLayer = 0;
    private RaycastHit hit;//�BRay�������ɓ����������̏��
    public GameObject Muzzle;//�BRay�𔭎˂���ꏊ
    private int distance = 10000;//�BRay���΂�����
    void Start()
    {
        _line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
    //    Ray ray = new(_muzzle.position, this.transform.forward);   // muzzle ���琳�ʂ� ray ���΂�

    //    if (Physics.Raycast(ray, out RaycastHit hit, _maxFireDistance))
    //    {
    //        _rayCastHitPosition = hit.point;
    //        DrawLaser(_rayCastHitPosition);
    //        if (Input.GetButtonDown("Fire1"))
    //        {
    //            Fire1(_rayCastHitPosition);

    //        }
    //    }

        if (Input.GetMouseButton(0))//�B�}�E�X�����N���b�N���ꂽ�� 
        {
            Ray ray = new Ray(Muzzle.transform.position, Muzzle.transform.forward);//�BRay��Muzzle�̏ꏊ����O���ɔ�΂�
            Debug.DrawRay(ray.origin, ray.direction, Color.red);//�BRay��ԐF�ŕ\��������

            if (Physics.Raycast(ray, out hit, distance))
            {//�BRay��distance�͈͓̔��ŉ����ɓ�����������

                if (hit.collider.tag == "Enemy")//�B���������������̃^�O��Enemy�������� 
                {
                    Destroy(hit.collider.gameObject);//�B�������������������āA
                    //Instantiate(Explosion.gameObject, hit.collider.gameObject.transform.position, gameObject.transform.rotation);//�BRay�����������ꏊ�ɔ����𐶐�����
                }
            }
        }
    }

    void Fire1(Vector3 _rayCastHitPosition)
    {
        //_explosionObject.Explode(_rayCastHitPosition);
        Collider[] colliders = Physics.OverlapSphere(_rayCastHitPosition, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(uppower, _rayCastHitPosition, radius, _widthpower, ForceMode.Impulse);
        }
    }
    void DrawLaser(Vector3 destination)
    {
        Vector3[] positions = { _muzzle.position, destination };   //���[�U�[�̎n�_�͏�� Muzzle �ɂ���
        _line.positionCount = positions.Length;   // Line���I�_�Ǝn�_�݂̂ɐ�������
        _line.SetPositions(positions);
    }
}
