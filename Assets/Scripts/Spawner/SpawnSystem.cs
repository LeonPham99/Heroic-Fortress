using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnModes
{
    Fixed,
    Random
}

public class SpawnSystem : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private SpawnModes spawnModes = SpawnModes.Fixed;
    [SerializeField] private int enemyCount = 10;
    [SerializeField] private float timeDelayBetweenWaves;

    [Header("Fixed Delay")]
    [SerializeField] private float timeDelayBetweenSpawn;

    [Header("Random Delay")]
    [SerializeField] private float minRandomDelay;
    [SerializeField] private float maxRandomDelay;

    private float _spawnTimer;
    private int _enemiesSpawned;
    private int _enemiesRemaining;

    private ObjectPooling _pooling;
    private Waypoint _waypoint;

    void Start()
    {
        _pooling = GetComponent<ObjectPooling>();
        _waypoint = GetComponent<Waypoint>();

        _enemiesRemaining = enemyCount;
    }


    void Update()
    {
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer < 0)
        {
            _spawnTimer = GetSpawnDelay();
            if (_enemiesSpawned < enemyCount)
            {
                _enemiesSpawned++;
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy()
    {
        GameObject newInstance = _pooling.GetInstanceFromPool();
        Enemy enemy = newInstance.GetComponent<Enemy>();
        enemy.Waypoint = _waypoint;
        enemy.ResetEnemyWayPoint();
        enemy.transform.localPosition = transform.position;

        newInstance.SetActive(true);
    }

    private float GetSpawnDelay()
    {
        float delay = 0f;
        if (spawnModes == SpawnModes.Fixed)
        {
            delay = timeDelayBetweenSpawn;
            Debug.Log($"Time Delay: {timeDelayBetweenSpawn}");
        } 
        else
        {
            delay = GetRandomDelay();
        }

        return delay;
    }

    private float GetRandomDelay()
    {
        float randomTimer = Random.Range(minRandomDelay, maxRandomDelay);
        Debug.Log($"Timer: {randomTimer}");
        return randomTimer;
    }

    private IEnumerator NextWave()
    {
        yield return new WaitForSeconds(timeDelayBetweenWaves);
        _enemiesRemaining = enemyCount;
        _spawnTimer = 0f;
        _enemiesSpawned = 0;
    }

    private void RecordEnemyEnd(Enemy enemy)
    {
        _enemiesRemaining--;
        if (_enemiesRemaining <= 0 )
        {
            StartCoroutine(NextWave());
        }
    }

    private void OnEnable()
    {
        Enemy.OnEndReached += RecordEnemyEnd;
        EnemyHealth.OnEnemyKilled += RecordEnemyEnd;
    }

    private void OnDisable()
    {
        Enemy.OnEndReached -= RecordEnemyEnd;
        EnemyHealth.OnEnemyKilled -= RecordEnemyEnd;
    }
}
