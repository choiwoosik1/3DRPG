using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Flag를 관리하는 클래스
/// </summary>
public class FlagSystem : MonoBehaviour
{
    // HashSet: 중복 비허용 집합
    HashSet<string> _flags = new HashSet<string>();


    [Header("---- 디버깅용 ----")]
    [SerializeField] List<string> _flagsForTest;

    private void Awake()
    {
        // Global 이벤트 구독
        EventBus.OnAddFlag += AddFlags;
        EventBus.OnRemoveFlag += RemoveFlag;
    }

    private void OnDestroy()
    {
        // Global 이벤트 구독 해제(Global 이벤트는 반드시 구독 해제를 해야한다.)
        // 구독을 해제하지 않으면 FlagSystem객체가 Destroy되어도 참조가 유지되어
        // 메모리에 남아 메모리 누수가 발생할 수 있음.
        EventBus.OnAddFlag -= AddFlags;
        EventBus.OnRemoveFlag -= RemoveFlag;
    }

    /// <summary>
    /// 플래그를 추가하는 함수
    /// </summary>
    /// <param name="flag"></param>
    public void AddFlags(string flag)
    {
        _flags.Add(flag);
        
        // 전처리기
#if UNITY_EDITOR
        // 에디터 전용
        _flagsForTest = new List<string>(_flags);
#endif
    }

    /// <summary>
    /// 플래그를 제거하는 함수
    /// </summary>
    /// <param name="flag"></param>
    public void RemoveFlag(string flag)
    {
        _flags.Remove(flag);

        // 전처리기
#if UNITY_EDITOR
        // 에디터 전용
        _flagsForTest = new List<string>(_flags);
#endif
    }

    /// <summary>
    /// Flag가 현재 있는지 여부를 반환해 주는 함수
    /// </summary>
    /// <param name="flag">검사할 플래그</param>
    /// <returns></returns>
    public bool ContainsFlag(string flag)
    {
        return _flags.Contains(flag);
    }
}
