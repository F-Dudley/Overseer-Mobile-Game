using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemElement : MonoBehaviour
{
    [Header("Button Values")]
    [SerializeField] private int itemIndex = -1;

    [Header("Element References")]
    [SerializeField] private TextMeshProUGUI itemTitleUI;
    [SerializeField] private TextMeshProUGUI itemPriceUI;
    [SerializeField] private Image itemSpriteUI;

    #region Element Functions
    public void SetElementContents(string _itemName, int _itemPrice, Sprite _itemTexture)
    {
        itemTitleUI.text = _itemName;
        itemPriceUI.text = _itemPrice.ToString();
        itemSpriteUI.sprite = _itemTexture;
    }

    public void ElementPressed()
    {
        GameShop.instance.PurchaseItem(itemIndex);
    }
    #endregion
}
