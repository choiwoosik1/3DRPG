using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 대사를 표시해주는 View
/// </summary>
public class DialogueView : MonoBehaviour
{
    [SerializeField] float _characterSpan;          // 한 글자 출력에 걸리는 시간(초)

    [Header("---- 컴포넌트 참조 ----")]
    [SerializeField] TextMeshProUGUI _nameText;     // 캐릭터 이름 텍스트
    [SerializeField] TextMeshProUGUI _speechText;   // 대사 본문 텍스트

    string _speech;                                 // 현재 출력중인 문자열
    Coroutine _speechRoutine;                       // 출력 코루틴
    bool _isPlaying;                                // 대사 출력 중인지 여부

    public bool IsPlaying => _isPlaying;

    public void beginspeech(string speech)
    {
        _speech = speech;

        // 코루틴 재생
        _speechRoutine = StartCoroutine(SpeechRoutine());
    }

    /// <summary>
    /// 대사 출력을 종료하는 함수
    /// </summary>
    public void EndSpeech()
    {
        // 코루틴 정지
        if(_speechRoutine != null)
        {
            // 변수를 정지해야함 SpeechRotine()이 들어가면 안됨
            StopCoroutine(_speechRoutine);
            _speechRoutine = null;
        }

        _isPlaying = false;
        _speechText.text = _speech;
    }

    public void SetNameText(string name)
    {
        _nameText.text = name;
    }
    
    /// <summary>
    /// 대사 한 줄 출력 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator SpeechRoutine()
    {
        _isPlaying = true;

        // Time.timeScale에 영향을 받는 WaitForSeconds
        //WaitForSeconds waitForSeconds = new WaitForSeconds(_characterSpan);

        // Time.timeScale에 영향을 받지 않는 WaitForSeconds
        WaitForSecondsRealtime waitForSeconds = new WaitForSecondsRealtime(_characterSpan);


        for(int i = 1; i <= _speech.Length; i++)
        {
            _speechText.text = _speech.Substring(0, i);
            yield return waitForSeconds;

            // 매 반복문 마다 WaitForSeconds를 만들어냄
            //yield return new WaitForSeconds(_characterSpan);
        }
        _isPlaying = false;
    }

    //public void SetSpeechText(string text)
    //{
    //    _speechText.text = text;
    //}

    //public void AppendSpeechChar(char c)
    //{
    //    _speechText.text += c;
    //}
}
