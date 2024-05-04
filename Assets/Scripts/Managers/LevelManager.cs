using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelManager : SingleTon<LevelManager>
{
    private int maxLevel = 3;

    private new void Awake()
    {
        InitializePlayerPrefs();
    }

    private void InitializePlayerPrefs()
    {
        if (PlayerPrefs.HasKey("UnlockedLevel"))
        {
            return;
        }
        else
        {
            PlayerPrefs.SetInt("UnlockedLevel", 1);
            PlayerPrefs.Save();
        }
    }

    public void UnlockNextMap()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        if (unlockedLevel <= maxLevel)
        {
            unlockedLevel++;
            PlayerPrefs.SetInt("UnlockedLevel", unlockedLevel);
            PlayerPrefs.Save();
        }
    }

    public void LoadLevel(int level)
    {
        if (level <= PlayerPrefs.GetInt("UnlockedLevel"))
        {
            string sceneName = "Map " + level;
            SceneManager.LoadSceneAsync(sceneName);
        }
    }

    public void ResetUnlockedMap()
    {
        PlayerPrefs.SetInt("UnlockedLevel", 1);
        PlayerPrefs.Save();
    }
}
