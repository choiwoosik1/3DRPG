using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 런타임 데이터 클래스(런타임 데이터 + 비즈니스 로직)
/// 인벤토리에 포함되기 전에는 설정 데이터만 갖음.
/// </summary>
[System.Serializable]
public class ItemModel
{
    ItemConfig _config;
    Inventory _inventory;
    public ItemConfig Config => _config;
    public ItemType ItemType => _config.ItemType;
    public ItemModel(ItemConfig config)
    {
        _config = config;
    }

    /// <summary>
    /// 아이템이 인벤토리에 추가될 때 자동으로 호출되어야 하는 함수
    /// 비 소모성 아이템의 경우 패시브 효과를 적용
    /// </summary>
    /// <param name="inventory"></param>
    public void Acquire(Inventory inventory)
    {
        _inventory = inventory;
        if(ItemType == ItemType.NonConsumable)
        {
            if (_config.Effect == null) return;

            _config.Effect.Apply(inventory);
        }
    }

    /// <summary>
    /// 아이템을 사용하는 함수
    /// </summary>
    public void Use()
    {
        if (_inventory == null) return;
        if (_config.Effect == null) return;
        if (ItemType == ItemType.NonConsumable) return;

        Debug.Log($"{_config.ItemName} 아이템 사용 !");
        _config.Effect.Apply(_inventory);
    }

    /// <summary>
    /// 아이템이 인벤토리에서 제거될 때 자동으로 호출되어야 하는 함수.
    /// 비 소모성 아이템의 경우 패시브 효과를 해제한다.
    /// </summary>
    public void Remove()
    {
        if (_inventory == null) return;

        if (ItemType == ItemType.NonConsumable)
        {
            if (_config.Effect == null) return;

            _config.Effect.Disapply(_inventory);
        }
    }
}
        