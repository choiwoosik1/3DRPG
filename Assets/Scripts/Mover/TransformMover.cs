using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformMover : Mover
{
    /// <summary>
    /// ȸ�� �ӷ�
    /// </summary>
    [SerializeField] float _rotSpeed;

    public override event Action<Vector3> OnMoved;

    public override void Move(Vector3 direction)
    {
        // y���� �̵��� ����
        direction.y = 0;

        // �̵� ������ 0�̸�(������) �̵����� ����
        if (direction.magnitude < Utils.Epsilon)
        {
            return;
        }

        // �̵�
        transform.Translate(
            direction.normalized * _moveSpeed * Time.deltaTime, Space.World);

        // ȸ��
        // 1) Ư�� ������ ��� �ٶ󺸰� ����(transform.forward Ȱ��)
        //transform.forward = direction;

        // 2) Ư�� ������ õõ�� �ٶ󺸰� ����
        // LookRotation() : Ư�� ������ �ٶ󺸴� ȸ�� ���� ����� �ִ� �Լ�
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,             // ���� ȸ�� ��
            targetRotation,                 // ��ǥ ȸ�� ��
            _rotSpeed * Time.deltaTime);   // �����Ӵ� ȸ�� ����
    }
}
