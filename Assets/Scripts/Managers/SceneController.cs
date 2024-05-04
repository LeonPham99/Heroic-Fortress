using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : SingleTon<SceneController>
{
    public void LoadMap(int mapID)
    {
        string sceneName = "Map " + mapID;
        SceneManager.LoadSceneAsync(sceneName);
        Time.timeScale = 1;
    }

    public void RestartScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
        SaveLoadManager.Instance.LoadSaveDropdown();
    }

    public void ReturnHomeScreen()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
