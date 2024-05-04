using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public static Action<Node> OnNodeSelected;
    public static Action OnTurretSold;
    public static Node CurrentSelectedNode;

    [SerializeField] private GameObject attackRangeSprite;

    private float _rangeSize;
    private Vector3 _rangeOriginalSize;

    public Turret Turret { get; set; }

    private void Start()
    {
        InitializeRange();
    }

    private void OnDestroy()
    {
        if (CurrentSelectedNode == this)
        {
            CurrentSelectedNode = null;
        }
    }

    public void SetTurret(Turret turret)
    {
        Turret = turret;
    }

    public bool isEmpty()
    {
        return Turret == null;
    }

    public void SelectTurret()
    {
        if (UIManager.Instance.isGamePaused)
        {
            return;
        }

        if (CurrentSelectedNode != null && CurrentSelectedNode != this)
        {
            CurrentSelectedNode.CloseAttackRangeSprite();
            UIManager.Instance.CloseNodeUIPanel();
        }

        OnNodeSelected?.Invoke(this);
        CurrentSelectedNode = this;

        if (!isEmpty())
        {
            ShowTurretInfor();
        }
    }

    public void SellTurret()
    {
        if (!isEmpty())
        {
            CurrencySystem.Instance.AddCoins(Turret.TurretUpgrade.GetSellValue());
            Destroy(Turret.gameObject);
            Turret = null;
            attackRangeSprite.SetActive(false);
            OnTurretSold?.Invoke();
        }
    }

    public void CloseAttackRangeSprite()
    {
        attackRangeSprite.SetActive(false);
    }

    private void ShowTurretInfor()
    {
        attackRangeSprite.SetActive(true);
        attackRangeSprite.transform.localScale = _rangeOriginalSize * Turret.AttackRange / (_rangeSize / 2);
    }

    public void InitializeRange()
    {
        _rangeSize = attackRangeSprite.GetComponent<SpriteRenderer>().bounds.size.y;
        _rangeOriginalSize = attackRangeSprite.transform.localScale;
    }
}
