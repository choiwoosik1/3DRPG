using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 대사 런타임 데이터 모델
/// </summary>
public class DialogueModel
{
    DialogueConfig _config;
    string[] _lines;

    /// <summary>
    /// 대화 종료 이벤트
    /// </summary>
    public event Action OnEnded;

    public DialogueModel(DialogueConfig config)
    {
        _config = config;

        _lines = _config.Content.Split('\n');
    }

    /// <summary>
    /// 대사의 한 줄을 반환해 주는 함수
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public string GetLines(int index)
    {
        if (index < 0 || index >= _lines.Length) return null;

        return _lines[index];
    }

    /// <summary>
    /// 대화 종료 이벤트를 발행하는 함수
    /// </summary>
    public void InvokeEnded()
    {
        OnEnded?.Invoke();
    }
}
