using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingController : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _turnSpeed = 10f;
    [SerializeField] float _jumpPower = 10f;
    [SerializeField] float _gravity = 20f;
    float moveFB, moveLR;
    private Rigidbody _rb;
    Vector3 movement;
    bool webGLRightClickRotation = true;
    [SerializeField] public GameObject cam;
    float rotX, rotY; 
    [SerializeField] public float sensitivity = 30.0f;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        if (Application.isEditor)
        {
            webGLRightClickRotation = false;                // エディタ上では WebGL での右クリック回転を無効化
            sensitivity = sensitivity * 1.5f;               // エディタ上では感度を1.5倍に増やす
        }
    }

    void Update()
    {
        moveFB = Input.GetAxis("Horizontal") * _moveSpeed;      // 水平方向（左右）の入力に基づいて移動量を計算
        moveLR = Input.GetAxis("Vertical") * _moveSpeed;        // 垂直方向（前後）の入力に基づいて移動量を計算
    }
    private void FixedUpdate()
    {
    //    // 移動する方向を決める
    //    Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

    //    // 移動する向きにキャラクターの向きを変える
    //    if (direction.magnitude >= 0.1f)
    //    {
    //        Quaternion lookRotation = Quaternion.LookRotation(direction);
    //        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _turnSpeed);
    //    }
        //// Rigidbodyに力を加える
        //Vector3 movement = direction * _moveSpeed;// * Time.deltaTime;
        //movement.y = _rb.velocity.y; // ジャンプ中のY軸方向の速度を保持
        //_rb.velocity = movement;

        movement = new Vector3(moveFB, _gravity, moveLR);   // 移動量をベクトルとして設定
        movement = transform.rotation * movement;            // 移動量をキャラクターのローカル座標系に変換
        _rb.velocity = movement;

        rotX = Input.GetAxis("Mouse X") * sensitivity;     // マウスの X 軸移動量に基づいて横回転量を計算
        rotY = Input.GetAxis("Mouse Y") * sensitivity;     // マウスの Y 軸移動量に基づいて縦回転量を計算

        if (webGLRightClickRotation)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                CameraRotation(cam, rotX, rotY);             // WebGL での右クリック回転が有効な場合、マウスの移動量に基づいてカメラを回転
            }
        }
        else if (!webGLRightClickRotation)
        {
            CameraRotation(cam, rotX, rotY);                 // WebGL での右クリック回転が無効な場合、常にマウスの移動量に基づいてカメラを回転
        }
        //// ジャンプ
        //if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        //{
        //    _rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
        //}
    }
    void CameraRotation(GameObject cam, float rotX, float rotY)
    {
        transform.Rotate(0, rotX * Time.deltaTime, 0);       // キャラクターを横回転
        //cam.transform.Rotate(-rotY * Time.deltaTime, 0, 0);  // カメラを縦回転
    }
    bool IsGrounded()
    {
        RaycastHit hit;
        float distance = 0.1f;
        Vector3 dir = Vector3.down;

        if (Physics.Raycast(transform.position, dir, out hit, distance))
        {
            return true;
        }

        return false;
    }
}
