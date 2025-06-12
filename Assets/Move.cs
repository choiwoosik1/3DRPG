using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] Rigidbody _rigid;
    [SerializeField] float _moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal") * _moveSpeed;
        float v = Input.GetAxisRaw("Vertical") * _moveSpeed;

        _rigid.AddForce(new Vector3(h, 0, v), ForceMode.Impulse);
    }
}
