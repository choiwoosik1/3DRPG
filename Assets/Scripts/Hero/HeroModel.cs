using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 주인공 캐릭터의 런타임 데이터를 관리하는 클래스
/// </summary>
public class HeroModel : CombatCharacterModel
{
    [SerializeField] float _moveSpeed;
    [SerializeField] float _rotSpeed;
    [SerializeField] CombatCharacterModel _characterModel;

    public void SetMoveSpeed()
    {
        _moveSpeed = _characterModel.MoveSpeed;
    }

    public void SetRotSpeed()
    {
        _rotSpeed = _characterModel.RotSpeed;
    }
}