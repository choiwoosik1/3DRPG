using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// 인벤토리 메뉴의 View들의 상호작용을 처리하는 클래스
/// </summary>
public class ItemDragController : MonoBehaviour
{
    [SerializeField] Inventory _inventory;
    [SerializeField] EquipController _equipController;
    [SerializeField] ItemView _dragView;                    // 드래그 중인 아이템을 보여 주는 뷰
    [SerializeField] ItemDescription _tooltipView;          // 아이템 툴팁 뷰

    ItemModel _selectedItemModel;
   
    /// <summary>
    /// 툴팁 뷰의 위치를 설정하는 함수
    /// </summary>
    /// <param name="pos"></param>
    public void SetTooltipPosition(Vector2 pos)
    {
        _tooltipView.transform.position = pos;
    }

    /// <summary>
    /// 툴팁을 보여주는 함수
    /// </summary>
    /// <param name="itemModel"></param>
    public void ShowTooltip(ItemModel itemModel)
    {
        if (itemModel == null) return;

        _tooltipView.SetItemModel(itemModel);
        _tooltipView.gameObject.SetActive(true);
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
    }

    public void DropOnEquipmentView(EquipSlotType slotType)
    {
        if (_selectedItemModel == null) return;

        if(_selectedItemModel.GetIsEquippable(slotType) == true)
        {
            _inventory.UseItem(_selectedItemModel.SlotIndex);
        }
    }

}
