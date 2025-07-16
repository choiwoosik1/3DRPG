using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RingEffect", menuName = "GameSettings/ItemEffect/RingEffect")]
public class RingEffect : ItemEffect
{
    [Header("---- 최대 체력 증가량 ----")]
    [SerializeField] float _upgradeHp;

    public float UpgradeHp => _upgradeHp;

    public override void Apply(Inventory inventory)
    {

        inventory.HeroModel.UpgradeHp(_upgradeHp);
    }
}
