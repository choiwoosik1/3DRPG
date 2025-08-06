using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 특정 지점을 중심으로 범위 내의 IDamageable 객체를 감지하는 클래스
/// </summary>
public class DamageableDetector : MonoBehaviour
{
    [SerializeField] Transform _detectPoint;            // 감지 지점(특정 지점)
    [SerializeField] float _radius;                     // 구성 감지 범위의 반지름
    [SerializeField] LayerMask _targetLayerMask;        // 감지할 LayerMask

    public Transform HitPoint => _detectPoint;          // 타점

    /// <summary>
    /// IDamageable 감지 이벤트
    /// </summary>
    public event Action<IDamageable> OnDetected;

    Collider[] _colliders = new Collider[5];

    /// <summary>
    /// 범위 내의 IDaamageable을 감지하는 함수
    /// </summary>
    /// <returns></returns>
    public void DetectDamageable()
    {
        for(int i =0; i < _colliders.Length; i++)
        {
            _colliders[i] = null;
        }

        // _detectPoint.position을 중심으로
        // _radius 반지름의 구를 그리고
        // 그것과 겹치는 _targetLayerMask의 Collider들을
        // _collider 배열에 순서대로 저장
        int count =
            Physics.OverlapSphereNonAlloc(
                _detectPoint.position,
                _radius,
                _colliders,
                _targetLayerMask);

        // 감지된 Collider 수가 0보다 크면
        if(count > 0)
        {
            foreach(var collider in _colliders)
            {
                if (collider == null) continue;

                // GetCompoonentInParent(): 자신이나 부모 게임 오브젝트에서 컴포넌트를 찾아 반환해 주는 함수
                IDamageable damageable = collider.GetComponentInParent<IDamageable>();
                if(damageable != null)
                {
                    Debug.Log("공격 판정", collider);
                    OnDetected?.Invoke(damageable);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_detectPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_detectPoint.position, _radius);
    }

    public void SetDetectPoint(Transform hitPoint)
    {
        _detectPoint = hitPoint;
    }
}
