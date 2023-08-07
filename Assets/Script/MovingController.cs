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
            webGLRightClickRotation = false;                // �G�f�B�^��ł� WebGL �ł̉E�N���b�N��]�𖳌���
            sensitivity = sensitivity * 1.5f;               // �G�f�B�^��ł͊��x��1.5�{�ɑ��₷
        }
    }

    void Update()
    {
        moveFB = Input.GetAxis("Horizontal") * _moveSpeed;      // ���������i���E�j�̓��͂Ɋ�Â��Ĉړ��ʂ��v�Z
        moveLR = Input.GetAxis("Vertical") * _moveSpeed;        // ���������i�O��j�̓��͂Ɋ�Â��Ĉړ��ʂ��v�Z
    }
    private void FixedUpdate()
    {
    //    // �ړ�������������߂�
    //    Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

    //    // �ړ���������ɃL�����N�^�[�̌�����ς���
    //    if (direction.magnitude >= 0.1f)
    //    {
    //        Quaternion lookRotation = Quaternion.LookRotation(direction);
    //        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _turnSpeed);
    //    }
        //// Rigidbody�ɗ͂�������
        //Vector3 movement = direction * _moveSpeed;// * Time.deltaTime;
        //movement.y = _rb.velocity.y; // �W�����v����Y�������̑��x��ێ�
        //_rb.velocity = movement;

        movement = new Vector3(moveFB, _gravity, moveLR);   // �ړ��ʂ��x�N�g���Ƃ��Đݒ�
        movement = transform.rotation * movement;            // �ړ��ʂ��L�����N�^�[�̃��[�J�����W�n�ɕϊ�
        _rb.velocity = movement;

        rotX = Input.GetAxis("Mouse X") * sensitivity;     // �}�E�X�� X ���ړ��ʂɊ�Â��ĉ���]�ʂ��v�Z
        rotY = Input.GetAxis("Mouse Y") * sensitivity;     // �}�E�X�� Y ���ړ��ʂɊ�Â��ďc��]�ʂ��v�Z

        if (webGLRightClickRotation)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                CameraRotation(cam, rotX, rotY);             // WebGL �ł̉E�N���b�N��]���L���ȏꍇ�A�}�E�X�̈ړ��ʂɊ�Â��ăJ��������]
            }
        }
        else if (!webGLRightClickRotation)
        {
            CameraRotation(cam, rotX, rotY);                 // WebGL �ł̉E�N���b�N��]�������ȏꍇ�A��Ƀ}�E�X�̈ړ��ʂɊ�Â��ăJ��������]
        }
        //// �W�����v
        //if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        //{
        //    _rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
        //}
    }
    void CameraRotation(GameObject cam, float rotX, float rotY)
    {
        transform.Rotate(0, rotX * Time.deltaTime, 0);       // �L�����N�^�[������]
        //cam.transform.Rotate(-rotY * Time.deltaTime, 0, 0);  // �J�������c��]
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
