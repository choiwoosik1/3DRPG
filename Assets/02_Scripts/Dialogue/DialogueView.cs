using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 대사를 표시해주는 View
/// </summary>
public class DialogueView : MonoBehaviour
{
    [Header("---- 컴포넌트 참조 ----")]
    [SerializeField] TextMeshProUGUI _nameText;     // 캐릭터 이름 텍스트
    [SerializeField] TextMeshProUGUI _speechText;   // 대사 본문 텍스트

    string _speech;

    //public void BeginSpeech(string speech)
    //{
    //    _speechText.text = speech;
    //}

    public void SetNameText(string name)
    {
        _nameText.text = name;
    }

    public void SetSpeechText(string text)
    {
        _speechText.text = text;
    }

    public void AppendSpeechChar(char c)
    {
        _speechText.text += c;
    }
}
