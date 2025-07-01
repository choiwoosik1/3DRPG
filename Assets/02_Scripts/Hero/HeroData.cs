using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 주인공 캐릭터 정보를 저장하는 데이터 클래스
/// 이름, 스탯 etc.
/// </summary>

[System.Serializable]           // Inspector View 확인용. 수정용 X
public class HeroData
{
    [SerializeField] string _heroName = "Hero";
    public string HeroName => _heroName;
    
    /// <summary>
    /// 주인공 캐릭터 이름을 변경하는 함수
    /// </summary>
    /// <param name="heroName"></param>
    public void SetHeroName(string heroName)
    {
        _heroName = heroName;
    }
}
