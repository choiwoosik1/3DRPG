using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 아이템의 정보를 UI에 표시하는 클래스
/// </summary>
public class ItemView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    Inventory _inventory;
    int _slotIndex;

    ItemModel _model;
    [SerializeField] Image _iconImage;

    public void Initialize(Inventory inventory, int slotIndex)
    {
        _inventory = inventory;
        _slotIndex = slotIndex;
    }

    /// <summary>
    /// 드래그 시 빈자리 표현을 위한 아이콘 이미지 숨김 / 표시 함수 
    /// </summary>
    /// <param name="isHidden">숨김 여부</param>
    public void Hide(bool isHidden)
    {
        _iconImage.enabled = !isHidden;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log($"드래그 시작 ! {gameObject.name}", gameObject);

        _inventory.BeginDrag(_slotIndex);
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log($"드래그 중... {gameObject.name}", gameObject);

            
        _inventory.Dragging(eventData.position);
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log($"드랍 ! {gameObject.name}", gameObject);

        _inventory.Drop(_slotIndex);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log($"드래그 종료 ! {gameObject}", gameObject);

        _inventory.EndDrag();
    }

    public void SetItemModel(ItemModel model)
    {
        _model = model;

        if(_model != null)
        {
            _iconImage.sprite = _model.Config.IconSprite;
            _iconImage.gameObject.SetActive(true);
        }
        else
        {
            _iconImage.gameObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
}
