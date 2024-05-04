using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public TurretData data;

    private TurretUpgrade turretUpgrade;
    private Turret turret;
    private TurretProjectile turretProjectile;

    private void Awake()
    {
        turretUpgrade = GetComponent<TurretUpgrade>();
        turret = GetComponent<Turret>();
        turretProjectile = GetComponent<TurretProjectile>();

        UpdateTurretData();
    }

    private void Update()
    {
        UpdateTurretData();
    }

    public void SetData(TurretData newData)
    {
        data = newData;
        ApplyData();
    }

    private void ApplyData()
    {
        if (turretProjectile.Damage != data.damage)
            turretProjectile.Damage = data.damage;

        if (turretProjectile.DelayPerShot != data.delayPerShot)
            turretProjectile.DelayPerShot = data.delayPerShot;

        if (turretUpgrade.Level != data.upgradeLevel)
            turretUpgrade.Level = data.upgradeLevel;

        if (turretUpgrade.UpgradeCost != data.currentUpgradeCost)
            turretUpgrade.UpgradeCost = data.currentUpgradeCost;

        if (turretUpgrade.SellPercentage != data.sellPercentage)
            turretUpgrade.SellPercentage = data.sellPercentage;

        transform.position = data.position;
        transform.rotation = data.rotation;
    }

    private void UpdateTurretData()
    {
        if (turretUpgrade != null && turret != null && turretProjectile != null)
        {
            data = new TurretData(
                turret.TurretPrefabID,
                turretProjectile.Damage,
                turretProjectile.DelayPerShot,
                turretUpgrade.Level,
                turretUpgrade.UpgradeCost,
                turretUpgrade.SellPercentage,
                turret.AttackRange,
                transform.position,
                transform.rotation
            );
        }
    }
}
