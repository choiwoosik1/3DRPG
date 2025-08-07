using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

/// <summary>
/// 자식 게임오브젝트 컴포넌트를 enum으로 바인딩하여 쉽게 접근하고 관리할 수 있도록 도와주는 추상 클래스
/// </summary>
public abstract class ViewBase : MonoBehaviour
{
    // 바인딩된 컴포넌트를 저장하는 딕셔너리
    protected Dictionary<Type, Component[]> _componentMap = new Dictionary<Type, Component[]>();

    /// <summary>
    /// 특정 타입의 컴포넌트를 enum에 바인딩.
    /// </summary>
    protected void Bind<T>(Type type) where T : Component
    {
        // enum의 각 이름 배열로 가져오기
        string[] names = Enum.GetNames(type);

        // 각 enum string값-int값 딕셔너리
        Dictionary<string, int> nameMap = new Dictionary<string, int>();
        for (int i = 0; i < names.Length; i++)
            nameMap[names[i]] = i;

        // T 타입에 대한 컴포넌트 배열
        Component[] components = new Component[names.Length];
        _componentMap[typeof(T)] = components;

        // T 타입 컴포넌트 모두 찾기
        Component[] founds = GetComponentsInChildren<T>(true);
        foreach (var found in founds)
        {
            // 각 enum의 순서에 맞게 T 타입 컴포넌트 배열 설정
            if (nameMap.TryGetValue(found.name, out var idx))
                components[idx] = found;
        }
    }


    /// <summary>
    /// 바인딩된 컴포넌트 반환.
    /// </summary>
    protected T Get<T>(int idx) where T : Component
    {
        if (_componentMap.TryGetValue(typeof(T), out var components) == true
            && idx >= 0 && idx < components.Length)
            return components[idx] as T;

        Debug.LogError($"Component of type {typeof(T).Name} at index {idx} not found in {_componentMap[typeof(T)]}.");
        return null;
    }

    /// <summary>
    /// TextMeshProUGUI 텍스트 설정.
    /// </summary>
    public void SetTMP(int idx, string text)
    {
        TextMeshProUGUI tmp = GetTMP(idx);
        if (tmp)
            tmp.text = text;
    }

    /// <summary>
    /// Image 스프라이트 설정.
    /// </summary>
    public void SetImage(int idx, Sprite sprite)
    {
        Image image = GetImage(idx);
        if (image)
            image.sprite = sprite;
    }

    /// <summary>
    /// Image FillAmount 값 설정
    /// </summary>
    /// <param name="idx"></param>
    /// <param name="amount"></param>
    public void SetImageFillAmount(int idx, float amount)
    {
        Image image = GetImage(idx);
        if (image)
            image.fillAmount = amount;
    }

    protected Text GetText(int idx)
    {
        return Get<Text>(idx);
    }
    protected Button GetButton(int idx)
    {
        return Get<Button>(idx);
    }
    protected Image GetImage(int idx)
    {
        return Get<Image>(idx);
    }
    protected TextMeshProUGUI GetTMP(int idx)
    {
        return Get<TextMeshProUGUI>(idx);
    }
    protected Transform GetTransform(int idx)
    {
        return Get<Transform>(idx);
    }
}