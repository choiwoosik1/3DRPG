using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 런타임 데이터 클래스
/// </summary>
[System.Serializable]
public class ItemModel
{
    [SerializeField] ItemConfig _config;
    public ItemConfig Config => _config;
    public ItemModel(ItemConfig config)
    {
        _config = config;
    }
}
        