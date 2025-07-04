using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 유저가 보유한 Item들을 관리하는 클래스
/// </summary>
public class Inventory : MonoBehaviour
{
    const int _slotCount = 18;

    [Header("---- 아이템 설정 데이터 ----")]
    [SerializeField] ItemConfig[] _itemConfigs;
    Dictionary<string, ItemConfig> _itemConfigMap = new();

    [Header("---- 컴포넌트 참조 ----")]
    [SerializeField] ItemView[] _itemViews;
    [SerializeField] GameObject _inventoryPanel;
    // 유저가 보유하고 있는 아이템 배열   
    ItemModel[] _itemModels = new ItemModel[_slotCount];

    private void Awake()
    {
        foreach(var itemConfig in _itemConfigs)
        {
            _itemConfigMap[itemConfig.Id] = itemConfig;
        }
    }

    private void Start()
    {
        for(int i = 0; i < _itemViews.Length; i++)
        {
            _itemViews[i].SetItemModel(_itemModels[i]);
        }
    }

    /// <summary>
    /// 아이템 ID로 아이템을 획득하는 함수
    /// </summary>
    /// <param name="Id">획득할 아이템의 ID</param>
    public void AddItem(string id)
    {
        if(_itemConfigMap.ContainsKey(id) == false)
        {
            Debug.LogWarning($"존재하지 않는 아이템입니다. (ID : {id})");
            return;
        }

        // 아이템 설정 데이터 검색
        ItemConfig itemConfig = _itemConfigMap[id];

        for(int i = 0; i < _itemModels.Length; i++)
        {
            if (_itemModels[i] == null)
            {
                // 아이템 설정 데이터로 아이템 모델 생성
                _itemModels[i] = new ItemModel(itemConfig);

                // 아이템 뷰에 아이템 모델 설정
                _itemViews[i].SetItemModel(_itemModels[i]);
                return;
            }
        }

        Debug.Log($"아이템 슬롯이 가득 찼습니다.");
    }

    // 테스트
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            AddItem("Mace");
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            AddItem("Apple");   
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            _inventoryPanel.SetActive(true);
        }
    }
}
