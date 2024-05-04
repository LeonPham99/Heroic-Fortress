using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret Shop Setting")]
public class TurretSettings : ScriptableObject
{
    public GameObject turretPrefab;
    public int turretShopCost;
    public Sprite turretShopSprite;
}
