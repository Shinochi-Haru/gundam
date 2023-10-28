using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    private enum State
    {
        LongRangeAtttck, // �������U��
        CloseRangeAttack // �ߋ����U��
    }
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
    private State _state = State.CloseRangeAttack;
    [SerializeField] Transform _closeRangeMazzle = null;
    [SerializeField] float _maxCloseDistance = 10f;

    void Start()
    {
        _line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        switch (_state)
        {
            case State.LongRangeAtttck: // �������U��
                UpdateLongRangeState();
                break;
            case State.CloseRangeAttack: //�@�ߋ����U��
                UpdatCloseRangeState();
                break;
        }
    }

    void UpdateLongRangeState()
    {
        LongRangeAtttckRay();
    }

    void UpdatCloseRangeState()
    {
        CloseRangeAttackRay();
    }
    void LongRangeAtttckRay()
    {
        Camera mainCamera = Camera.main;
        Vector3 cameraPosition = mainCamera.transform.position;
        Vector3 cameraForward = mainCamera.transform.forward;

        // �J�����̒��� ���琳�ʂ� ray ���΂�
        Ray ray = new Ray(cameraPosition, cameraForward);

        if (Physics.Raycast(ray, out RaycastHit hit, _maxFireDistance))
        {
            _rayCastHitPosition = hit.point;
            DrawLaser(_rayCastHitPosition);
            if (Input.GetButtonDown("Fire1"))
            {
                LongRangeFire1(_rayCastHitPosition);
            }
        }
    }
    void LongRangeFire1(Vector3 _rayCastHitPosition)
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

    void CloseRangeAttackRay()
    {
        Vector3 origin = _closeRangeMazzle.position;
        Vector3 direction = _closeRangeMazzle.up;
        Ray ray = new Ray(origin, direction);

        _line.SetPosition(0, origin);
        _line.SetPosition(1, origin + direction * _maxCloseDistance); // �K���Ȓ����ɐݒ�
    }
    void CloseRangeFire1()
    {

    }
}
