using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 전역에서 하나만 존재하도록 보장되는 게임 매니저 클래스
/// 게임에서 하나의 객체만 필요한 매니저 등을 관리한다.
/// </summary>
public class GameManager : MonoBehaviour
{
    // 유일한 GameManager 객체를 가리키는 변수
    static GameManager _instance;

    [SerializeField] HeroData _heroData = new HeroData();
    public HeroData HeroData => _heroData;

    /// <summary>
    /// GameManager의 싱글톤 객체(인스턴스)
    /// 필요 시 Scene에 GameObject를 자동으로 새로 생성하고
    /// GameManager Component를 추가
    /// </summary>
    public static GameManager Instance
    {
        get
        {
            // 유일한 GameManager가 안 만들어져 있으면
            if(_instance == null)
            {
                _instance = FindAnyObjectByType<GameManager>();
                if(_instance == null)
                {
                    // "GameManager란 이름으로 Scene에 새 GameObject 생성"
                    GameObject go = new GameObject("GameManager");
                    
                    // 만들어진 GameObject에 GameManager Component 추가
                    _instance = go.AddComponent<GameManager>();
                    
                    // Scene 전환 시 GameObject 제거 방지
                    DontDestroyOnLoad(go);
                } 
            }
            return _instance;
        }
    }

    /// <summary>
    /// 싱글톤 초기화 및 중복 개체(인스턴스) 제거
    /// </summary>
    private void Awake()
    {
        // 유일한 GameManager 객체가 없었으면
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        // 유일한 GameManager 객체가 이미 있으면
        else
        {
            Destroy(gameObject);
        }
    }
}
