using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyFX : MonoBehaviour
{
    [SerializeField] private Transform textDamageSpawnPosition;
    [SerializeField] private AudioClip enemyHit;

    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void EnemyHit(Enemy enemy, float damage)
    {
        if (_enemy == enemy)
        {
            AudioManager.Instance.PlayEffect(enemyHit);
            GameObject newInstance = DamageTextManager.Instance.pooling.GetInstanceFromPool();
            TextMeshProUGUI damageText = newInstance.GetComponent<DamageText>().DmgText;
            damageText.text = damage.ToString();

            newInstance.transform.SetParent(textDamageSpawnPosition, false);
            newInstance.transform.position = textDamageSpawnPosition.position;
            newInstance.SetActive(true);

            if (_enemy.transform.rotation.y == 180)
            {
                newInstance.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                newInstance.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    private void OnEnable()
    {
        Projectile.OnEnemyHit += EnemyHit;
    }

    private void OnDisable()
    {
        Projectile.OnEnemyHit -= EnemyHit;
    }
}
