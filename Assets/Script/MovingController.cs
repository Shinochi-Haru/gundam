using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingController : MonoBehaviour
{
    [SerializeField] float _upPower = 10f;
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
        //è„è∏Ç∑ÇÈ
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
