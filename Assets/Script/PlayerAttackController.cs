using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    private enum State
    {
        LongRangeAtttck, // 遠距離攻撃
        CloseRangeAttack // 近距離攻撃
    }
    LineRenderer _line = default;
    [SerializeField] Transform _muzzle = null;
    Vector3 _rayCastHitPosition;
    [SerializeField] float _maxFireDistance = 100f;
    /// <summary>爆発の半径 </summary>
    [SerializeField] float radius = 5.0F;
    /// <summary>爆発の上に働く強さ </summary>
    [SerializeField] float uppower = 10.0F;
    /// <summary>よこに働く力 </summary>
    [SerializeField] float _widthpower = 0;
    /// <summary> 爆破の影響を受けない/// </summary>
    [SerializeField] int _wallLayer = 0;
    private State _state = State.CloseRangeAttack;
    [SerializeField] Transform _closeRangeMazzle = null;
    [SerializeField] float _maxCloseDistance = 10f;
    bool isRotating = false;
    [SerializeField]  private GameObject targetObject;

    void Start()
    {
        _line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        switch (_state)
        {
            case State.LongRangeAtttck: // 遠距離攻撃
                UpdateLongRangeState();
                break;
            case State.CloseRangeAttack: //　近距離攻撃
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
        CloseRangeFire1();
    }
    void LongRangeAtttckRay()
    {
        Camera mainCamera = Camera.main;
        Vector3 cameraPosition = mainCamera.transform.position;
        Vector3 cameraForward = mainCamera.transform.forward;

        // カメラの中央 から正面に ray を飛ばす
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
        Vector3[] positions = { _muzzle.position, destination };   //レーザーの始点は常に Muzzle にする
        _line.positionCount = positions.Length;   // Lineを終点と始点のみに制限する
        _line.SetPositions(positions);
    }

    void CloseRangeAttackRay()
    {
        Vector3 origin = _closeRangeMazzle.position;
        Vector3 direction = _closeRangeMazzle.up;
        Ray ray = new Ray(origin, direction);

        _line.SetPosition(0, origin);
        _line.SetPosition(1, origin + direction * _maxCloseDistance); // 適当な長さに設定
    }
    void CloseRangeFire1()
    {
        if (Input.GetMouseButtonDown(0) && !isRotating)
        {
            isRotating = true;

            // カメラの方向を取得
            Vector3 lookDir = Camera.main.transform.forward;

            // 上下の回転は必要ないので、y軸の回転だけ0に設定
            lookDir.y = 0;

            // カメラの方向にオブジェクトを向ける
            targetObject.transform.LookAt(targetObject.transform.position + lookDir, Vector3.up);

            // オブジェクトを前に倒す
            targetObject.transform.rotation = Quaternion.Euler(90, targetObject.transform.eulerAngles.y, 0);
            // x軸を中心に-90度回転し、y軸の回転はそのまま
        }

        else if (Input.GetMouseButtonUp(0) && isRotating)
        {
            isRotating = false;

            // オブジェクトを元に戻す
            targetObject.transform.rotation = Quaternion.identity; // 回転をリセット
        }
    }

}
