using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public enum LineCommandType
{
    Speech = -1,
    Name,
}

public class DialoguePresenter : MonoBehaviour
{
    [SerializeField] string[] _commandWords;            // 명령어 배열
    [SerializeField] DialogueView _view;

    Coroutine _typingCoroutine;

    DialogueModel _model;
    int _lineIndex = 0;

    /// <summary>
    /// 대화 재생 종료 이벤트
    /// </summary>
    public event Action OnEnded;

    /// <summary>
    /// 대화를 시작하는 함수
    /// </summary>
    /// <param name="model"></param>
    public void Play(DialogueModel model)
    {
        _model = model;

        _view.gameObject.SetActive(true);

        _lineIndex = 0;
        _view.SetNameText(string.Empty);

        PlayCurrentLine();
    }

    /// <summary>
    /// 대화를 끝내는 함수
    /// </summary>
    public void Stop()
    {
        _view.gameObject.SetActive(false);
        OnEnded?.Invoke();
    }

    /// <summary>
    /// 대화 다음 줄로 넘어가는 함수
    /// </summary>
    public void Next()
    {
        _lineIndex++;
        PlayCurrentLine();
    }

    /// <summary>
    /// 현재 대사 줄을 재생하는 함수
    /// </summary>
    void PlayCurrentLine()
    {
        string line = _model.GetLines(_lineIndex);
        if(string.IsNullOrEmpty(line) == false)
        {
            LineCommandType commandType = ParseLine(line, out string str);
            switch (commandType)
            {
                case LineCommandType.Name:
                    _view.SetNameText(str);
                    break;
                default:
                    if (_typingCoroutine != null)
                        StopCoroutine(_typingCoroutine);
                    _typingCoroutine = StartCoroutine(TypeText(str));
                    break;
            }

            if(commandType != LineCommandType.Speech)
            {
                _lineIndex++;
                PlayCurrentLine();
            }
        }
        else
        {
            //대화 종료
            Stop();
        }
    }

    /// <summary>
    /// 대사 한 줄의 명령어와 내용을 반환해 주는 함수
    /// </summary>
    /// <param name="line">해독할 한 줄</param>
    /// <param name="str">명령어 내용</param>
    /// <returns>명령어</returns>
    LineCommandType ParseLine(string line, out string str)
    {
        str = line;

        for(int i = 0; i< _commandWords.Length; i++)
        {
            if(line.StartsWith(_commandWords[i]) == true)
            {
                if(line.Length == _commandWords[i].Length)
                {
                    str = string.Empty;
                }
                else
                {
                    // Substring: 매개변수 부터 끝까지
                    // 촌장 부분만 가져오는 것과 동일
                    str = line.Substring(_commandWords[i].Length);
                }
                return (LineCommandType)i;
            }
        }
        return LineCommandType.Speech;
    }

    IEnumerator TypeText(string sentence)
    {
        _view.SetSpeechText(string.Empty); // 기존 텍스트 초기화
        foreach (char c in sentence)
        {
            _view.AppendSpeechChar(c); // 글자 추가
            yield return new WaitForSeconds(0.05f); // 0.05초 텀
        }
    }
}
