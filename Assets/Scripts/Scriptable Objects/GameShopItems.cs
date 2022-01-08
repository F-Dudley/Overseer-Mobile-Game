using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShopItem
{
    public string name;
    public int price;

    [Space]

    public Sprite texture;
}

[CreateAssetMenu(fileName = "New_Shop_Items", menuName = "ScriptableObjects/Shop Items", order = 1)]
public class GameShopItems : ScriptableObject
{
    public List<ShopItem> items;
}