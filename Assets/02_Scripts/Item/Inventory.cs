using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;

/// <summary>
/// 유저가 보유한 Item들을 관리하는 클래스
/// </summary>
public class Inventory : MonoBehaviour
{
    const int _slotCount = 18;
    TextMeshProUGUI _descriptText;
    TextMeshProUGUI _nameText;

    [Header("---- 아이템 설정 데이터 ----")]
    [SerializeField] ItemConfig[] _itemConfigs;
    Dictionary<string, ItemConfig> _itemConfigMap = new();

    [Header("---- 컴포넌트 참조 ----")]
    [SerializeField] ItemView[] _itemViews;
    [SerializeField] ItemView _selectedItemView;
    [SerializeField] HeroModel _heroModel;
    //[SerializeField] ItemDescription _itemDescView;
    [SerializeField] EquipController _equipContoller;
    [SerializeField] ItemDragController _dragController;

    public EquipController EquipController => _equipContoller;
    public HeroModel HeroModel => _heroModel;

    // 유저가 보유하고 있는 아이템 배열   
    ItemModel[] _itemModels = new ItemModel[_slotCount];

    // 현재 선택 중인 슬롯 번호
    int _selectedSlotIndex = -1;

    // 현재 인벤토리 메뉴가 열려있는지 여부
    bool _hasOpened = false;

    private void Awake()
    {
        foreach (var itemConfig in _itemConfigs)
        {
            _itemConfigMap[itemConfig.Id] = itemConfig;
        }
    }

    private void Start()
    {
        for (int i = 0; i < _itemViews.Length; i++)
        {
            _itemViews[i].Initialize(this, i, _dragController);
            _itemViews[i].SetItemModel(_itemModels[i]);
        }

        _equipContoller.Initialize();
    }

    /// <summary>
    /// 아이템 설정 데이터로 아이템 모델을 만들어 반환해 주는 함수
    /// </summary>
    /// <param name="itemConfig"></param>
    /// <returns></returns>
    public ItemModel CreateItemModel(ItemConfig itemConfig)
    {
        switch(itemConfig)
        {
            case EquipmentItemConfig equipmentItemConfig:
                return new EquipmentItemModel(equipmentItemConfig);
            default:
                return new ItemModel(itemConfig);
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

        // 아이템 설명 뷰 강제 숨김
        //HideItemDescVIew();
    }

    /// <summary>
    /// 슬롯 번호로 아이템 모델을 반환을 시도하는 함수
    /// </summary>
    /// <param name="slotIndex">슬롯 번호</param>
    /// <param name="itemModel">찾은 아이템 모델</param>
    /// <returns>아이템 모델 존재 여부</returns>
    public bool TryGetItemModel(int slotIndex, out ItemModel itemModel)
    {
        itemModel = null;

        // 인덱스 범위 검사
        if (slotIndex < 0 || slotIndex >= _itemViews.Length) return false;

        itemModel = _itemModels[slotIndex];
        return itemModel != null;
    }

    /// <summary>
    /// 아이템 슬롯이 비어 있는지 여부를 반환하는 함수
    /// </summary>
    /// <param name="slotIndex">슬롯 번호</param>
    /// <returns></returns>
    public bool GetIsEmptySlot(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= _itemViews.Length)
            return false;

        ItemModel itemModel = _itemModels[slotIndex];
        return itemModel == null;
    }

    /// <summary>
    /// 아이템 ID로 아이템을 획득하는 함수
    /// </summary>
    /// <param name="Id">획득할 아이템의 ID</param>
    public void AddItem(string id)
    {
        if (_itemConfigMap.ContainsKey(id) == false)
        {
            Debug.LogWarning($"존재하지 않는 아이템입니다. (ID : {id})");
            return;
        }

        // 아이템 설정 데이터 검색
        ItemConfig itemConfig = _itemConfigMap[id];

        for (int i = 0; i < _itemModels.Length; i++)
        {
            if (_itemModels[i] == null)
            {
                // 아이템 설정 데이터로 아이템 모델 생성
                _itemModels[i] = CreateItemModel(itemConfig);

                // 아이템 획득 시 실행되어야 하는 함수 호출
                _itemModels[i].Acquire(this, i);

                // 아이템 뷰에 아이템 모델 설정
                _itemViews[i].SetItemModel(_itemModels[i]);
                return;
            }
        }

        Debug.Log($"아이템 슬롯이 가득 찼습니다.");
    }

