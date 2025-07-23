using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 대사 설정 데이터
/// </summary>
[CreateAssetMenu(fileName = "DialogueConfig", menuName = "GameSettings/Dialogue")]
public class DialogueConfig : ScriptableObject
{
    [SerializeField] string _id;                        // 고유번호
    [SerializeField][TextArea(5, 10)] string _content;  // 대사 내용

    public string Id => _id;
    public string Content => _content;
}
