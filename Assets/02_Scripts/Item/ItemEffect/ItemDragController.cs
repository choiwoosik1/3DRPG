using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

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
}
