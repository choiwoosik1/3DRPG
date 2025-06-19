using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public enum JumpState
{
    Grounded,       // 지면에 닿아있는 상태
    Jumping,       // 점프하고 있는 상태
    Falling         // 떨어지고 있는 상태
}

/// <summary>
/// 캐릭터의 점프를 담당하는 클래스
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class Jumper : MonoBehaviour
{
    [SerializeField] float _jumpPower;              // 점프력
    [SerializeField] LayerMask _groundLayerMask;    // 지면 레이어 마스크
    [SerializeField] Transform _groundChecker;      // 지면 체크용 트랜스폼
    [SerializeField] float _groundCheckRadius;      // 지면 체크 반경

    /// <summary>
    /// 점프 상태 변경 이벤트
    /// </summary>
    public event Action<JumpState> OnStateChanged;

    Rigidbody _rigid;       // Rigidbody 참조
    JumpState _state = JumpState.Grounded;       // 현재 점프 상태

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        // Physics.CheckSphere() : 원하는 위치에 가상의 구를 만들어서 겹치는 Collider가 있으면 true, 없으면 false를 반환
        bool isGrounded = Physics.CheckSphere(
            _groundChecker.position,
            _groundCheckRadius,
            _groundLayerMask.value);

        // Y 방향 속력
        float velocityY = _rigid.velocity.y;

        // 점프 중인 상태인 경우
        if(_state == JumpState.Jumping)
        {
            // 최초로 Y 방향 속력이 음수가 되면
            if(velocityY < 0)
            {
                // 추락 중 상태로 변경
                ChangeState(JumpState.Falling);
            }
        }

        // 추락 중 상태인 경우
        else if(_state == JumpState.Falling)
        {
            // 최초로 지면에 닿으면
            if(isGrounded == true)
            {
                // 착지 중 상태로 변경
                ChangeState(JumpState.Grounded);
            }
        }

        // 착지 중 상태인 경우
        else //(_state == JumpState.Grounded)
        {
            // 최초로 지면에 닿지 않게 되면
            if(isGrounded == false)
            {
                // 추락 중 상태로 변경
                ChangeState(JumpState.Falling);
            }
        }
    }

    /// <summary>
    /// 점프 동작을 실행하는 함수
    /// </summary>
    public void Jump()
    {
        // 착지해 있는 상태가 아니면 리턴
        if (_state != JumpState.Grounded) return;

        // Y 방향 속력 초기화
        Vector3 velocity = _rigid.velocity;
        velocity.y = 0;
        _rigid.velocity = velocity;

        // 점프 힘을 준다
        _rigid.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);

        // 상태 전환
        ChangeState(JumpState.Jumping);

    }

    void ChangeState(JumpState state)
    {
        // 현재 상태가 새 상태와 동일하면 리턴
        if (_state == state) return;

        else if (state == JumpState.Jumping)
        {
            Debug.Log("점프 !");
        }

        else if (state == JumpState.Falling)
        {
            Debug.Log("추락 시작...");
        }

        else if (state == JumpState.Grounded) 
        {
            Debug.Log("착지 !");
        }
        _state = state;
        OnStateChanged?.Invoke(state);
        
    }

    private void OnDrawGizmosSelected()
    {
        if (_groundChecker == null)
            return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_groundChecker.position, _groundCheckRadius);
    }
}
