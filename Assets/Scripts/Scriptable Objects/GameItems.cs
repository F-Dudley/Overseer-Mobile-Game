using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameItem
{
    [Header("UI Variables")]
    public string name;
    public int price;
    public Sprite texture;

    [Header("Game Variables")]
    public GameObject itemPrefab;    
}

[CreateAssetMenu(fileName = "New_Game_Items", menuName = "ScriptableObjects/Game Items", order = 1)]
public class GameItems : ScriptableObject
{
    public List<GameItem> items;
}