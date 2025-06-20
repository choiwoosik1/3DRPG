using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 적 캐릭터를 담당하는 클래스
/// </summary>
public class Enemy : MonoBehaviour
{
    [Header("---- 컴포넌트 참조 ----")]
    [SerializeField] CombatCharacterModel _model;

    private void Start()
    {
        _model.OnDead += OnDead;
    }

    private void Update()
    {
        // 테스트
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _model.TakeHit(10);
        }
    }

    void OnDead()
    {
        Destroy(gameObject);
    }
}
