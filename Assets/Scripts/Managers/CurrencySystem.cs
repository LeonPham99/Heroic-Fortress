using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencySystem : SingleTon<CurrencySystem>
{
    [SerializeField] private int coinTest = 500;
    private string CURRENCY_SAVE_KEY = "MYGAME_CURRENCY";

    public static string GetCurrencySaveKey()
    {
        return Instance.CURRENCY_SAVE_KEY;
    }

    public int TotalCoins { get; set; }

    private void Start()
    {
        LoadCoins();
    }

    private void LoadCoins()
    {
        TotalCoins = PlayerPrefs.GetInt(CURRENCY_SAVE_KEY, coinTest);
    }

    public void AddCoins(int amount)
    {
        TotalCoins += amount;
        PlayerPrefs.SetInt(CURRENCY_SAVE_KEY, TotalCoins);
        PlayerPrefs.Save();
    }
    public void ResetCoinsToDefault()
    {
        PlayerPrefs.DeleteKey(CURRENCY_SAVE_KEY);
        LoadCoins();
    }

    public void RemoveCoins(int amount)
    {
        if (TotalCoins >= amount)
        {
            TotalCoins -= amount;
            PlayerPrefs.SetInt(CURRENCY_SAVE_KEY, TotalCoins);
            PlayerPrefs.Save();
            Debug.Log("Coins after removing: " + TotalCoins);
        }
    }

    private void AddCoins(Enemy _enemy)
    {
        AddCoins(_enemy.CoinValue);
    }

    private void OnEnable()
    {
        EnemyHealth.OnEnemyKilled += AddCoins;
    }

    private void OnDisable()
    {
        EnemyHealth.OnEnemyKilled -= AddCoins;
    }
}
