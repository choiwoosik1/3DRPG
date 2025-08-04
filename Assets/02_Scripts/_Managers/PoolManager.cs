using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 오브젝트 풀들을 관리하는 클래스
/// </summary>
public class PoolManager : MonoBehaviour
{
    const string _prefabPathForamt = "Prefabs/{0}";

    ResourcesManager _resourcesManager;

    Dictionary<string, Pool> _poolMap = new();

    public void Initialize(ResourcesManager resourcesManager)
    {
        _resourcesManager = resourcesManager;
    }

    /// <summary>
    /// Prefab 경로에 해당하는 Pool을 반환하는 함수
    /// 아직 생성되지 않은 Pool이면 새로 생성해 반환
    /// </summary>
    /// <param name="prefabPath"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public Pool GetPool(string prefabPath, int size = 10)
    {
        // Prefab 경로에 해당하는 Pool이 없으면
        if (_poolMap.ContainsKey(prefabPath) == false)
        {
            // ResourcesManager에서 Prefab Resources를 로드
            GameObject prefab = _resourcesManager.LoadResource<GameObject>(
                string.Format(_prefabPathForamt, prefabPath));

            // Prefab Load에 실패한 경우
            if(prefab == null)
            {
                return null;
            }

            Transform parent = new GameObject($"Pool_{prefabPath}").transform;
            DontDestroyOnLoad(parent.gameObject);
            Pool pool = new Pool(prefab, parent, size);
            _poolMap[prefabPath] = pool;
        }

        return _poolMap[prefabPath];
    }

    /// <summary>
    /// Pool에서 게임 오브젝트를 가져오는 함수
    /// </summary>
    /// <param name="prefabPath"></param>
    /// <returns></returns>
    public GameObject GetFromPool(string prefabPath)
    {
        Pool pool = GetPool(prefabPath);
        if (pool == null)
        {
            return null;
        }

        return pool.Pop();
    }
}
