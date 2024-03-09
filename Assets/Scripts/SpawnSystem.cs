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
    [SerializeField] private GameObject TestObject;

    [Header("Fixed Delay")]
    [SerializeField] private float timeDelayBetweenSpawn;

    [Header("Random Delay")]
    [SerializeField] private float minRandomDelay;
    [SerializeField] private float maxRandomDelay;

    private float _spawnTimer;
    private int _enemiesSpawned;
    private ObjectPooling _pooling;

    void Start()
    {
        _pooling = GetComponent<ObjectPooling>();
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
}
