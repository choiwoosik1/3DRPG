using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 플레이어 캐릭터 클래스(이동, 점프, 공격, 피격, 사망, 상호작용, 스킬 사용)
/// </summary>
public class Hero : MonoBehaviour
{
    /// <summary>
    /// 이동 속력
    /// </summary>
    [SerializeField] float _moveSpeed;

    /// <summary>
    /// 회전 속력 
    /// </summary>
    [SerializeField] float _rotSpeed;
    
    /// <summary>
    /// 방향대로 이동시키는 함수
    /// </summary>
    /// <param name="direction">이동 방향</param>
    public void Move(Vector3 direction)
    {
        // y방향 이동은 무시
        direction.y = 0;

        // 이동 방향이 0이면(없으면) 이동하지 않음
        if (direction.magnitude < Utils.Epsilon)
        {
            return;
        }

        // 이동
        transform.Translate(
            direction.normalized * _moveSpeed * Time.deltaTime, Space.World);

        // 회전
        // 1) 특정 방향을 즉시 바라보게 설정(transform.forward 활용)
        //transform.forward = direction;

        // 2) 특정 방향을 천천히 바라보게 설정
        // LookRotation() : 특정 방향을 바라보는 회전 값을 만들어 주는 함수
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,             // 현재 회전 값
            targetRotation,                 // 목표 회전 값
            _rotSpeed * Time.deltaTime );   // 프레임당 회전 정도
    }
}
