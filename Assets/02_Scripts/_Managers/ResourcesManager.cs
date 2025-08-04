using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// LoadResources라는 함수를 만들어

/// <summary>
/// Unity Resources를 활용하여 게임의 Resource를 관리하는 매니저
/// </summary>
public class ResourcesManager : MonoBehaviour
{
    Dictionary<string, GameObject> _prefabCache = new();

    // type -> 유니티에서 지원하는 모든 에셋에 대해 적용 가능하도록
    Dictionary<Type, Dictionary<string, UnityEngine.Object>> _resourceCache = new();

    /// <summary>
    /// 지정 경로의 prefab을 로드해 반환하는 함수
    /// 이미 로드되어 있으면 캐시에서 찾아 반환하고 캐시에 없으면 새로 로드
    /// </summary>
    /// <param name="path">폴더 안의 프리펩 경로</param>
    /// <returns></returns>
    public GameObject LoadPrefab(string path)
    {
        // 이미 캐시에 로드한 프리펩이 저장되어 있으면
        if(_prefabCache.ContainsKey(path) == true)
        {
            return _prefabCache[path];
        }

        GameObject prefab = Resources.Load<GameObject>(path);
        if(prefab == null )
        {
            Debug.LogError($"{path}경로 프리펩이 존재하지 않습니다.");
        }
        else
        {
            _prefabCache[path] = prefab;
        }
        
        return prefab;
    }

    /// <summary>
    /// Resource를 Load합니다. 이미 캐시에 있다면 캐시에서 반환합니다.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    public T LoadResource<T>(string path) where T : UnityEngine.Object
    {
        // 타임별 캐시 확인
        if(_resourceCache.TryGetValue(typeof(T), out var cache) == false)
        {
            cache = new Dictionary<string, UnityEngine.Object>();
            _resourceCache[typeof(T)] = cache;
        }

        // 경로(폴더 경로 포함한 이름)에 따른 캐시 확인
        if(cache.ContainsKey(path) == true)
        {
            return cache[path] as T;
        }

        // Resources 폴더에서 Resource 로드
        T resource = Resources.Load<T>(path);
        if(resource == null)
        {
            Debug.LogError($"{path}경로의 Resources 폴더에서 찾을 수 없습니다.");
        }
        else
        {
            cache[path] = resource;
        }
        return resource;
    }
}
