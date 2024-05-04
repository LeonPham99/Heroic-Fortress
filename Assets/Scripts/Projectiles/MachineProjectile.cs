using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineProjectile : Projectile
{
    public Vector2 Direction { get; set; }

    protected override void Update()
    {
        MoveProjectile();
    }

    protected override void MoveProjectile()
    {
        Vector2 movement = Direction.normalized * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy.EnemyHealth.CurrentHealth > 0f)
            {
                OnEnemyHit?.Invoke(enemy, Damage);
                enemy.EnemyHealth.DealDamage(Damage);
            }

            ObjectPooling.ReturnToPooling(gameObject);
        }
    }

    private void OnEnable()
    {
        StartCoroutine(ObjectPooling.ReturnToPoolWithDelay(gameObject, 5f));
    }
}
