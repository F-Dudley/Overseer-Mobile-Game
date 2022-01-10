using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

[System.Serializable]
public class Pool
{
    public int maxSize = 50;
    public int activePoolItems = 0;
    public Queue<GameObject> contents = new Queue<GameObject>();
}

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private Pool assaultPool;
    [SerializeField] private Pool artilleryPool;
    [SerializeField] private Pool supportPool;

    #region Unity Functions
    private void Start()
    {
        GrowAssaultPool(assaultPool.maxSize / 4);
        //GrowArtilleryPool();
        GrowSupportPool(supportPool.maxSize / 4);
    }
    #endregion

    #region Assault Pool
    public GameObject TakeAssaultItem()
    {
        if (assaultPool.contents.Count == 0) GrowAssaultPool(assaultPool.maxSize / 4);

        assaultPool.activePoolItems++;
        GameObject itemFromPool =  assaultPool.contents.Dequeue();
        NavMeshAgent itemAgent = itemFromPool.GetComponent<NavMeshAgent>();

        if (itemAgent.Warp(GameManager.SpawnPoint.position))
        {
            Debug.Log("Warped to location");
        }

        itemFromPool.SetActive(true);

        return itemFromPool;
    }

    public void ReturnAssaultItem(GameObject _itemToReturn)
    {
        assaultPool.activePoolItems--;
        assaultPool.contents.Enqueue(_itemToReturn);
    }

    private void GrowAssaultPool(int _growByNumber)
    {
        if (assaultPool.contents.Count + assaultPool.activePoolItems < assaultPool.maxSize)
        {
            Debug.Log("Growing Assault Pool");

            for (int i = 0; i < _growByNumber; i++)
            {
                GameObject newItem = Instantiate(GameManager.GameEnviromentScript.GetRandomAssaultEnemy(), GameManager.SpawnPoint.position, GameManager.SpawnPoint.rotation);
                newItem.transform.localScale *= GameManager.SpawnedInItemsScalar;
                newItem.SetActive(false);

                assaultPool.contents.Enqueue(newItem);
            }
        }
    }
    #endregion

    #region Artillery Pool
    public GameObject TakeArtilleryItem()
    {
        if (artilleryPool.contents.Count <= 0) GrowArtilleryPool(artilleryPool.maxSize / 4);

        artilleryPool.activePoolItems++;
        GameObject itemFromPool =  artilleryPool.contents.Dequeue();
        itemFromPool.transform.position = GameManager.SpawnPoint.position;
        itemFromPool.transform.rotation = GameManager.SpawnPoint.rotation;
        itemFromPool.SetActive(true);

        return itemFromPool;
    }

    public void ReturnArtilleryItem(GameObject _itemToReturn)
    {
        artilleryPool.activePoolItems--;
        artilleryPool.contents.Enqueue(_itemToReturn);
    }
    
    private void GrowArtilleryPool(int _growByNumber)
    {
        if (artilleryPool.contents.Count + artilleryPool.activePoolItems <= artilleryPool.maxSize)
        {
            Debug.Log("Growing Artillery Pool");
            for (int i = 0; i < _growByNumber; i++)
            {
                GameObject newItem = Instantiate(GameManager.GameEnviromentScript.GetRandomArtilleryEnemy(), GameManager.SpawnPoint.position, GameManager.SpawnPoint.rotation);
                newItem.transform.localScale *= GameManager.SpawnedInItemsScalar;
                newItem.SetActive(false);

                assaultPool.contents.Enqueue(newItem);
            }
        }
    }
    #endregion

    #region Support Pool
    public GameObject TakeSupportItem()
    {
        if (supportPool.contents.Count <= 0) GrowSupportPool(supportPool.maxSize / 4);

        supportPool.activePoolItems++;
        GameObject itemFromPool =  supportPool.contents.Dequeue();
        itemFromPool.transform.position = GameManager.SpawnPoint.position;
        itemFromPool.transform.rotation = GameManager.SpawnPoint.rotation;
        itemFromPool.SetActive(true);

        return itemFromPool;
    }

    public void ReturnSupportItem(GameObject _itemToReturn)
    {
        supportPool.activePoolItems--;
        supportPool.contents.Enqueue(_itemToReturn);
    }

    private void GrowSupportPool(int _growByNumber)
    {
        if (supportPool.contents.Count + supportPool.activePoolItems <= supportPool.maxSize)
        {
            Debug.Log("Growing Support Pool");

            for (int i = 0; i < (supportPool.maxSize / 20); i++)
            {
                GameObject newItem = Instantiate(GameManager.GameEnviromentScript.GetRandomSupportEnemy(), GameManager.SpawnPoint.position, GameManager.SpawnPoint.rotation);
                newItem.transform.localScale *= GameManager.SpawnedInItemsScalar;
                newItem.SetActive(false);

                supportPool.contents.Enqueue(newItem);
            }
        }
    }
    #endregion
}