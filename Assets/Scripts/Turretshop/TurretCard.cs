using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;
using System;

public class TurretCard : MonoBehaviour
{
    public static Action<TurretSettings> OnPlaceTurret;

    [SerializeField] private Image turretImage;
    [SerializeField] private TextMeshProUGUI turretCost;

    public TurretSettings TurretLoaded { get; set; }

    public void SetupTurretButton(TurretSettings turretSettings)
    {
        TurretLoaded = turretSettings;
        turretImage.sprite = turretSettings.turretShopSprite;
        turretCost.text = turretSettings.turretShopCost.ToString();
    }

    public void PlaceTurret()
    {
        if (CurrencySystem.Instance.TotalCoins >= TurretLoaded.turretShopCost)
        {
            CurrencySystem.Instance.RemoveCoins(TurretLoaded.turretShopCost);
            UIManager.Instance.CloseTurretShopPanel();
            OnPlaceTurret?.Invoke(TurretLoaded);
        }
    }
}
