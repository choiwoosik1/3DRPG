using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 캐릭터 이동을 담당하는 추상 클래스
/// </summary>
public abstract class Mover : MonoBehaviour
{
    /// <summary>
    /// 이동 속력
    /// </summary>
    [SerializeField] protected float _moveSpeed;

    /// <summary>
    /// 이동 이벤트
    /// </summary>
    public abstract event Action<Vector3> OnMoved;

    /// <summary>
    /// 방향대로 이동 시키는 함수
    /// </summary>
    /// <param name="direction">이동할 방향</param>
    public abstract void Move(Vector3 direction);
}
