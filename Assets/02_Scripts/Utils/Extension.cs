using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension
{
    public static bool Contains(this LayerMask layerMask, int layer)
    {
        return Utils.Contains(layerMask, layer);
    }

    public static void DestroyOrReturnToPool(this GameObject go)
    {
        Utils.DestrotyOrReturnToPool(go);
    }

    public static int Choose(IReadOnlyList<float> probs)
    {
        return Utils.Choose(probs);
    }

    public static T ChooseRandom<T>(IReadOnlyList<T> list)
    {
        return Utils.ChooseRandom<T>(list);
    }


    /// <summary>
    /// 리스트를 랜덤한 순서로 Shuffle하는 함수
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    public static void Shuffle<T>(this IList<T> list)
    {
        Utils.Shuffle(list);
    }

    /// <summary>
    /// 게임 오브젝트에서 T 타입인 컴포넌트를 찾아서 반환하거나, 
    /// 없으면 새로 추가해서 반환하는 함수
    /// </summary>
    /// <typeparam name="T">찾거나 추가할 컴포넌트의 타입</typeparam>
    /// <param name="go">대상 게임 오브젝트</param>
    /// <returns></returns>
    public static T GetOrAddComponent<T>(this GameObject go) where T : Component
    {
        return Utils.GetOrAddComponent<T>(go);
    }

    /// <summary>
    /// 게임 오브젝트에서 T 타입인 컴포넌트를 찾아서 반환하거나, 
    /// 없으면 새로 추가해서 반환하는 함수
    /// </summary>
    /// <typeparam name="T">찾거나 추가할 컴포넌트의 타입</typeparam>
    /// <param name="target">대상 게임 오브젝트</param>
    /// <returns></returns>
    public static T GetOrAddComponent<T>(this Component target) where T : Component
    {
        return Utils.GetOrAddComponent<T>(target);
    }
}
