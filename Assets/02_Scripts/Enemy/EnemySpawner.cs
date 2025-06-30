using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Enemy[] _enemyPrefabs;

    [Header("---- 스폰 범위")]
    [SerializeField] float _minRadius;
    [SerializeField] float _maxRadius;

    [Header("---- 스폰 간격 ----")]
    [SerializeField] float _spawnSpan;

    float _enemyCount;      // 현재 적 캐릭터 수

    void SpawnEnemy()
    {
        //Enemy enem = Instantiate(_enemyPrefabs, transform);
    }
}
