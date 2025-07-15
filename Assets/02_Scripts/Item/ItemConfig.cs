using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Consumable,         // 소모성 아이템
    NonConsumable,      // 비소모성 아이템
    Equipment,          // 장비 아이템
}

/// <summary>
/// 아이템의 설정 데이터 클래스
/// </summary>

[CreateAssetMenu(fileName = "ItemConfig", menuName = "GameSettings/ItemConfig")]
public class ItemConfig : ScriptableObject
{
    [SerializeField] string _id;                                // 아이디
    [SerializeField] ItemType _itemType;                        // 아이템 타입
    [SerializeField] string _itemName;                          // 아이템 이름
    [TextArea(3, 5)][SerializeField] string _description;       // 아이템 설명
    [SerializeField] int _price;                                // 아이템 가격
    [SerializeField] Sprite _iconSprite;                        // 아이콘 스프라이트
    [SerializeField] ItemEffect _effect;                        // 아이템 효과

    public string Id => _id;
    public ItemType ItemType => _itemType;
    public string ItemName => _itemName;
    public int Price => _price;
    public Sprite IconSprite => _iconSprite;
    public string Description => _description;
    public ItemEffect Effect => _effect;
}
