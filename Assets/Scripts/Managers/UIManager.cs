using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : SingleTon<UIManager>
{
    [Header("Panels")]
    [SerializeField] private GameObject turretShopPanel;
    [SerializeField] private GameObject nodeUIPanel;
    [SerializeField] private GameObject pauseGamePanel;
    [SerializeField] private GameObject achievementPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject soundPanel;
    [SerializeField] private GameObject savePanel;
    [SerializeField] private GameObject loadPanel;

    [Header("Dropdown")]
    [SerializeField] private TMP_Dropdown loadDropdown;
    public TMP_Dropdown LoadDropdown => loadDropdown;

    [Header("InputText")]
    [SerializeField] private TMP_InputField saveNameInputField;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI upgradeText;
    [SerializeField] private TextMeshProUGUI sellText;
    [SerializeField] private TextMeshProUGUI turretLevelText;
    [SerializeField] private TextMeshProUGUI lifesText;
    [SerializeField] private TextMeshProUGUI currentLifesText;
    [SerializeField] private TextMeshProUGUI currentCoinsText;
    [SerializeField] private TextMeshProUGUI totalCoinsText;
    [SerializeField] private TextMeshProUGUI currentWaveText;
    [SerializeField] private TextMeshProUGUI winTotalCoinsText;
    [SerializeField] private TextMeshProUGUI winTotalLifesText;
    [SerializeField] private TextMeshProUGUI gameOverTotalCoinsText;

    public bool isGamePaused { get; private set; }

    private Node _currentNodeSelected;

    private void OnEnable()
    {
        Node.OnNodeSelected += NodeSelected;
    }

    private void OnDisable()
    {
        Node.OnNodeSelected -= NodeSelected;
    }

    private void Update()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        totalCoinsText.text = CurrencySystem.Instance.TotalCoins.ToString();
        currentCoinsText.text = CurrencySystem.Instance.TotalCoins.ToString();
        lifesText.text = GameManager.Instance.TotalLives.ToString();
        currentLifesText.text = GameManager.Instance.TotalLives.ToString();
        currentWaveText.text = $"Wave {WaveManager.Instance.CurrentWaves}";
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void ResumeTime()
    {
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    public void SlowTime()
    {
        Time.timeScale = 0.5f;
    }

    public void FastTime()
    {
        Time.timeScale = 2f;
    }

    public void RestartGame()
    {
        ResumeTime();
        SceneController.Instance.RestartScene();
    }

    public void ReturnHomeScreen()
    {
        SceneController.Instance.ReturnHomeScreen();
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        gameOverTotalCoinsText.text = CurrencySystem.Instance.TotalCoins.ToString();
        CloseNodeUIPanel();
        CloseTurretShopPanel();
        PauseGame();
    }

    public void ShowWinPanel()
    {
        winPanel.SetActive(true);
        winTotalCoinsText.text = CurrencySystem.Instance.TotalCoins.ToString();
        winTotalLifesText.text = GameManager.Instance.TotalLives.ToString();
        CloseNodeUIPanel();
        CloseTurretShopPanel();
        PauseGame();
    }

    public void OpenSavePanel(bool status)
    {
        savePanel.SetActive(status);
        if (!status)
        {
            ResumeTime();
            return;
        }
        else
        {
            PauseGame();
            CloseAllShopPanels();
        }
    }

    public void OpenLoadPanel(bool status)
    {
        loadPanel.SetActive(status);
        if (!status)
        {
            ResumeTime();
            return;
        }
        else
        {
            PauseGame();
            CloseAllShopPanels();
        }
    }

    public void OpenSoundPanel(bool status)
    {
        soundPanel.SetActive(status);
        if (!status)
        {
            ResumeTime();
            return;
        }
        else
        {
            PauseGame();
            CloseAllShopPanels();
        }
    }

    public void OpenAchievementPanel(bool status)
    {
        achievementPanel.SetActive(status);
        CloseNodeUIPanel();
        CloseTurretShopPanel();
    }

    public void OpenPauseGamePanel(bool status)
    {
        pauseGamePanel.SetActive(status);

        if (!status)
        {
            ResumeTime();
            return;
        }
        else
        {
            PauseGame();
            CloseAllShopPanels();
        }
    }

    public void CloseTurretShopPanel()
    {
        turretShopPanel.SetActive(false);
    }

    public void CloseNodeUIPanel()
    {
        if (_currentNodeSelected != null)
        _currentNodeSelected.CloseAttackRangeSprite();
        nodeUIPanel.SetActive(false);
    }

    private void CloseAchievementPanel()
    {
        achievementPanel.SetActive(false);
    }

    private void CloseAllShopPanels()
    {
        CloseTurretShopPanel();
        CloseNodeUIPanel();
        CloseAchievementPanel();
    }

    public void OnSaveButtonClick()
    {
        PauseGame();
        if (!string.IsNullOrWhiteSpace(saveNameInputField.text))
        {
            SaveLoadManager.Instance.SaveGame(saveNameInputField.text);
            savePanel.SetActive(false);
            ResumeTime();
        }
    }

    public void OnLoadButtonClick()
    {
        PauseGame();
        if (loadDropdown.options.Count > 0)
        {
            string saveName = loadDropdown.options[loadDropdown.value].text;

            GameData data = SaveLoadManager.Instance.LoadGame(saveName);
            if (data != null)
            {
                SaveLoadManager.Instance.ApplyGameData(data);
            }
            else
            {
                Debug.LogError("Failed to load game data.");
            }
            ResumeTime();
        }
    }
    public void OnDeleteButtonClick()
    {
        var dropdown = FindObjectOfType<TMP_Dropdown>();
        if (dropdown.options.Count > 0 && dropdown.value < dropdown.options.Count)
        {
            var saveName = dropdown.options[dropdown.value].text;
            SaveLoadManager.Instance.DeleteGame(saveName);
        }
    }

    private void NodeSelected(Node nodeSelected)
    {
        if (isGamePaused) return;

        CloseAllShopPanels();
        _currentNodeSelected = nodeSelected;
        if (_currentNodeSelected.isEmpty())
            turretShopPanel.SetActive(true);
        else
            ShowNodeUI();
    }

    private void ShowNodeUI()
    {
        nodeUIPanel.SetActive(true);
        UpdateUpgradeText();
        UpdateTurretLevel();
        UpdateSellValue();
    }

    private void UpdateUpgradeText()
    {
        upgradeText.text = _currentNodeSelected.Turret.TurretUpgrade.UpgradeCost.ToString();
    }

    private void UpdateTurretLevel()
    {
        turretLevelText.text = $"Level {_currentNodeSelected.Turret.TurretUpgrade.Level}";
    }

    private void UpdateSellValue()
    {
        int sellAmount = _currentNodeSelected.Turret.TurretUpgrade.GetSellValue();
        sellText.text = sellAmount.ToString();
    }

    public void UpgradeTurret()
    {
        _currentNodeSelected.Turret.TurretUpgrade.UpgradeTurret();
        UpdateUpgradeText();
        UpdateTurretLevel();
        UpdateSellValue();
    }

    public void SellTurret()
    {
        _currentNodeSelected.SellTurret();
        _currentNodeSelected = null;
        nodeUIPanel.SetActive(false);
    }
    public void UpdateUI()
    {
        totalCoinsText.text = CurrencySystem.Instance.TotalCoins.ToString();
        currentLifesText.text = GameManager.Instance.TotalLives.ToString();
        currentWaveText.text = $"Wave {WaveManager.Instance.CurrentWaves}";
    }
}
