using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public enum SpawnModes
{
    Fixed,
    Random
}

public class SpawnSystem : MonoBehaviour
{
    public static Action OnWaveCompleted;

    [Header("Settings")]
    [SerializeField] private SpawnModes spawnModes = SpawnModes.Fixed;
    [SerializeField] private int enemyCount = 10;
    [SerializeField] private float timeDelayBetweenWaves;

    [Header("Fixed Delay")]
    [SerializeField] private float timeDelayBetweenSpawn;

    [Header("Random Delay")]
    [SerializeField] private float minRandomDelay;
    [SerializeField] private float maxRandomDelay;

    [Header("Poolers")]
    [SerializeField] private ObjectPooling enemyWave10Pooling;
    [SerializeField] private ObjectPooling enemyWave11To20Pooling;
    [SerializeField] private ObjectPooling enemyWave21To30Pooling;
    [SerializeField] private ObjectPooling enemyWave31To40Pooling;
    [SerializeField] private ObjectPooling enemyWave41To50Pooling;
    [SerializeField] private ObjectPooling enemyWave51To60Pooling;
    [SerializeField] private ObjectPooling enemyWave61To70Pooling;
    [SerializeField] private ObjectPooling enemyWave71To80Pooling;
    [SerializeField] private ObjectPooling enemyWave81To90Pooling;
    [SerializeField] private ObjectPooling enemyWave91To100Pooling;
    [SerializeField] private ObjectPooling enemyWave101To110Pooling;

    private float _spawnTimer;
    private int _enemiesSpawned;
    private int _enemiesRemaining;

    private Waypoint _waypoint;

    void Start()
    {
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
        GameObject newInstance = GetPooling().GetInstanceFromPool();
        if (newInstance == null)
        {
            return;
        }
        Enemy enemy = newInstance.GetComponent<Enemy>();
        enemy.Waypoint = _waypoint;
        enemy.ResetEnemyWayPoint();
        enemy.transform.localPosition = transform.position;

        newInstance.SetActive(true);
    }

    private void StartNextWave()
    {
        EnemyAnimations.OnEnemyDiedAnimationComplete -= StartNextWave;
        OnWaveCompleted?.Invoke();
        StartCoroutine(NextWave());
    }

    private float GetSpawnDelay()
    {
        float delay = 0f;
        if (spawnModes == SpawnModes.Fixed)
        {
            delay = timeDelayBetweenSpawn;
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
        return randomTimer;
    }

    private ObjectPooling GetPooling()
    {
        int currentWave = WaveManager.Instance.CurrentWaves;
        if (currentWave <= 10) return enemyWave10Pooling;
        if (currentWave <= 20) return enemyWave11To20Pooling;
        if (currentWave <= 30) return enemyWave21To30Pooling;
        if (currentWave <= 40) return enemyWave31To40Pooling;
        if (currentWave <= 50) return enemyWave41To50Pooling;
        if (currentWave <= 60) return enemyWave51To60Pooling;
        if (currentWave <= 70) return enemyWave61To70Pooling;
        if (currentWave <= 80) return enemyWave71To80Pooling;
        if (currentWave <= 90) return enemyWave81To90Pooling;
        if (currentWave <= 100) return enemyWave91To100Pooling;
        if (currentWave <= 110) return enemyWave101To110Pooling;
        return null;
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

        if (_enemiesRemaining <= 0)
        {
            if (enemy.EnemyHealth.IsAlive)
            {
                StartNextWave();
            }
            else
            {
                EnemyAnimations.OnEnemyDiedAnimationComplete += StartNextWave;
            }
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
