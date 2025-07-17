using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 최대 HP를 증가시키는 패시브 아이템 효과
/// </summary>

[CreateAssetMenu(fileName ="AddMMaxHpEffect", menuName = "GameSettings/ItemEffect/AddMaxHp")]
public class AddMaxHpEffect : ItemEffect
{
    [SerializeField] float _amount;
    public float Amount => _amount;

    public override void Apply(Inventory inventory)
    {
        inventory.HeroModel.AddMaxHp(_amount);
    }

    public override void Disapply(Inventory inventory)
    {
        inventory.HeroModel.AddMaxHp(-_amount);
    }
}
