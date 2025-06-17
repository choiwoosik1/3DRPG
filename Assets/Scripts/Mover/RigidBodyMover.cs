using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidBodyMover : Mover
{
    /// <summary>
    /// 회전 속력
    /// </summary>
    [SerializeField] float _rotSpeed;   // 회전 속력

    Rigidbody _rigid;               // 리지드바디 참조
    Vector3 _velocity;              // 이동 속도 벡터
    Quaternion _targetRotation;     // 현재 목표 회전값


    public override event Action<Vector3> OnMoved;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _targetRotation = _rigid.rotation;
    }

    private void FixedUpdate()
    {
        // 리지드바디의 y방향 속력값 그대로 유지
        _velocity.y = _rigid.velocity.y;

        // 리지드바디에 원하는 속도 벡터 정용
        _rigid.velocity = _velocity;

        // 리지드바디에 목표 회전값에 따른 부드러운 회전 적용
        _rigid.rotation = Quaternion.RotateTowards(
            _rigid.rotation,
            _targetRotation,
            _rotSpeed * Time.fixedDeltaTime);

        _velocity.y = 0;
        OnMoved?.Invoke(_velocity);
    }

    public override void Move(Vector3 direction)
    {
        // y 방향 무시
        direction.y = 0;

        if(direction.magnitude < Utils.Epsilon)
        {
            // 현재 속도를 0으로 설정
            _velocity = Vector3.zero;
        }
        else
        {
            // 현재 속도값 설정
            _velocity = direction.normalized * _moveSpeed;

            // 목표 회전값 설정
            _targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        }
    }
}
