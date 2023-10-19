using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
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

    void Start()
    {
        _line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new(_muzzle.position, this.transform.forward);   // muzzle から正面に ray を飛ばす

        if (Physics.Raycast(ray, out RaycastHit hit, _maxFireDistance))
        {
            _rayCastHitPosition = hit.point;
            DrawLaser(_rayCastHitPosition);
            if (Input.GetButtonDown("Fire1"))
            {
                Fire1(_rayCastHitPosition);

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
        Vector3[] positions = { _muzzle.position, destination };   //レーザーの始点は常に Muzzle にする
        _line.positionCount = positions.Length;   // Lineを終点と始点のみに制限する
        _line.SetPositions(positions);
    }
}
