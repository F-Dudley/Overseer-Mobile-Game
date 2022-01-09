using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct InventoryItem
{
    private int index;
    private int stackAmount;

    public InventoryItem(int _index)
    {
        index = _index;
        stackAmount = 0;
    }

    public int Index
    {
        get {
            return index;
        }
    }

    public int StackAmount
    {
        get {
            return stackAmount;
        }
    }

    public void AddToStack() => stackAmount++;
    public void RemoveFromStack() => stackAmount--;
}

public class GameInventory : MonoBehaviour
{
    public static GameInventory instance;

    [SerializeField] private List<InventoryItem> inventoryItems = new List<InventoryItem>();

    [Header("Other")]
    [SerializeField] private GameItems gameItems;

    #region Unity Functions
    private void Awake() => instance = this;

    private void Start()
    {
        for (int i = 0; i < gameItems.items.Count; i++)
        {
            inventoryItems.Add(new InventoryItem(i));
        }

        CreateInventory();
    }
    #endregion

    #region Inventory Functions
    private void CreateInventory()
    {
        foreach (GameItem item in gameItems.items)
        {
            
        }
    }    

    public void Add(int _itemIndex)
    {
        inventoryItems[_itemIndex].AddToStack();
    }

    public void Remove(int _itemIndex)
    {
        inventoryItems[_itemIndex].RemoveFromStack();
    }
    #endregion

    #region UI Functions
    private void UpdateInventoryUI()
    {

    }
    #endregion
}
