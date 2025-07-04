using System.Collections;
using System.Collections.Generic;
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
        if(poolable != null)
        {
            poolable.ReturnToPool();
        }
        else
        {
            Object.Destroy(go);
        }
    }
}
