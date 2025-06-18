using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 사용자 입력을 처리하는 추상 클래스
/// 입력을 감지하고 전달하는 역할을 한다.
/// </summary>
public abstract class InputHandler : MonoBehaviour
{
    /// <summary>
    /// 이동 입력 이벤트
    /// </summary>
    public abstract event Action<Vector2> OnMoveInput;

    /// <summary>
    /// 카메라 회전 입력 이벤트
    /// </summary>
    public abstract event Action<Vector2> OnCameraRotInput;

    /// <summary>
    /// 카메라 줌 입력 이벤트
    /// </summary>
    public abstract event Action<float> OnCameraZoomInput;

    public abstract event Action OnAttackInput;
}
