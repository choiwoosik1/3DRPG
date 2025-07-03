using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameObject Pool
/// 게임 오브젝트들을 미리 생성해 두었다가 필요할 때 건네주고
/// 사용이 끝나면 다시 돌려받는 클래스
/// </summary>
public class Pool
{
    Stack<GameObject> _pool;        // 게임 오브젝트 스택
    GameObject _prefab;             // Pooling 할 원본 Prefab
    Transform _parent;              // Pooling GameObject들의 부모 Transform
    

    /// <summary>
    /// Pool 생성자 
    /// </summary>
    /// <param name="prefab">Pooling할 Prefab</param>
    /// <param name="parent">Pool의 부모 Transform</param>
    /// <param name="initialSize">초기 Pool 크기</param>
    public Pool(GameObject prefab, Transform parent, int initialSize)
    {
        _prefab = prefab;
        _parent = parent;
        _pool = new Stack<GameObject>(initialSize);

        for(int i = 0; i < initialSize; i++)
        {
            CreatePoolObj();
        }
    }

    /// <summary>
    /// Pool에 새 GameObject를 추가하는 함수
    /// </summary>
    void CreatePoolObj()
    {
        // 원본 Prefab을 복제하여 새 게임오브젝트 생성
        GameObject go = Object.Instantiate(_prefab);

        // 새 게임오브젝트의 부모를 Pool의 부모로 설정
        go.transform.SetParent(_parent);

        // 새 게임 오브젝트 비활성화
        go.SetActive(false);

        // 새 게임 오브젝트에서 Poolable Component를 가져온다
        Poolable poolable = go.GetComponent<Poolable>();
        
        // Poolable Component가 없으면 새로 만든다.
        if(poolable == null)
        {
            poolable = go.AddComponent<Poolable>();
        }

        // 새 게임오브젝트를 스택에 추가
        _pool.Push(go);
    }

    /// <summary>
    /// Pool에서 게임 오브젝트를 가져오는 함수
    /// Pool이 비어있다면 새로 생성해 반환
    /// </summary>
    /// <returns></returns>
    public GameObject Pop()
    {
        // Pool에 남은 게임 오브젝트가 있는 경우
        if( _pool.Count > 0 )
        {
            GameObject go = _pool.Pop();
            go.SetActive(true);
            return go;
        }

        // Pool에 남은 게임 오브젝트가 없어서 새로 생성해 반환
        GameObject newGo = Object.Instantiate(_prefab);

        Poolable poolable = newGo.GetComponent<Poolable>();
        if(poolable == null)
        {
            poolable = newGo.AddComponent<Poolable>();
        }
        poolable.SetPool(this);

        return newGo;
    }

    /// <summary>
    /// 게임 오브젝트를 Pool로 반환하는 함수
    /// </summary>
    /// <param name="go"></param>
    public void Push(GameObject go)
    {
        go.transform.SetParent( _parent);
        go.SetActive(false);
        _pool.Push(go);
    }
}
