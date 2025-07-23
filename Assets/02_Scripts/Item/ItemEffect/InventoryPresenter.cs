using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// 인벤토리 메뉴의 View들의 상호작용을 처리하는 클래스
/// </summary>
public class InventoryPresenter : MonoBehaviour
{
    [SerializeField] Inventory _inventory;
    [SerializeField] EquipController _equipController;
    [SerializeField] ItemView _dragView;                    // 드래그 중인 아이템을 보여 주는 뷰
    [SerializeField] ItemDescription _tooltipView;          // 아이템 툴팁 뷰

    [SerializeField] ItemView[] _itemViews;
    [SerializeField] EquipmentView[] _equipmentViews;

    bool _hasOpened = false;                                // 인벤토리 메뉴 열렸는지 여부
    ItemModel _selectedItemModel;                           // 드래그 중인 아이템 모델

    /// <summary>
    /// 인벤토리 메뉴 여닫기 이벤트
    /// </summary>
    public event Action<bool> OnToggled;

    public void Initialize()
    {
        _inventory.OnSlotChanged += OnItemSlotChanged;
        _equipController.OnSlotChanged += OnEquipmentSlotChanged;

        // 아이템 뷰들 초기화
        for(int i = 0; i < _itemViews.Length; i++)
        {
            _itemViews[i].Initialize(this, i);
            _inventory.TryGetItemModel(i, out ItemModel itemModel);
            _itemViews[i].SetItemModel(itemModel);
        }

        // 장비 뷰들 초기화
        for(int i = 0; i< _equipmentViews.Length; i++)
        {
            _equipmentViews[i].Initialize(this);
            _equipController.TryGetEquipment((EquipSlotType)i, out Equipment equipment);
            _equipmentViews[i].SetEquipMent(equipment);
        }
    }

    /// <summary>
    /// 인벤토리 메뉴 여닫는 함수
    /// </summary>
    public void Toggle()
    {
        _hasOpened = !_hasOpened;
        gameObject.SetActive(_hasOpened);

        OnToggled?.Invoke(_hasOpened);

        EndDrag();
        HideTooltip();
        foreach(var itemView in _itemViews)
        {
            itemView.Hide(false);
        }
        foreach(var equipmentView in _equipmentViews)
        {
            equipmentView.Hide(false);
        }
    }

    public void UseItem(int slotIndex)
    {
        _inventory.UseItem(slotIndex);

        ShowTooltip(slotIndex);
    }
    
    public void UnequipEquipment(EquipSlotType slotType)
    {
        _equipController.Unequip(slotType);

        ShowTooltip(slotType);
    }

    /// <summary>
    /// 아이템 슬롯에 변화가 있을 경우 자동으로 호출되어 해당 뷰를 갱신
    /// </summary>
    /// <param name="slotIndex"></param>
    /// <param name="itemModel"></param>
    void OnItemSlotChanged(int slotIndex, ItemModel itemModel)
    {
        
        if (slotIndex < 0 || slotIndex >= _itemViews.Length) return;

        _itemViews[slotIndex].SetItemModel(itemModel);
    }

    /// <summary>
    /// 장비 슬롯에 변화가 있을 경우 자동으로 호출되어 해당 뷰를 갱신하는 함수
    /// </summary>
    /// <param name="slotType"></param>
    /// <param name="equipment"></param>
    void OnEquipmentSlotChanged(EquipSlotType slotType, Equipment equipment)
    {
        int slotIndex = (int)slotType;
        if (slotIndex < 0 || slotIndex >= _equipmentViews.Length) return;

        _equipmentViews[slotIndex].SetEquipMent(equipment);
    }

    /// <summary>
    /// 툴팁 뷰의 위치를 설정하는 함수
    /// </summary>
    /// <param name="pos"></param>
    public void SetTooltipPosition(Vector2 pos)
    {
        _tooltipView.transform.position = pos;
    }

    ///// <summary>
    ///// 툴팁을 보여주는 함수
    ///// </summary>
    ///// <param name="itemModel"></param>
    //public void ShowTooltip(ItemModel itemModel)
    //{
    //    if (itemModel == null) return;

    //    _tooltipView.SetItemModel(itemModel);
    //    _tooltipView.gameObject.SetActive(true);
    //}

    public void ShowTooltip(int slotIndex)
    {
        _inventory.TryGetItemModel(slotIndex, out ItemModel itemModel);
        if(itemModel != null)
        {
            _tooltipView.SetItemModel(itemModel);
            _tooltipView.gameObject.SetActive(true);
        }
        else
        {
            _tooltipView.gameObject.SetActive(false);
        }
    }

    public void ShowTooltip(EquipSlotType slotType)
    {
        _equipController.TryGetEquipment(slotType, out Equipment equipment);
        if(equipment != null)
        {
            _tooltipView.SetItemModel(equipment.ItemModel);
            _tooltipView.gameObject.SetActive(true);
        }
        else
        {
            _tooltipView.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 툴팁을 숨기는 함수
    /// </summary>
    public void HideTooltip()
    {
        _tooltipView.gameObject.SetActive(false);
    }

    public void BeginDrag(ItemModel itemModel)
    {
        if (itemModel == null) return;

        _selectedItemModel = itemModel;
        _dragView.SetItemModel(_selectedItemModel);
        _dragView.gameObject.SetActive(true);
    }

    public void Dragging(Vector2 pos)
    {
        if (_selectedItemModel == null) return;

        _dragView.transform.position = pos;
        HideTooltip();
    }

    public void EndDrag()
    {
        _dragView.gameObject.SetActive(false);

        _selectedItemModel = null;
    }

    public void DropnItemView(int slotIndex)
    {
        if(_selectedItemModel == null) return;

        // 드래그 중인 아이템이 장착 중인 아이템이었으면
        if(_selectedItemModel.IsEquipped == true)
        {
            // 드롭을 받은 아이템 슬롯이 빈 슬롯이면
            if (_inventory.GetIsEmptySlot(slotIndex) == true)
            {
                // 드래그 중인 아이템 장착 해제
                _equipController.Unequip(_selectedItemModel);

                // 장착 해제된 슬롯과 드롭받은 슬롯을 스왑
                _inventory.SwapItems(_selectedItemModel.SlotIndex, slotIndex);
            }
        }
        // 드래그 중인 아이템이 장착중인 아이템이 아니었으면(인벤토리에 있는 아이템이었으면)
        else
        {
            _inventory.SwapItems(_selectedItemModel.SlotIndex, slotIndex);
        }

        ShowTooltip(slotIndex);
    }

    public void DropOnEquipmentView(EquipSlotType slotType)
    {
        if (_selectedItemModel == null) return;

        if(_selectedItemModel.GetIsEquippable(slotType) == true)
        {
            _inventory.UseItem(_selectedItemModel.SlotIndex);
        }

        ShowTooltip(slotType);
    }

}
