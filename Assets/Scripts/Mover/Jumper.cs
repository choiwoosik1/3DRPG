using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] Rigidbody _rigid;
    [SerializeField] float _jumpPower;
    bool _isJump = false;

    private void Start()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !_isJump)
        {
            _isJump = true;
            _rigid.AddForce(new Vector3(0, _jumpPower, 0), ForceMode.Impulse);
        }
    }
}
