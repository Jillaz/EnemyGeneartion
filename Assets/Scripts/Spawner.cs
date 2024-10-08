using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private float _spawnDelay = 2f;
    [SerializeField] private Transform _selectedTarget;
    private List<Transform> _spawnPoints = new List<Transform>();
    private ObjectPool<Enemy> _enemyPool;
    private int _poolCapacity = 10;
    private int _poolMaxSize = 10;

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _spawnPoints.Add(transform.GetChild(i));
        }

        _enemyPool = new ObjectPool<Enemy>(
            createFunc: () => Instantiate(_enemyPrefab),
            actionOnGet: (enemy) => OnActionGet(enemy),
            actionOnRelease: (enemy) => OnActionRelease(enemy),
            actionOnDestroy: (enemy) => Destroy(enemy.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
            );
    }

    private void Start()
    {
        if (_spawnPoints.Count != 0)
        {
            StartCoroutine(CreateEnemies());
        }
    }

    private IEnumerator CreateEnemies()
    {
        var spawnDelay = new WaitForSeconds(_spawnDelay);

        while (true)
        {
            _enemyPool.Get();

            yield return spawnDelay;
        }
    }

    private void OnActionGet(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
        enemy.transform.position = GetSpawnPoint();
        enemy.Init(_selectedTarget);
        enemy.Released += ReleaseEnemy;
    }

    private void ReleaseEnemy(Enemy enemy)
    {
        _enemyPool.Release(enemy);
    }

    private void OnActionRelease(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
        enemy.Released -= ReleaseEnemy;
    }

    private Vector3 GetSpawnPoint()
    {
        int selectedSpawnPoint;

        int minSpawnPointNumber = 0;
        int maxSpawnPointNumber = _spawnPoints.Count;
        selectedSpawnPoint = Random.Range(minSpawnPointNumber, maxSpawnPointNumber);

        return _spawnPoints[selectedSpawnPoint].position;
    }
}
