using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

/// <summary>
/// 체력을 회복하는 소비형 아이템 효과
/// </summary>

[CreateAssetMenu(fileName = "HealEffect", menuName = "GameSettings/ItemEffect/Heal")]
public class HealEffect : ItemEffect
{
    [Header("---- 힐량 ----")]
    [SerializeField] float _amount;     // 힐량
    
    public float amount => _amount;

    public override void Apply(Inventory inventory)
    {
        inventory.HeroModel.Heal(_amount);
    }
}
