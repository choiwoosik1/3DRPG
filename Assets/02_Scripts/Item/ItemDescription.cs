using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 아이템 설명 표시 View
/// 인벤토리에서 아이템 슬롯 위에 포인터를 올렸을 때 자동으로 표시되는 툴팁
/// </summary>
public class ItemDescription : MonoBehaviour
{
    const string _priceTextFormat = "가격 : {0} 골드";

    [Header("---- 컴포넌트 참조 ----")]
    [SerializeField] TextMeshProUGUI _nameText;
    [SerializeField] TextMeshProUGUI _descText;
    [SerializeField] TextMeshProUGUI _priceText;

    public void SetItemModel(ItemModel model)
    {
        _nameText.text = model.Config.ItemName;
        _descText.text = model.Config.Description;
        _priceText.text = string.Format(_priceTextFormat, model.Config.Price);
    }
}
