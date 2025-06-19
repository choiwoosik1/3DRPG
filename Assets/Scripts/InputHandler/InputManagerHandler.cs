using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 유니티의 InputManager 기반 사용자 입력을 처리하는 클래스
/// </summary>
public class InputManagerHandler : InputHandler
{
    public override event Action<Vector2> OnMoveInput;
    public override event Action<Vector2> OnCameraRotInput;
    public override event Action<float> OnCameraZoomInput;
    public override event Action OnAttackInput;
    public override event Action OnJumpInput;

    Vector2 _moveInput;         // 이동 입력 벡터
    Vector2 _cameraRotInput;    // 카메라 회전 입력 벡터
    float _cameraZoomInput;     // 카메라 줌 입력 값

    private void Update()
    {
        // 입력값들 가져오기
        _moveInput.x = Input.GetAxisRaw("Horizontal");
        _moveInput.y = Input.GetAxisRaw("Vertical");

        _cameraRotInput.x = Input.GetAxisRaw("Mouse X");
        _cameraRotInput.y = Input.GetAxisRaw("Mouse Y");

        _cameraZoomInput = Input.GetAxisRaw("Mouse ScrollWheel");

        // 입력 이벤트들 발생
        OnMoveInput?.Invoke(_moveInput);
        OnCameraRotInput?.Invoke(_cameraRotInput);
        OnCameraZoomInput?.Invoke(_cameraZoomInput);

        if (Input.GetButtonDown("Fire1"))
        {
            OnAttackInput?.Invoke();
        }

        if (Input.GetButtonDown("Jump"))
        {
            OnJumpInput?.Invoke();
        }
    }
}
