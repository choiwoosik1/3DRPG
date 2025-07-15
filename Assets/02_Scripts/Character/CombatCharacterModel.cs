using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 전투 캐릭터의 모델 클래스
/// (체력, 공격력, 방어력 등 Runtime Data 관리
/// </summary>
public class CombatCharacterModel : MonoBehaviour, IDamageable, IAttackable
{
    [Header("---- 이동 ----")]
    [SerializeField] float _moveSpeed;      // 이동 속력
    [SerializeField] float _rotSpeed;       // 회전 속력

    [Header("---- 피격 ----")]
    [SerializeField] float _maxHp;          // 최대 체력
    [SerializeField] float _currentHp;      // 현재 체력
    [SerializeField] float _armor;          // 방어력

    [Header("---- 공격 ----")]
    [SerializeField] float _damage;         // 공격력

    public float MoveSpeed => _moveSpeed;
    public float RotSpeed => _rotSpeed;

    public float CurrentHp => _currentHp;

    public event Action<float, float> OnHpChanged;
    public event Action OnDead;

    public void Hit(IDamageable damageable)
    {
        damageable.TakeHit(_damage);
    }

    public void TakeHit(float damage)
    {
        // 들어온 데미지에 방어력 적용
        damage = Mathf.Max(damage - _armor, 0);

        // 데미지에 따라 현재 체력 계산
        _currentHp = Mathf.Min(_currentHp - damage, _maxHp);

        // 체력 반경 이벤트 발행
        OnHpChanged?.Invoke(_currentHp, _maxHp);

        // 사망 시 이벤트 발행
        if(_currentHp <= 0)
        {
            OnDead?.Invoke(); 
        }
    }

    /// <summary>
    /// 체력을 회복하는 함수
    /// </summary>
    /// <param name="amount">회복량</param>
    public void Heal(float amount)
    {
        amount = MathF.Max(amount, 0);
        _currentHp = MathF.Min(_currentHp + amount, _maxHp);
        OnHpChanged?.Invoke(_currentHp, _maxHp);
    }
}
