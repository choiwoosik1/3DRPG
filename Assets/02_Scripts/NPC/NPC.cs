using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    [Header("---- 컴포넌트 참조 ----")]
    [SerializeField] DialogueInteract _interact;
    [SerializeField] PathFollower _pathFollower;

    private void Start()
    {
        _interact.OnBegun += OnInteractionBegun;
        _interact.OnEnded += OnInteractionEnded;

        _pathFollower.StartFollowing();
    }

    /// <summary>
    /// 상호작용 시작 시 자동으로 호출되는 함수
    /// </summary>
    void OnInteractionBegun()
    {
        _pathFollower.StopFollowing();
    }

    /// <summary>
    /// 상호작용 종료 시 자동으로 호출되는 함수
    /// </summary>
    void OnInteractionEnded()
    {
        _pathFollower.StartFollowing();
    }
}
