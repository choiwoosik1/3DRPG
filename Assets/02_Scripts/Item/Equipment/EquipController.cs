using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

/// <summary>
/// 장비 장착/해제를 총괄하는 클래스
/// </summary>
public class EquipController : MonoBehaviour
{
    Dictionary<EquipSlotType, Equipment> _equipmentMap = new();

    [SerializeField] HeroModel _heroModel;
    [SerializeField] Inventory _inventory;
    [SerializeField] ItemDragController _dragController;

    [SerializeField] Transform[] _slotTransforms;       // 장비 슬롯 부모 트랜스폼
    [SerializeField] EquipmentView[] _equipmentViews;
    

    /// <summary>
    /// 무기 장착 이벤트, Hit Point 전달
    /// </summary>
    public event Action<Transform> OnWeaponEquipped;

    public void Initialize()
    {
        foreach(var equipmentView in _equipmentViews)
        {
            equipmentView.Initialize(this, _dragController);
            equipmentView.SetEquipMent(null);
        }
    }

    void SetEquipmentView(EquipSlotType slotType, Equipment equipment)
    {
        int slotIndex = (int)slotType;
        if (slotIndex < 0 || slotIndex >= _equipmentViews.Length) return;

        _equipmentViews[slotIndex].SetEquipMent(equipment);
    }

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

        // 장비 프리펩 생성
        Transform slotTransform = _slotTransforms[slotIndex];
        Equipment equipment = Instantiate(itemModel.EquipmentPrefab, slotTransform);
        equipment.SetItemModel(itemModel);
        _equipmentMap[slotType] = equipment;

        // 장비 스탯 적용
        _heroModel.AddMaxHp(equipment.BonusMaxHp);
        _heroModel.AddArmor(equipment.BonusArmor);
        _heroModel.AddDamage(equipment.BonusDamage);

        // is keyWord 사용 (cf. as 키워드)
        // equipment가 Weapon 클래스이면 중괄호 안에서는 weapon이라는 이름으로 Weapon자료형으로 쓰겠다
        if(equipment is Weapon weapon)
        {
            OnWeaponEquipped?.Invoke(weapon.HitPoint);
        }

        itemModel.SetIsEquipped(true);

        SetEquipmentView(slotType, equipment);
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
            EquipmentItemModel itemModel = equipment.ItemModel;

            // 1. 해당 장비가 연결된 아이템 모델을 인벤토리에 추가
            // -> 인벤토리에 아이템 추가 실패 시 장비 해제 불가
            if (_inventory.TryAddItem(equipment.ItemModel) == false) return;
            
            // 2. 장비로 인한 능력치 변화 해제
            _heroModel.AddMaxHp(-equipment.BonusMaxHp);
            _heroModel.AddArmor(-equipment.BonusArmor);
            _heroModel.AddDamage(-equipment.BonusDamage);

            // 3. 장비 제거
            Destroy(equipment.gameObject);

            // 4. 장비 맵에서 키 제거
            _equipmentMap.Remove(slotType);

            // 5. 무기의 경우 무기 제거 이벤트 알림
            if(slotType == EquipSlotType.Weapon)
            {
                OnWeaponEquipped?.Invoke(null);
            }

            itemModel.SetIsEquipped(false);

            // 6. 장비 ㅅㄹ롯 뷰 갱신
            SetEquipmentView(slotType, null);
        }
    }

    public void Unequip(ItemModel itemModel)
    {
        if(_equipmentMap.ContainsKey(itemModel.EquipSlotType) == true)
        {
            if(_equipmentMap[itemModel.EquipSlotType].ItemModel == itemModel)
            {
                UnEquip(itemModel.EquipSlotType);
            }
        }
    }
}
