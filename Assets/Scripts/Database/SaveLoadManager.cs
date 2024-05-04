using System.IO;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.Linq;

public class SaveLoadManager : SingleTon<SaveLoadManager>
{
    public List<Turret> turrets = new List<Turret>();
    public List<Node> nodes = new List<Node>();

    public void AddNode(Node node)
    {
        nodes.Add(node);
    }

    public void AddTurret(Turret turret)
    {
        turrets.Add(turret);
    }

    public void RemoveTurret(Turret turret)
    {
        turrets.Remove(turret);
    }

    private void InstantiateTurrets(List<TurretData> turretsData)
    {
        foreach (TurretData turretData in turretsData)
        {
            GameObject turretPrefab = Resources.Load<GameObject>("Prefabs/" + turretData.turretPrefabID.ToString());
            if (turretPrefab != null)
            {
                GameObject turretInstance = Instantiate(turretPrefab, turretData.position, turretData.rotation);
                TurretController turretController = turretInstance.GetComponent<TurretController>();
                if (turretController != null)
                {
                    turretController.SetData(turretData);
                }
            }
        }
    }

    public void SaveGame(string saveName)
    {
        List<TurretData> turretsData = new List<TurretData>();
        foreach (TurretController controller in FindObjectsOfType<TurretController>())
        {
            turretsData.Add(controller.data);
        }

        GameData data = new GameData
        {
            coins = CurrencySystem.Instance.TotalCoins,
            lives = GameManager.Instance.TotalLives,
            currentWave = WaveManager.Instance.CurrentWaves,
            mapName = SceneManager.GetActiveScene().name,
            turretsData = turretsData
        };

        string json = JsonUtility.ToJson(data);
        Debug.Log("JSON Data: " + json);
        File.WriteAllText(Application.persistentDataPath + "/" + saveName + ".json", json);
        LoadSaveDropdown();
    }

    public void LoadDefaultGame()
    {
        string saveName = "LastPlayed";
        GameData data = LoadGame(saveName);
        if (data != null)
        {
            ApplyGameData(data);
        }
    }

    public GameData LoadGame(string saveName)
    {
        string path = Application.persistentDataPath + "/" + saveName + ".json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            GameData loadedData = JsonUtility.FromJson<GameData>(json);
            if (loadedData != null)
                return loadedData;
            else
                Debug.LogError("Failed to parse game data.");
        }
        else
        {
            Debug.LogError("Save file not found: " + saveName);
        }
        return null;
    }

    public void ApplyGameData(GameData data)
    {
        CurrencySystem.Instance.TotalCoins = data.coins;
        PlayerPrefs.SetInt(CurrencySystem.GetCurrencySaveKey(), data.coins);
        PlayerPrefs.Save();

        GameManager.Instance.SetTotalLives(data.lives);
        WaveManager.Instance.CurrentWaves = data.currentWave;

        InstantiateTurrets(data.turretsData);
        SceneManager.LoadScene(data.mapName);
    }

    public void DeleteGame(string saveName)
    {
        string path = Application.persistentDataPath + "/" + saveName + ".json";
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Deleted save file: " + saveName);
            LoadSaveDropdown();
        }
        else
        {
            Debug.LogError("Save file not found: " + saveName);
        }
    }

    public void LoadSaveDropdown()
    {
        List<string> saveFiles = new List<string>();
        var info = new DirectoryInfo(Application.persistentDataPath);
        var fileInfo = info.GetFiles("*.json");
        foreach (var file in fileInfo)
        {
            saveFiles.Add(Path.GetFileNameWithoutExtension(file.Name));
        }
        UIManager.Instance.LoadDropdown.ClearOptions();
        UIManager.Instance.LoadDropdown.AddOptions(saveFiles);
    }
}

[System.Serializable]
public class GameData
{
    public int coins;
    public int lives;
    public int currentWave;
    public string mapName;
    public List<TurretData> turretsData;
}
