using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PlayScene을 총괄하는 클래스
/// </summary>
public class PlayScene : MonoBehaviour
{
    [Header("---- 컴포넌트 참조 ----")]
    [SerializeField] InputHandler _inputHandler;
    [SerializeField] Hero _hero;
    [SerializeField] CameraController _cameraController;

    // InputHandler의 이벤트 알림을 구독해서 Hero가 움직이도록
    void Start()
    {
        // 이동 입력 이벤트 구독
        _inputHandler.OnMoveInput += OnMoveInput;

        // 카메라 회전 입력 이벤트 구독
        _inputHandler.OnCameraRotInput += _cameraController.Rotate;
        
        // 카메라 줌 입력 이벤트 구독
        _inputHandler.OnCameraZoomInput += _cameraController.Zoom;

        // 공격 입력 이벤트 구독
        _inputHandler.OnAttackInput += _hero.Attack;

        // 점프 입력 이벤트 구독
        _inputHandler.OnJumpInput += _hero.Jump;
    }

    /// <summary>
    /// 입력 방향을 받아 Vector3로 변환한 후 Hero에게 이동 명령을 내리는 함수
    /// </summary>
    /// <param name="inputVec"></param>
    void OnMoveInput(Vector2 inputVec)
    {
        // 카메라 앞쪽 방향
        Vector3 camForward = Camera.main.transform.forward;

        // 카메라 오른쪽 방향
        Vector3 camRight = Camera.main.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 direction = camForward * inputVec.y + camRight * inputVec.x;

        //Vector3 direction = Vector3.zero;
        //direction.x = inputVec.x;
        //direction.z = inputVec.y;

        //_hero.Move(direction);
        
        _hero.Move(direction);
    }
}
