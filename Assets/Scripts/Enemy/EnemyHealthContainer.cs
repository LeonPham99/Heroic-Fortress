using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthContainer : MonoBehaviour
{
    [SerializeField] private Image fillAmountImage;
    public Image FillAmountImage => fillAmountImage;

    [SerializeField] private RectTransform canvasRectTransform;

    public RectTransform CanvasRectTransform => canvasRectTransform;
}
