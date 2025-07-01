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
    [SerializeField] HeroModel _model;
    [SerializeField] CharacterAnimatorHandler _animatorHandler;
    [SerializeField] DamageableDetector _damageableDetector;
    [SerializeField] HeroStatusView _statusView;

    private void Start()
    {
        // 임시
        _statusView.SetHeroNameText(GameManager.Instance.HeroData.HeroName);

        // Mover 초기화
        _mover.SetMoveSpeed(_model.MoveSpeed);
        _mover.SetRotSpeed(_model.RotSpeed);

        // 이동 이벤트 구독
        _mover.OnMoved += OnMoved;

        // 점프 상태 변경 이벤트 구독
        _jumper.OnStateChanged += OnJumpStateChanged;

        // 공격 판정 이벤트 구독
        //_animatorHandler.OnAttacked += _damageableDetector.DetectDamageable;
        _animatorHandler.OnAttacked += OnAttaked;

        // IDamageaable 감지 이벤트 구독
        //_damageableDetector.OnDetected += _model.Hit;
        _damageableDetector.OnDetected += Hit;

        // 체력 변경 이벤트 구독
        _model.OnHpChanged += _statusView.SetHpBar;
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
    /// 공격 판정 시 자동으로 실행되는 함수
    /// </summary>
    void OnAttaked()
    {
        Debug.Log("공격 판정 시도...");
        _damageableDetector.DetectDamageable();
    }

    /// <summary>
    /// 감지된 IDemageable에 데미지를 입히는 함수
    /// IDamageable이 감지되었을 때 자동으로 실행되는 함수
    /// </summary>
    /// <param name="damageable"></param>
    void Hit(IDamageable damageable)
    {
        _model.Hit(damageable);
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

    /// <summary>
    /// 질주를 On/Off 하는 함수
    /// </summary>
    /// <param name="isActive">질주 활성화 여부</param>
    public void SetSprintActive(bool isActive)
    {
        float moveSpeed = _model.MoveSpeed;

        if(isActive == true)
        {
            moveSpeed *= _model.SprintRate;
        }
        _mover.SetMoveSpeed(moveSpeed);
    }
}
