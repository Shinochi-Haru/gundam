using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingController : MonoBehaviour
{
    [SerializeField] float _upPower = 10f;
    [SerializeField] float _horizonPower = 10f;
    private Vector3 moveDirection; // �L�����N�^�[�̈ړ�����

    Rigidbody _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // �㉺���E�̃L�[�̓��͂��擾
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        // �ړ�������������߂�
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // �ړ���������ɃL�����N�^�[�̌�����ς���
        if (direction.magnitude >= 0.1f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
        }

        // Rigidbody�ɗ͂�������
        Vector3 movement = direction * _horizonPower * Time.deltaTime;
        _rb.AddForce(movement, ForceMode.VelocityChange);
        //�㏸����
        if (Input.GetKey(KeyCode.Space))
        {
            _rb.AddForce(Vector3.up * _upPower);
        }
        else
        {
            _rb.velocity = Vector3.zero;
        }
    }
}
