using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 적 캐릭터를 담당하는 클래스
/// </summary>
public class Enemy : MonoBehaviour
{
    [Header("---- 컴포넌트 참조 ----")]
    [SerializeField] CombatCharacterModel _model;
    [SerializeField] NavMeshAgent _navAgent;
    [SerializeField] Transform _target;

    private void Start()
    {
        _model.OnDead += OnDead;
    }

    private void Update()
    {
        float distance = (_target.transform.position - transform.position).magnitude;


        if(distance > 20.0f)
        {
            Stop();
        }

        else
        {
            FollowTarget();
        }

    }

    /// <summary>
    /// 이동을 멈추는 함수
    /// </summary>
    public void Stop()
    {
        // NavMeshAgent 이동을 멈추는 코드
        _navAgent.isStopped = true;

        // 현재 NavMeshAgent 경로 초기화
        _navAgent.ResetPath();
    }

    /// <summary>
    /// 타겟을 목적지로 설정하는 함수
    /// (NavMeshAgent를 사용해서 자동으로 이동)
    /// </summary>
    public void FollowTarget()
    {
        // NavMeshAgent에 목적지를 설정하는 함수
        _navAgent.SetDestination(_target.position);
    }

    void OnDead()
    {
        Destroy(gameObject);
    }

    //IEnumerator RomingRoutine()
    //{
    //    while()
    //}
}
