using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameShop : MonoBehaviour
{
    public static GameShop instance;

    [Header("Opening Animation")]
    [SerializeField] private float startY;
    private RectTransform rectTransform;

    [Header("Other")]
    [SerializeField] private GameShopItems itemsLog;

    #region Unity Functions
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startY = rectTransform.anchoredPosition.y;
    }
    #endregion

    #region Main Shop Functions
    public void PurchaseItem(int _itemIndex)
    {

    }

    public void ToggleShopLocation()
    {
        if (rectTransform.anchoredPosition.y == startY) rectTransform.DOAnchorPosY(0, 1f).SetEase(Ease.OutSine);
        else rectTransform.DOAnchorPosY(startY, 1f).SetEase(Ease.InSine);
    }
    #endregion
}