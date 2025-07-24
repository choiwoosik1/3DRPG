using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPC : MonoBehaviour
{
    FlagSystem _flagSystem;

    [Header("---- 설정 데이터 ----")]
    [SerializeField] DialogueConfig[] _configs;             // 대화 설정 데이터 배열

    List<DialogueConfig> _sortedConfigs;                    // 우선순위 내림차순으로 정렬된 대화 설정 데이터 리스트

    private void Awake()
    {
        // OrderByDescending : 내림차순 정렬
        // _configs에 들어있는 config를 config.Priority 내림차순으로 정렬
        _sortedConfigs = _configs.OrderByDescending(config => config.Priority).ToList();
    }

    private void Start()
    {
        _flagSystem = FindAnyObjectByType<FlagSystem>();
    }

    /// <summary>
    /// 상호작용을 실행하는 함수
    /// </summary>
    public void Interact()
    {
        foreach (var config in _sortedConfigs)
        {
            // 필요한 플래그 조건 통과 여부
            // 1) 대화 설정 데이터 자체가 필요 플래그가 없는 경우이거나
            // 2) 대화 설정 데이터의 필요 플래그가 켜져있는 경우
            bool requiredPassed = string.IsNullOrEmpty(config.RequireFlag) || 
                _flagSystem.ContainsFlag(config.RequireFlag);

            // 없어야 하는 플래그 조건 통과 여부
            // 1) 대화 설정 데이터 자체가 숨김 플래그가 없는 경우이거나
            // 2) 대화 설정 데이터의 숨김 플래그가 꺼져있는 경우
            bool hiddenPassed = string.IsNullOrEmpty(config.HiddenFlag) ||
                (_flagSystem.ContainsFlag(config.HiddenFlag) == false);

            if(requiredPassed && hiddenPassed)
            {
                // DialogueSystem 찾기
                DialogueSystem dialogueSystem = FindObjectOfType<DialogueSystem>();

                // DialogueModel 생성
                DialogueModel model = new DialogueModel(config);

                // 대화 재생
                dialogueSystem.PlayDialogue(model);
                return;
            }
        }
    }

    // 테스트용 치트키
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F11))
        {
            Interact();
        }
    }
}
