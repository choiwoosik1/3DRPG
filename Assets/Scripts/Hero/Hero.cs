using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 플레이어 캐릭터 클래스(이동, 점프, 공격, 피격, 사망, 상호작용, 스킬 사용)
/// </summary>
public class Hero : MonoBehaviour
{
    [Header("---- 컴포넌트 참조 ----")]
    [SerializeField] Mover _mover;
    [SerializeField] Animator _animator;
    [SerializeField] Jumper _jumper;

    private void Start()
    {
        _mover.OnMoved += OnMoved;
        _jumper.OnStateChanged += OnJumpStateChanged;
    }


    /// <summary>
    /// 방향대로 이동시키는 함수
    /// </summary>
    /// <param name="direction">이동 방향</param>
    public void Move(Vector3 direction)
    {
        _mover.Move(direction);
    }

    void OnMoved(Vector3 velocity)
    {
        _animator.SetFloat(AnimatorParameters.MoveSpeed, velocity.magnitude);
    }

    /// <summary>
    /// 공격 동작을 실행 시키는 함수
    /// </summary>
    public void Attack()
    {
        _animator.SetTrigger(AnimatorParameters.OnAttack);
    }

    /// <summary>
    /// 점프 동작을 실행시키는 함수
    /// </summary>
    public void Jump()
    {
        _jumper.Jump();
    }

    public void OnJumpStateChanged(JumpState jumpstate)
    {
        switch(jumpstate)
        {
            case JumpState.Grounded:
                _animator.SetTrigger(AnimatorParameters.OnLand);
                break;
            case JumpState.Jumping:
                _animator.SetTrigger(AnimatorParameters.OnJump);
                break;
            case JumpState.Falling:
                _animator.SetTrigger(AnimatorParameters.OnFalling);
                break;
        }
    }
}
