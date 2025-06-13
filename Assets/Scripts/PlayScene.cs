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

    // InputHandler의 이벤트 알림을 구독해서 Hero가 움직이도록
    void Start()
    {
        // 이동 입력 이벤트 구독
        _inputHandler.OnMoveInput += OnMoveInput;
    }

    /// <summary>
    /// 입력 방향을 받아 Vector3로 변환한 후 Hero에게 이동 명령을 내리는 함수
    /// </summary>
    /// <param name="inputVec"></param>
    public void OnMoveInput(Vector2 inputVec)
    {
        //Vector3 direction = Vector3.zero;
        //direction.x = inputVec.x;
        //direction.z = inputVec.y;

        //_hero.Move(direction);

        Vector3 _inputVec = new Vector3(inputVec.x, 0, inputVec.y);
        
        _hero.Move(_inputVec);
    }
}
