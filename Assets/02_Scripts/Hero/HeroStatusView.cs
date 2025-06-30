using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 주인공 캐릭터 UI 담당
/// </summary>
public class HeroStatusView : MonoBehaviour
{
    [SerializeField] Image _hpBar;
    [SerializeField] Image _mpBar;
    [SerializeField] TextMeshProUGUI _heroNameText;


    public void SetHpBar(float currentHp, float maxHp)
    {
        _hpBar.fillAmount = currentHp / maxHp;
    }

    public void SetMpBar(float currentMp, float maxMp)
    {
        _mpBar.fillAmount = currentMp / maxMp;
    }

    public void SetHeroNameText(string heroName)
    {
        _heroNameText.text = heroName;
    }
}
