using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton Pattern(싱글톤 패턴)
// 프로그램 전체에서 단 하나의 객체만 존재하도록 보장하고
// 그 객체에 전역적으로(Global) 접근할 수 있게 해주는 디자인 패턴

// -> Scene끼리 어떤 데이터를 주고받아야 할 때
// -> 게임 전체에 필요한 기능이 있을 경우 (Resource 관리, Sound 재생, 로컬 라이징등)

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    //static GameManager _Instance;
    //public static GameManager Instance
    //{
    //    get => _Instance;
    //    private set => _Instance = value;
    //}

    [SerializeField] string _heroName = "Hero";
    public string HeroName => _heroName;

    private void Awake()
    {
        // "Instance" 변수가 아무것도 가리키지 않는 경우
        if(Instance == null)
        {
            // 자신 객체를 "Instance"로 설정
            Instance = this;

            // Scene 전환 후에도 게임 오브젝트가 파괴되지 않게 설정
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }
    
    public void SetHeroName(string heroName)
    {
        _heroName = heroName;
    }
}
