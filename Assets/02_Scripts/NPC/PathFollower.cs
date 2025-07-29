using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public enum PathFollowingType
{
    Loop,       // 0번 -> 끝번 -> 0번 -> 끝번 ...
    PingPong,   // 0번 -> 끝번 -> 끝번 -> 1번 ...
}

/// <summary>
/// 지정된 경로(WayPoints)를 따라 순환 이동하는 클래스
/// </summary>
public class PathFollower : MonoBehaviour
{
    [Header("---- 컴포넌트 참조 ----")]
    [SerializeField] NavMeshAgent _agent;

    [Header("---- 런타임 데이터 ----")]
    [SerializeField] List<Transform> _wayPoints = new List<Transform>();
    [SerializeField] PathFollowingType _followingType = PathFollowingType.Loop;
    [SerializeField] float _moveSpeed;
    [SerializeField] float _arrivalThreshold;       // 도착 감지 임계값

    int _currentIndex = 0;
    int _direction = 1;                             // Loop일 때 1, PingPong일 때 1 또는 -1

    private void Start()
    {
        _agent.speed = _moveSpeed;
    }

    private void Update()
    {
        if (_wayPoints.Count < 2 || _agent.isStopped == true) return;

        // if(transform.position = _wayPoints[_currentIndex].position)
        // -> 불가능(float 값은 정확한 값이 아니기 때문에)

        float distance = Vector3.Distance(transform.position, _wayPoints[_currentIndex].position);
        if(distance < _arrivalThreshold)            // 도착한 것으로 판단
        {
            AdvanceToNextWaypoint();
        }
    }

    /// <summary>
    /// WayPoint로 향하는 함수
    /// </summary>
    /// <param name="index">WayPoint 순번</param>
    void MoveToWayPoint(int index)
    {
        if (index < 0 || index >= _wayPoints.Count) return;

        _agent.SetDestination(_wayPoints[index].position);
    }

    /// <summary>
    /// 다음 WayPoint로 향하는 함수
    /// </summary>
    void AdvanceToNextWaypoint()
    {
        switch (_followingType)
        {
            case PathFollowingType.Loop:
                _currentIndex = (_currentIndex + 1) % _wayPoints.Count;
                break;
            case PathFollowingType.PingPong:
                _currentIndex += _direction;

                if(_currentIndex >= _wayPoints.Count)
                {
                    _direction = -1;
                    _currentIndex = _wayPoints.Count - 2;
                }
                else if(_currentIndex < 0)
                {
                    _direction = 1;
                    _currentIndex = 1;
                }
                break;
        }

        MoveToWayPoint(_currentIndex);
    }

    /// <summary>
    /// 이동을 시작하는 함수
    /// </summary>
    public void StartFollowing()
    {
        _agent.isStopped = false;
        MoveToWayPoint(_currentIndex);
    }

    public void StopFollowing()
    {
        _agent.isStopped = true;
        _agent.ResetPath();
    }
}
