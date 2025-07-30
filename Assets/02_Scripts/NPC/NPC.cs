using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    [Header("---- 컴포넌트 참조 ----")]
    [SerializeField] DialogueInteract _interact;
    [SerializeField] PathFollower _pathFollower;

    [Header("---- 스탯 데이터 ----")]
    [SerializeField] float _rotSpeed;

    Coroutine _rotateTowardRoutine;

    private void Start()
    {
        _interact.OnBegun += OnInteractionBegun;
        _interact.OnEnded += OnInteractionEnded;

        _pathFollower.StartFollowing();
    }

    /// <summary>
    /// 상호작용 시작 시 자동으로 호출되는 함수
    /// </summary>
    void OnInteractionBegun(Transform subject)
    {
        _pathFollower.StopFollowing();
        _rotateTowardRoutine = StartCoroutine(RotateTowardRoutine(subject));
    }

    /// <summary>
    /// 상호작용 종료 시 자동으로 호출되는 함수
    /// </summary>
    void OnInteractionEnded()
    {
        if(_rotateTowardRoutine != null )
        {
            StopCoroutine(_rotateTowardRoutine);
            _rotateTowardRoutine = null;
        }

        _pathFollower.StartFollowing();
    }

    IEnumerator RotateTowardRoutine(Transform target)
    {
        // 바라볼 방향 벡터
        Vector3 direction = target.position - transform.position;
        direction.y = 0;

        if (direction == Vector3.zero) yield break;

        // 목표 회전값 계산
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        float angle = Quaternion.Angle(transform.rotation, targetRotation);

        // 목표 회전값과 자신의 회전값 사이의 각도가 거의 0이 아닌 동안
        while(angle > Utils.Epsilon)
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                _rotSpeed * Time.deltaTime);
            angle = Quaternion.Angle(transform.rotation, targetRotation);
            yield return null;
        }
    }
}
