using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public static class Utils
{
    public static bool Contains(LayerMask layerMask, int layer)
    {
        return ((1 << layer) & layerMask.value) != 0;
    }

    public const float Epsilon = 0.01f;

    /// <summary>
    /// 게임 오브젝트가 Object Pooling을 사용하면 풀로 되돌리고,
    /// Pooling을 하지 않는 게임 오브젝트면 파괴하는 함수
    /// </summary>
    /// <param name="go"></param>
    public static void DestrotyOrReturnToPool(GameObject go)
    {
        Poolable poolable = go.GetComponent<Poolable>();
        if (poolable != null)
        {
            poolable.ReturnToPool();
        }
        else
        {
            Object.Destroy(go);
        }
    }

    // IReadOnlyList는 리스트에서 읽기 전용 기능만 제공하는 인터페이스

    /// <summary>
    /// 확률에 따라 선택된 Index를 반환하는 함수
    /// </summary>
    /// <param name="probs"></param>
    /// <returns></returns>
    public static int Choose(IReadOnlyList<float> probs)
    {
        float total = 0;
        foreach (float prob in probs)
        {
            if (prob > 0)
            {
                total += prob;
            }
        }

        float randomValue = Random.value * total;

        for (int i = 0; i < probs.Count; i++)
        {
            if (probs[i] <= 0) continue;

            if (randomValue <= probs[i])
            {
                return i;
            }
            else
            {
                randomValue -= probs[i];
            }
        }
        return 0;
    }

    // Generic : 타입을 마치 변수처럼 다루는 방식

    /// <summary>
    /// 요소들 중 랜덤한 요소를 골라 반환하는 함수
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>

    public static T ChooseRandom<T>(IReadOnlyList<T> list)
    {
        if (list == null || list.Count == 0)
        {
            return default(T);
        }

        int index = Random.Range(0, list.Count);
        return list[index];
    }

    /// <summary>
    /// 리스트를 랜덤한 순서로 Shuffle하는 함수
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    public static void Shuffle<T>(IList<T> list)
    {
        int count = list.Count;
        for (int i = 0; i < count; i++)
        {
            int k = Random.Range(i, count);
            T temp = list[k];
            list[k] = list[i];
            list[i] = temp;
        }
    }

    /// <summary>
    /// 게임 오브젝트에서 T 타입인 컴포넌트를 찾아서 반환하거나, 
    /// 없으면 새로 추가해서 반환하는 함수
    /// </summary>
    /// <typeparam name="T">찾거나 추가할 컴포넌트의 타입</typeparam>
    /// <param name="go">대상 게임 오브젝트</param>
    /// <returns></returns>
    public static T GetOrAddComponent<T>(GameObject go) where T : Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
        {
            component = go.AddComponent<T>();
        }

        return component;
    }

    /// <summary>
    /// 게임 오브젝트에서 T 타입인 컴포넌트를 찾아서 반환하거나, 
    /// 없으면 새로 추가해서 반환하는 함수
    /// </summary>
    /// <typeparam name="T">찾거나 추가할 컴포넌트의 타입</typeparam>
    /// <param name="target">대상 게임 오브젝트</param>
    /// <returns></returns>
    public static T GetOrAddComponent<T>(Component target) where T : Component
    {
        T component = target.GetComponent<T>();
        if(component == null)
        {
            component = target.gameObject.AddComponent<T>();
        }
        return component;
    }

    /// <summary>
    /// 주어진 GameObject의 자식 중에서 특정 이름을 가진 T 타입의 컴포넌트를 찾습니다
    /// 직계 자식 게임 오브젝트들만 탐색하거나, 재귀적으로 모든 하위 자식까지 탐색할 수 있습니다.
    /// </summary>
    /// <typeparam name="T">찾으려는 컴포넌트의 타입</typeparam>
    /// <param name="target">탐색할 부모 GameObject</param>
    /// <param name="name">찾으려는 자식의 이름 (null일 경우 이름과 상관없이 찾음)</param>
    /// <param name="recursive">true일 경우 재귀적으로 모든 하위 자식까지 탐색, false일 경우 직계 자식들만 탐색</param>
    /// <returns></returns>
    //public static T FindChild<T>(GameObject target, string name = null, bool recursive = false) where T : Component
    //{
    //    if (recursive)
    //    {
    //        T[] childs = target.GetComponentsInChildren<T>();

    //        foreach(T child in childs)
    //        {
    //            if(child.gameObject.name == name)
    //            {
    //                return child;
    //            }

    //        }
    //    }
    //    else
    //    {

    //    }


    //}
}
