using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 장비 장착/해제를 총괄하는 클래스
/// </summary>
public class EquipController : MonoBehaviour
{
    Dictionary<EquipSlotType, Equipment> _equipmentMap = new();

    [SerializeField] HeroModel _heroModel;
    [SerializeField] Inventory _inventory;

    [SerializeField] Transform[] _slotTransforms;       // 장비 슬롯 부모 트랜스폼

    /// <summary>
    /// 무기 장착 이벤트, Hit Point 전달
    /// </summary>
    public event Action<Transform> OnWeaponEquipped;

    /// <summary>
    /// 장비를 장착하는 함수
    /// 기존 장비가 있으면 자동으로 해제 후 새 장비 장착
    /// </summary>
    /// <param name="itemModel"></param>
    public void Equip(EquipmentItemModel itemModel)
    {
        EquipSlotType slotType = itemModel.EquipmentPrefab.EquipSlotType;
        int slotIndex = (int)slotType;
        if (slotIndex < 0 || slotIndex >= _slotTransforms.Length) return;

        // 기존 장비가 있었다면 해제
        UnEquip(slotType);

        Transform slotTransform = _slotTransforms[slotIndex];
        Equipment equipment = Instantiate(itemModel.EquipmentPrefab, slotTransform);
        equipment.SetItemModel(itemModel);
        _equipmentMap[slotType] = equipment;

        _heroModel.AddMaxHp(equipment.BonusMaxHp);
        _heroModel.AddArmor(equipment.BonusArmor);
        _heroModel.AddDamage(equipment.BonusDamage);

        // is keyWord 사용 (cf. as 키워드)
        // equipment가 Weapon 클래스이면 중괄호 안에서는 weapon이라는 이름으로 Weapon자료형으로 쓰겠다
        if(equipment is Weapon weapon)
        {
            OnWeaponEquipped?.Invoke(weapon.HitPoint);
        }
    }

    /// <summary>
    /// 장비를 해제하는 함수
    /// </summary>
    /// <param name="slotType"></param>
    public void UnEquip(EquipSlotType slotType)
    {
        if (_equipmentMap.ContainsKey(slotType))
        {
            Equipment equipment = _equipmentMap[slotType];

            // 1. 해당 장비가 연결된 아이템 모델을 인벤토리에 추가
            // 2. 장비로 인한 능력치 변화 해제
            // 3. 장비 제거
            // 4. 장비 맵에서 키 제거
            // 5. 무기의 경우 무기 제거 이벤트 알림
        }
    }
}
