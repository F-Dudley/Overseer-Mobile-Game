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
    [SerializeField] private bool shopOpen;
    private RectTransform rectTransform;

    [Header("Shop UI")]
    [SerializeField] private RectTransform itemsContainer;
    [SerializeField] private GameObject shopItemUIPrefab;

    [Header("Other")]
    [SerializeField] private GameItems itemsLog;

    #region Unity Functions
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startY = rectTransform.anchoredPosition.y;

        DrawShopItems();
    }
    #endregion

    #region Main Shop Functions
    public void PurchaseItem(int _itemIndex)
    {
        if (GameplayManager.instance.ResourcesAmount - itemsLog.items[_itemIndex].price >= 0)
        {
            GameplayManager.instance.ResourcesAmount -= itemsLog.items[_itemIndex].price;
            GameInventory.instance.Add(_itemIndex);
        }
    }

    public void ToggleShopLocation()
    {
        if (!shopOpen) rectTransform.DOAnchorPosY(0, 1f).SetEase(Ease.OutSine).OnComplete(() => shopOpen = true);
        else rectTransform.DOAnchorPosY(startY, 1f).SetEase(Ease.InSine).OnComplete(() => shopOpen = false);
    }
    #endregion

    #region Shop Drawing
    private void DrawShopItems()
    {
        for (int i = 0; i < itemsLog.items.Count; i++)
        {
            GameObject itemUI = Instantiate(shopItemUIPrefab, Vector3.zero, Quaternion.identity, itemsContainer);
            itemUI.GetComponent<ShopItemElement>().SetElementContents(i, itemsLog.items[i].name, itemsLog.items[i].price, itemsLog.items[i].texture);
        }

        GridLayoutGroup containerLayout = itemsContainer.GetComponent<GridLayoutGroup>();        
        RectTransform containerTransform = itemsContainer.GetComponent<RectTransform>();

        containerTransform.sizeDelta = new Vector2(containerTransform.sizeDelta.x, (itemsLog.items.Count % 3) * containerLayout.cellSize.y);
    }
    #endregion
}