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
    IPointerClickHandler
{
    EquipController _equipController;
    Inventory _inventory;

    Equipment _equipMent;

    [SerializeField] EquipSlotType _slotType;
    [SerializeField] Image _iconImage;

    public void Initialize(EquipController equipController, Inventory inventory)
    {
        _equipController = equipController;
        _inventory = inventory;
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
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
