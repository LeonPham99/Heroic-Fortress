using UnityEngine;

[System.Serializable]
public class TurretData
{
    public int turretPrefabID;
    public float damage;
    public float delayPerShot;
    public int upgradeLevel;
    public int currentUpgradeCost;
    public float sellPercentage;
    public float attackRange;
    public Vector3 position;
    public Quaternion rotation;

    // Constructor to initialize data
    public TurretData(int turretPrefabID, float damage, float delayPerShot, int upgradeLevel, int currentUpgradeCost, float sellPercentage, float attackRange, Vector3 position, Quaternion rotation)
    {
        this.turretPrefabID = turretPrefabID;
        this.damage = damage;
        this.delayPerShot = delayPerShot;
        this.upgradeLevel = upgradeLevel;
        this.currentUpgradeCost = currentUpgradeCost;
        this.sellPercentage = sellPercentage;
        this.attackRange = attackRange;
        this.position = position;
        this.rotation = rotation;
    }
}