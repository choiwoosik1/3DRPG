using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 일정 시간 간격으로 적 캐릭터를 스폰하는 역할
/// 최대 스폰 수, 스폰 반경, 생성된 적 캐릭터 리스트 관리
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    [Header("---- 동적으로 로드한 적 Prefab 변수(읽기 전용) ----")]
    [SerializeField] GameObject _enemyPrefab;

    [Header("---- 적 생성 ----")]
    [SerializeField] string _enemyPrefabPath;       // Enemy Prefab Asset이 저장되어 있는 경로
    [SerializeField] float _maxSpawnCount;
    [SerializeField] Hero _hero;

    [Header("---- 스폰 범위 ----")]
    [SerializeField] float _spawnRadius;

    [Header("---- 스폰 간격 ----")]
    [SerializeField] float _spawnSpan;

    [Header("---- 적 목록(읽기 전용) ----")]
    [SerializeField] List<Enemy> _enemies = new();  // 생성된 적 리스트

    Coroutine _spawnEnemyRoutine;                   // 적 생성 코루틴 변수

    float _enemyCount;                              // 현재 적 캐릭터 수

    private void Start()
    {
        _enemyPrefab = GameManager.Instance.ResourcesManager.LoadPrefab(_enemyPrefabPath);
        if (_enemyPrefab == null)
        {
            return;
        }

        _spawnEnemyRoutine = StartCoroutine(SpawnEnemyRoutine());
    }

    /// <summary>
    /// 적이 제거되었을 때 자동으로 호출되는 함수
    /// 해당 Enemy를 _enemies 리스트에서 제거
    /// </summary>
    /// <param name="enemy"></param>
    void OnEnemyRemoved(Enemy enemy)
    {

        _enemies.Remove(enemy);
    }

    /// <summary>
    /// 주기적으로 적을 생성하는 코루틴 함수
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnSpan);
            SpawnEnemy();
        }
    }

    /// <summary>
    /// 범위 내의 랜덤 위치(포지션)을 반환하는 함수
    /// </summary>
    /// <returns></returns>
    Vector3 GetRandomPos()
    {
        Vector3 pos = transform.position;
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        float randomRadius = Random.Range(0, _spawnRadius);
        pos.x += randomDir.x * randomRadius;
        pos.z += randomDir.y * randomRadius;
        return pos;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _spawnRadius);
    }

    void SpawnEnemy()
    {
        if (_enemies.Count >= _maxSpawnCount)
        {
            return;
        }

        // 1. Instantiate로 복제본 생성
        GameObject enemyGo = Instantiate(_enemyPrefab, transform);
        Enemy enemy = enemyGo.GetComponent<Enemy>();
        if (enemy == null) return;

        // 2. 복제본의 위치 설정
        enemy.transform.position = GetRandomPos();

        // 3. 복제본 초기화
        enemy.Initialize(_hero.transform);

        // 4. 복제본 리스트에 추가
        _enemies.Add(enemy);

        //  5. 생성된 복제본 이벤트 구독
        enemy.OnRemoved += OnEnemyRemoved;
    }

    //void Initialize()
    //{
    //    GameObject enemyGo = Resources.Load<GameObject>(_enemyPrefabPath);
    //    if (enemyGo == null)
    //    {
    //        Debug.LogError($"{_enemyPrefabPath} 경로가 존재하지 않습니다.");
    //        return;
    //    }
    //    _enemyPrefab = enemyGo.GetComponent<Enemy>();
    //    if (_enemyPrefab == null)
    //    {
    //        Debug.LogError($"{_enemyPrefabPath} 프리펩에 enemy 컴포넌트가 존재하지 않습니다.");
    //        return;
    //    }
    //}
}
