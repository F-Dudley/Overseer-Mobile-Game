using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    [SerializeField] private Transform spawnPoint;

    #region Unity Functions
    private void Start()
    {
        spawnPoint = GameManager.GameEnviromentScript.spawnPoint;

        GrowAssaultPool();
        //GrowArtilleryPool();
        //GrowSupportPool();
    }
    #endregion

    #region Assault Pool
    public GameObject TakeAssaultItem()
    {
        if (assaultPool.contents.Count == 0) GrowAssaultPool();

        assaultPool.activePoolItems++;
        GameObject itemFromPool =  assaultPool.contents.Dequeue();
        itemFromPool.transform.position = spawnPoint.position;
        itemFromPool.transform.rotation = spawnPoint.rotation;
        itemFromPool.SetActive(true);

        return itemFromPool;
    }

    public void ReturnAssaultItem(GameObject _itemToReturn)
    {
        assaultPool.activePoolItems--;
        assaultPool.contents.Enqueue(_itemToReturn);
    }

    private void GrowAssaultPool()
    {
        if (assaultPool.contents.Count + assaultPool.activePoolItems < assaultPool.maxSize)
        {
            Debug.Log("Growing Assault Pool");

            for (int i = 0; i < (assaultPool.maxSize / 20); i++)
            {
                GameObject newItem = Instantiate(GameManager.GameEnviromentScript.GetRandomAssaultEnemy(), spawnPoint.position, spawnPoint.rotation);
                newItem.transform.localScale *= GameManager.SpawnedInItemsScalar;
                assaultPool.contents.Enqueue(newItem);
            }
        }
    }
    #endregion

    #region Artillery Pool
    public GameObject TakeArtilleryItem()
    {
        if (artilleryPool.contents.Count <= 0) GrowArtilleryPool();

        artilleryPool.activePoolItems++;
        GameObject itemFromPool =  artilleryPool.contents.Dequeue();
        itemFromPool.transform.position = spawnPoint.position;
        itemFromPool.transform.rotation = spawnPoint.rotation;
        itemFromPool.SetActive(true);

        return itemFromPool;
    }

    public void ReturnArtilleryItem(GameObject _itemToReturn)
    {
        artilleryPool.activePoolItems--;
        artilleryPool.contents.Enqueue(_itemToReturn);
    }
    
    private void GrowArtilleryPool()
    {
        if (artilleryPool.contents.Count + artilleryPool.activePoolItems <= artilleryPool.maxSize)
        {
            Debug.Log("Growing Artillery Pool");
            for (int i = 0; i < (artilleryPool.maxSize / 20); i++)
            {
                GameObject newItem = Instantiate(GameManager.GameEnviromentScript.GetRandomArtilleryEnemy(), spawnPoint.position, spawnPoint.rotation);
                newItem.transform.localScale *= GameManager.SpawnedInItemsScalar;
                assaultPool.contents.Enqueue(newItem);
            }
        }
    }
    #endregion

    #region Support Pool
    public GameObject TakeSupportItem()
    {
        if (supportPool.contents.Count <= 0) GrowArtilleryPool();

        supportPool.activePoolItems++;
        GameObject itemFromPool =  supportPool.contents.Dequeue();
        itemFromPool.transform.position = spawnPoint.position;
        itemFromPool.transform.rotation = spawnPoint.rotation;
        itemFromPool.SetActive(true);
        
        return itemFromPool;
    }

    public void ReturnSupportItem(GameObject _itemToReturn)
    {
        supportPool.activePoolItems--;
        supportPool.contents.Enqueue(_itemToReturn);
    }

    private void GrowSupportPool()
    {
        if (supportPool.contents.Count + supportPool.activePoolItems <= supportPool.maxSize)
        {
            Debug.Log("Growing Support Pool");

            for (int i = 0; i < (supportPool.maxSize / 20); i++)
            {
                GameObject newItem = Instantiate(GameManager.GameEnviromentScript.GetRandomArtilleryEnemy(), spawnPoint.position, spawnPoint.rotation);
                newItem.transform.localScale *= GameManager.SpawnedInItemsScalar;
                supportPool.contents.Enqueue(newItem);
            }
        }
    }
    #endregion
}