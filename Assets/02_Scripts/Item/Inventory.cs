using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
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
    [SerializeField] ItemView _selectedItemView;
    [SerializeField] GameObject _inventoryPanel;


    // 유저가 보유하고 있는 아이템 배열   
    ItemModel[] _itemModels = new ItemModel[_slotCount];

    // 현재 선택 중인 슬롯 번호
    int _selectedSlotIndex = -1;

    // 현재 인벤토리 메뉴가 열려있는지 여부
    bool _hasOpened = false;

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
            _itemViews[i].Initialize(this, i);
            _itemViews[i].SetItemModel(_itemModels[i]);
    
        }
    }

    /// <summary>
    /// 인벤토리 메뉴를 여닫는 함수
    /// </summary>
    public void Toggle()
    {
        _hasOpened = !_hasOpened;
        gameObject.SetActive(_hasOpened);
        
        // 삼항 연산
        Time.timeScale = _hasOpened ? 0 : 1;
        Cursor.lockState = _hasOpened ? CursorLockMode.Confined : CursorLockMode.Locked;

        // 드래그 상태에서 인벤토리를 껐을 때 오류 생길 수 있음 방지
        EndDrag();
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

    /// <summary>
    /// 두 아이템의 자리를 바꾸는 함수
    /// </summary>
    /// <param name="a">첫 번째 아이템의 슬롯 번호</param>
    /// <param name="b">두 번째 아이템의 슬롯 번호</param>
    void SwapItems(int a, int b)
    {
        Debug.Log($"{a}번 아이템과 {b}번 아이템 교환");

        // a번 아이템 모델과 b번 아이템 모델의 교환
        ItemModel temp = _itemModels[a];
        _itemModels[a] = _itemModels[b];
        _itemModels[b] = temp;

        // 자리 바꾼 번호에 해당하는 ItemView 갱신
        _itemViews[a].SetItemModel(_itemModels[a]);
        _itemViews[b].SetItemModel(_itemModels[b]);
    }

    public void BeginDrag(int slotIndex)
    {
        ItemModel item = _itemModels[slotIndex];

        // 해당 슬롯 번호에 아이템이 있는 경우
        if(item != null)
        {
            _selectedSlotIndex = slotIndex;
            _selectedItemView.SetItemModel(item);
            _selectedItemView.gameObject.SetActive(true);
        }

        // 해당 슬롯 번호에 아이템이 없는 경우
        else
        {
            _selectedSlotIndex = -1;
            _selectedItemView.gameObject.SetActive(false);
        }
    }

    public void Dragging(Vector2 pos)
    {
        if (_selectedSlotIndex < 0) return;

        _selectedItemView.transform.position = pos;
    }

    public void Drop(int slotIndex)
    {
        if (_selectedSlotIndex < 0) return;

        SwapItems(_selectedSlotIndex, slotIndex);
    }

    public void EndDrag()
    {
        _selectedItemView.gameObject.SetActive(false);

        if (_selectedSlotIndex < 0) return;
        _selectedSlotIndex = -1;
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

        if (Input.GetKeyDown(KeyCode.F3))
        {
            AddItem("Boots");
        }

        if (Input.GetKeyDown(KeyCode.F4))
        {
            AddItem("Armor");
        }
    }
}
