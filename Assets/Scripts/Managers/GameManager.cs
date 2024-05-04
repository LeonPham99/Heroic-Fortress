using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingleTon<GameManager>
{
    [SerializeField] private int initialLives = 10;
    public int TotalLives { get; private set; }

    // Define events for game state changes
    public event Action<int> OnLivesChanged;
    public event Action OnGameLost;
    public event Action OnGameWon;

    private new void Awake()
    {
        SaveLoadManager.Instance.LoadSaveDropdown();
        SaveLoadManager.Instance.LoadDefaultGame();
    }

    private void Start()
    {
        TotalLives = initialLives;
    }

    public void SetTotalLives(int newLives)
    {
        TotalLives = newLives;
        if (TotalLives <= 0)
        {
            TotalLives = 0;
            GameOver();
        }
    }
    private void OnEnable()
    {
        Enemy.OnEndReached += ReduceLives;
    }

    private void OnDisable()
    {
        Enemy.OnEndReached -= ReduceLives;
    }

    private void ReduceLives(Enemy enemy)
    {
        TotalLives--;
        OnLivesChanged?.Invoke(TotalLives);

        if (TotalLives <= 0)
        {
            TotalLives = 0;
            GameOver();
        }
    }

    public void GameOver()
    {
        PauseGame();
        UIManager.Instance.ShowGameOverPanel();
        OnGameLost?.Invoke();
    }

    public void GameWin()
    {
        PauseGame();
        UIManager.Instance.ShowWinPanel();
        OnGameWon?.Invoke();
    }

    // Centralized methods to handle game pause and resume
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
