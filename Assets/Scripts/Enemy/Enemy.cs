using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Action<Enemy> OnEndReached;

    [SerializeField] private float moveSpeed;
    [SerializeField] private int coinValue;

    // Move speed of enemy
    public float MoveSpeed { get; set; }

    public int CoinValue { get; set; }

    // The waypoint reference
    public Waypoint Waypoint { get; set; }

    // The enemy health reference
    public EnemyHealth EnemyHealth { get; set; }

    // Returns the current Point Position where this enemy needs to go
    public Vector3 CurrentPosition => Waypoint.GetWayPointPosition(_currentWayPointIndex);

    private int _currentWayPointIndex;
    private Vector3 _lastPointPosition;

    private EnemyHealth _enemyHealth;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _enemyHealth = GetComponent<EnemyHealth>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        EnemyHealth = GetComponent<EnemyHealth>();

        _currentWayPointIndex = 0;
        MoveSpeed = moveSpeed;
        CoinValue = coinValue;
        _lastPointPosition = transform.position;
    }

    private void Update()
    {
        Move();
        Rotate();

        if (CurrentPointPositionReached())
        {
            UpdateCurrentPointIndex();
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position,
            CurrentPosition, MoveSpeed * Time.deltaTime);
    }

    public void StopMovement()
    {
        MoveSpeed = 0;
    }

    public void ResumeMovement()
    {
        MoveSpeed = moveSpeed;
    }

    public void Rotate()
    {
        if (CurrentPosition.x > _lastPointPosition.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private bool CurrentPointPositionReached()
    {
        float distanceToNextPointPosition = (transform.position - CurrentPosition).magnitude;
        if (distanceToNextPointPosition < 0.1f)
        {
            _lastPointPosition = transform.position;
            return true;
        }

        return false;
    }

    private void UpdateCurrentPointIndex()
    {
        int lastWayPointIndex = Waypoint.Points.Length - 1;
        if (_currentWayPointIndex < lastWayPointIndex)
        {
            _currentWayPointIndex++;
        }
        else
        {
            EndPointReached();
        }
    }

    private void EndPointReached()
    {
        OnEndReached?.Invoke(this);
        _enemyHealth.ResetHealth();
        ObjectPooling.ReturnToPooling(gameObject);
    }

    public void ResetEnemyWayPoint()
    {
        _currentWayPointIndex = 0;
    }
}
