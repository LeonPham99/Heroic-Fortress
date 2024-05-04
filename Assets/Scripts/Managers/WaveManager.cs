using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : SingleTon<WaveManager>
{
    public int CurrentWaves { get; set; }

    private List<int> maxWavesPerMap = new List<int> { 60, 80, 110};

    private int currentMapIndex = 0;

    void Start()
    {
        CurrentWaves = 1;
    }

    private void WaveProgress()
    {
        CurrentWaves++;
        if (CurrentWaves > maxWavesPerMap[currentMapIndex])
        {
            Time.timeScale = 0;
            GameManager.Instance.GameWin();
            LevelManager.Instance.UnlockNextMap();
        }
    }

    private void WaveCompleted()
    {
        AchievementManager.Instance.AddProgress("Waves10", 1);
        AchievementManager.Instance.AddProgress("Waves20", 1);
        AchievementManager.Instance.AddProgress("Waves50", 1);
        AchievementManager.Instance.AddProgress("Waves70", 1);
        AchievementManager.Instance.AddProgress("Waves100", 1);
        WaveProgress();
    }

    private void SetCurrentMapIndex(int mapIndex)
    {
        if (mapIndex >= 0 && mapIndex < maxWavesPerMap.Count)
        {
            currentMapIndex = mapIndex;
            CurrentWaves = 1;
        }
    }

    private void OnEnable()
    {
        SpawnSystem.OnWaveCompleted += WaveCompleted;
    }

    private void OnDisable()
    {
        SpawnSystem.OnWaveCompleted -= WaveCompleted;
    }
}
