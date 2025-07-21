using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 장비 슬롯 뷰
/// 툴팁, 우클릭 해제 기능
/// </summary>
public class EquipmentView : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler,
    IPointerClickHandler,
    IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    EquipController _equipController;
    Inventory _inventory;
    ItemDragController _dragController;

    Equipment _equipMent;

    [SerializeField] EquipSlotType _slotType;
    [SerializeField] Image _iconImage;

    public void Initialize(EquipController equipController, ItemDragController dragController)
    {
        _equipController = equipController;
        _dragController = dragController;
    }

    public void SetEquipMent(Equipment equipment)
    {
        _equipMent = equipment;

        if(_equipMent != null)
        {
            _iconImage.sprite = _equipMent.ItemModel.Config.IconSprite;
            _iconImage.gameObject.SetActive(true);
        }
        else
        {
            _iconImage.gameObject.SetActive(false);
        }
    }

    public void Hide(bool isHidden)
    {
        _iconImage.enabled = isHidden;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Right) return;

        _equipController.UnEquip(_slotType);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _dragController.SetTooltipPosition(transform.position); ;
        
        if(_equipMent == null) return;
        _dragController.ShowTooltip(_equipMent.ItemModel);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _dragController.HideTooltip();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;

        if (_equipMent == null) return;

        _dragController.BeginDrag(_equipMent.ItemModel);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;

        _dragController.Dragging(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;

        _dragController.EndDrag();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;

        _dragController.DropOnEquipmentView(_slotType);
    }
}