    /// <summary>
    /// 이미 있는 아이템을 인벤토리에 추가 시도하는 함수
    /// </summary>
    /// <param name="itemModel"></param>
    /// <returns></returns>
    public bool TryAddItem(ItemModel itemModel)
    {
        for(int i = 0; i < _itemModels.Length; i++)
        {
            if (_itemModels[i] == null)
            {
                _itemModels[i] = itemModel;
                itemModel.SetSlotIndex(i);
                _itemViews[i].SetItemModel(itemModel);
                return true;
            }
        }

        Debug.Log("아이템 슬롯이 가득 찼습니다.");
        return false;
    }

    /// <summary>
    /// 아이템을 제거하는 함수
    /// </summary>
    /// <param name="slotIndex">제거할 아이템 슬롯 번호</param>
    public void RemoveItem(int slotIndex)
    {
        if (TryGetItemModel(slotIndex, out ItemModel itemModel) == true)
        {
            itemModel.Remove();
            _itemModels[slotIndex] = null;
            _itemViews[slotIndex].SetItemModel(_itemModels[slotIndex]);
         
        }
    }



    /// <summary>
    /// 아이템을 사용하는 함수
    /// </summary>
    /// <param name="slotIndex">사용할 아이템 슬롯 번호</param>
    public void UseItem(int slotIndex)
    {
        if(TryGetItemModel(slotIndex, out ItemModel itemModel) == true)
        {
            if (itemModel.ItemType == ItemType.Consumable || itemModel.ItemType == ItemType.Equipment)
            {
                RemoveItem(slotIndex);
            }
            itemModel.Use();
        }
    }

    /// <summary>
    /// 두 아이템의 자리를 바꾸는 함수
    /// </summary>
    /// <param name="a">첫 번째 아이템의 슬롯 번호</param>
    /// <param name="b">두 번째 아이템의 슬롯 번호</param>
   public void SwapItems(int a, int b)
    {
        Debug.Log($"{a}번 아이템과 {b}번 아이템 교환");

        // a번 아이템 모델과 b번 아이템 모델의 교환
        ItemModel temp = _itemModels[a];
        _itemModels[a] = _itemModels[b];
        _itemModels[b] = temp;

        _itemModels[a]?.SetSlotIndex(a);
        _itemModels[b]?.SetSlotIndex(b);

        // 자리 바꾼 번호에 해당하는 ItemView 갱신
        _itemViews[a].SetItemModel(_itemModels[a]);
        _itemViews[b].SetItemModel(_itemModels[b]);
    }

    public void BeginDrag(int slotIndex)
    {
        ItemModel item = _itemModels[slotIndex];

        // 해당 슬롯 번호에 아이템이 있는 경우
        if (item != null)
        {
            _selectedSlotIndex = slotIndex;
            _itemViews[_selectedSlotIndex].Hide(true);
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
        //HideItemDescVIew();
    }

    public void Drop(int slotIndex)
    {
        if (_selectedSlotIndex < 0) return;

        SwapItems(_selectedSlotIndex, slotIndex);
        //ShowToolTip(slotIndex);
    }

    public void EndDrag()
    {
        _selectedItemView.gameObject.SetActive(false);

        if (_selectedSlotIndex < 0) return;
        _itemViews[_selectedSlotIndex].Hide(false);
        _selectedSlotIndex = -1;
    }

    /// <summary>
    /// 아이템 설명 뷰(툴팁)을 표시하는 함수
    /// </summary>
    /// <param name="slotIndex">표시할 아이템 슬롯 번호</param>
    //public void ShowToolTip(int slotIndex)
    //{
    //    if (slotIndex < 0 || slotIndex >= _itemModels.Length) return;

    //    // 표시할 아이템 모델 찾기
    //    ItemModel itemModel = _itemModels[slotIndex];
    //    if (itemModel == null) return;

    //    //_itemDescView.SetItemModel(itemModel);
    //    //_itemDescView.transform.position = _itemViews[slotIndex].transform.position;
    //    //_itemDescView.gameObject.SetActive(true);
    //}
    
    /// <summary>
    /// 아이템 설명 뷰(툴팁)를 숨기는 함수
    /// </summary>
    //public void HideItemDescVIew()
    //{
    //    //_itemDescView.gameObject.SetActive(false);
    //}

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

        if (Input.GetKeyDown(KeyCode.F5))
        {
            AddItem("Rings");
        }

        if (Input.GetKeyDown(KeyCode.F6))
        {
            AddItem("Sword");
        }
    }
}
