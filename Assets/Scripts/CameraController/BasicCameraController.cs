using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCameraController : CameraController
{
    [Header("---- ī�޶� ��Ŀ�� ----")]
    [SerializeField] Transform _target;         // ī�޶� ��Ŀ���� ������ Ÿ��
    [SerializeField] Transform _cameraFocus;    // ī�޶� ��Ŀ��

    [Header("---- �̵� ----")]
    [SerializeField] float _followingSpeed;

    [Header("---- ī�޶� ȸ�� ----")]
    [Tooltip("x�� ȸ�� �ΰ���")]
    [SerializeField] float _pitchSensitivity;
    [Tooltip("y�� ȸ�� �ΰ���")]
    [SerializeField] float _yawSensitivity;

    [SerializeField] float _minPitch;           // x�� ȸ�� �ּڰ�
    [SerializeField] float _maxPitch;           // x�� ȸ�� �ִ밪

    [SerializeField] float _rotRate;            // ��ǥ ȸ���� ���󰡴� ���� ��

    [Header("---- ī�޶� �� ----")]
    [SerializeField] float _zoomRate;           // �� ����(����)
    [SerializeField] float _minDistance;        // ī�޶��� ī�޶� ��Ŀ������ �ּ� �Ÿ�
    [SerializeField] float _maxDistance;        // ī�޶��� ī�޶� ��Ŀ������ �ִ� �Ÿ�

    Vector3 _focusOffset;                       // ī�޶� ��Ŀ���� Ÿ�����κ��� �󸶸�ŭ �������ִ���
    float _pitch;                               // ī�޶��� x �� ȸ�� ��
    float _yaw;                                 // ī�޶��� y �� ȸ�� ��
    float _distance;                            // ī�޶��� ī�޶� ��Ŀ������ �Ÿ�

    private void Start()
    {
        _pitch = transform.eulerAngles.x;       // Inspector View���� ���� ����. rotate�� �����ϸ� �ش� ���̶� ���� ���̶� �ٸ�
        _yaw = transform.eulerAngles.y;

        _focusOffset = _cameraFocus.position - _target.position;

        _distance = Vector3.Distance(_cameraFocus.position, transform.position);
    }

    private void Update()
    {
        // ī�޶� ��Ŀ��(_cameraFocus)�� Ÿ��(_target)�� _focusOffset��ŭ ������ ä�� ��� ����ٴϰ� �ϴ� �ڵ�
        //_cameraFocus.position = _target.TransformPoint(_focusOffset);

        // ī�޶� ��Ŀ���� Ÿ���� ������ �ӷ����� �����ϰ� �ϴ� ���
        // ī�޶� ���ΰ� ĳ���͸� ���󰡰� �ϱ⿡�� �������� ���� ���
        //_cameraFocus.position =
        //    Vector3.MoveTowards(_cameraFocus.position,
        //    _target.TransformPoint(_focusOffset),
        //    Time.deltaTime * _followingSpeed);

        // MoveTowards()�� ������ �ӷ����� �̵��� �� Ȱ��
        // Lerp()�� 0 ~ 1������ ���� -> ó������ �����ٰ� ������ õõ�� ����
        _cameraFocus.position = Vector3.Lerp(_cameraFocus.position,
            _target.TransformPoint(_focusOffset),
            Time.deltaTime * _followingSpeed);          // ��¥ �ӷ��� �ƴ�
    }

    // ��� Update()�� ���� ������ �ʰ� ����Ǵ� Update()
    private void LateUpdate()
    {
        UpdateCameraPosition(); 
    }

    public override void Rotate(Vector2 rotInput)
    {
        // x���� �� �Ʒ��� �����̱� ������ y�� ����
        // x�� ȸ���� ����
        _pitch -= rotInput.y * _pitchSensitivity;

        // y�� ȸ���� ����
        _yaw += rotInput.x * _yawSensitivity;

        // x�� ȸ���� ���� �� ����
        _pitch = Mathf.Clamp(_pitch, _minPitch, _maxPitch); 
    }

    public override void Zoom(float zoomInput)
    {
        // ī�޶��� �Ÿ��� ����
        _distance -= zoomInput * _zoomRate;

        // ī�޶��� �Ÿ����� ���� �� ����
        _distance = Mathf.Clamp(_distance, _minDistance, _maxDistance);
    }

    void UpdateCameraPosition()
    {
        // ��ǥ ȸ���� ����
        Quaternion targetRotation = Quaternion.Euler(_pitch, _yaw, 0);

        // ī�޶��� ȸ������ ��ǥ ȸ���� ����
        //transform.rotation = targetRotation;

        // ī�޶��� ȸ������ �ε巴�� ó��
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            Time.deltaTime * _rotRate);

        // ī�޶��� ī�޶� ��Ŀ���κ��� ��ġ ����
        Vector3 cameraOffset = -transform.forward * _distance;

        // ī�޶��� ��ġ�� ����
        transform.position = _cameraFocus.position + cameraOffset;
    }
}
