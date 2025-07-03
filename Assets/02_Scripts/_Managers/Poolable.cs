using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

/// <summary>
/// Pooling된 게임 오브젝트를 관리하는 클래스
/// Pool에서 가져온 게임 오브젝트를 Pool로 반환하는 기능
/// </summary>
public class Poolable : MonoBehaviour
{
    /// <summary>
    /// 자신 게임오브젝트가 생성된 Pool
    /// </summary>
    Pool _pool;

    /// <summary>
    /// Pool을 설정하는 함수
    /// </summary>
    /// <param name="pool"></param>
    public void SetPool(Pool pool)
    {
        _pool = pool;
    }

    /// <summary>
    /// Pool로 되돌리는 함수
    /// </summary>
    public void ReturnToPool()
    {
        if(_pool != null)
        {
            _pool.Push(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
}
