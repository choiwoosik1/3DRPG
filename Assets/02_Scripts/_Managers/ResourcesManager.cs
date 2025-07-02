using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unity Resources를 활용하여 게임의 Resource를 관리하는 매니저
/// </summary>
public class ResourcesManager : MonoBehaviour
{
    Dictionary<string, GameObject> _prefabCache = new();

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
            Debug.LogError($"{prefab}경로 프리펩이 존재하지 않습니다.");
        }
        else
        {
            _prefabCache[path] = prefab;
        }
        
        return prefab;
    }
}
