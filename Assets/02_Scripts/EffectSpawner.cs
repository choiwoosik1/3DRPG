using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpawner : MonoBehaviour
{
    [SerializeField] GameObject _hitEffectPrefab;

    [Header("---- 이펙트 생성 ----")]
    [SerializeField] string _effectPrefabPath;

    PoolManager _poolManager;

    private void Start()
    {
        _poolManager = GameManager.Instance.PoolManager;
    }

    public void SpawnEffect(Vector3 position)
    {
        GameObject hitEffectGo = _poolManager.GetFromPool(_effectPrefabPath);
        if (hitEffectGo == null) return;

        _hitEffectPrefab.transform.position = position;

    }
}
