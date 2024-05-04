using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public static Action<Enemy> OnEnemyKilled;
    public static Action<Enemy> OnEnemyHit;

    [SerializeField] private GameObject healthBarPrefab;
    [SerializeField] private Transform barPosition;

    [SerializeField] private float initialHealth = 10f;
    [SerializeField] private float maxHealth = 10f;

    public float CurrentHealth { get; set; }
    public bool IsAlive { get; private set; } = true;

    private Image _healthBar;
    private Enemy _enemy;

    void Start()
    {
        CreateHealthBar();
        CurrentHealth = initialHealth;

        _enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        _healthBar.fillAmount = Mathf.Lerp(_healthBar.fillAmount,
            CurrentHealth / maxHealth, Time.deltaTime * 10f);
    }

    private void CreateHealthBar()
    {
        GameObject newBar = Instantiate(healthBarPrefab, barPosition.position, Quaternion.identity);
        newBar.transform.SetParent(transform);

        EnemyHealthContainer container = newBar.GetComponent<EnemyHealthContainer>();
        _healthBar = container.FillAmountImage;
    }

    public void DealDamage(float damageReceived)
    {
        if (!IsAlive) return;

        CurrentHealth -= damageReceived;

        if (CurrentHealth <= 0)
        {
            IsAlive = false;
            CurrentHealth = 0;
            Die();
        }
        else
        {
            OnEnemyHit?.Invoke(_enemy);
        }
    }

    public void ResetHealth()
    {
        CurrentHealth = initialHealth;
        IsAlive = true;
        _healthBar.fillAmount = 1f;
    }

    private void Die()
    {
        AchievementManager.Instance.AddProgress("Killed20", 1);
        AchievementManager.Instance.AddProgress("Killed50", 1);
        AchievementManager.Instance.AddProgress("Killed100", 1);
        AchievementManager.Instance.AddProgress("Killed200", 1);
        AchievementManager.Instance.AddProgress("Killed300", 1);
        AchievementManager.Instance.AddProgress("Killed500", 1);
        if (!IsAlive) OnEnemyKilled?.Invoke(_enemy);
    }
}
