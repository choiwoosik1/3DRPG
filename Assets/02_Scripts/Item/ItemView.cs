using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 아이템의 정보를 UI에 표시하는 클래스
/// </summary>
public class ItemView : MonoBehaviour
{
    ItemModel _model;
    [SerializeField] Image _iconImage;

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
}
