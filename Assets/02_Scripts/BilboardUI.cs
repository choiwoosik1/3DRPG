using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 월드 UI의 방향을 카메라와 맞춰주는 클래스
/// </summary>
public class BilboardUI : MonoBehaviour
{
    Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void LateUpdate()
    {
        // 카메라의 정면(forward) 방향과 자신의 정면 방향 일치
        //transform.forward = _camera.transform.forward;

        transform.LookAt(transform.position + _camera.transform.forward,
            _camera.transform.up);
    }
}
