using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private float _spawnDelay = 2f;
    private List<Transform> _spawnPoints = new List<Transform>();
    private ObjectPool<Enemy> _enemyPool;
    private int _poolCapacity = 10;
    private int _poolMaxSize = 10;

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _spawnPoints.Add(transform.GetChild(i).transform);
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
            StartCoroutine(CreateEnemy());
        }
    }

    private IEnumerator CreateEnemy()
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
        enemy.transform.position = GetSpawnPosition();
        enemy.transform.eulerAngles = GetDirection();
        enemy.Release += ReleaseEnemy;
    }

    private void ReleaseEnemy(Enemy enemy)
    {
        _enemyPool.Release(enemy);
    }

    private void OnActionRelease(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
        enemy.Release -= ReleaseEnemy;
    }    

    private Vector3 GetSpawnPosition()
    {
        int selectedSpawnPoint;
        int minSpawnPointNumber = 0;
        int maxSpawnPointNumber = _spawnPoints.Count;

        selectedSpawnPoint = Random.Range(minSpawnPointNumber, maxSpawnPointNumber);

        return _spawnPoints[selectedSpawnPoint].position;
    }

    private Vector3 GetDirection()
    {
        int minRotationValue = 1;
        int maxRotationValue = 360;
        int rotationX = 0;
        int rotationY;
        int rotationZ = 0;

        rotationY = Random.Range(minRotationValue, maxRotationValue);

        return new Vector3(rotationX, rotationY, rotationZ);
    }
}
