using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum PoolObjectType
{
    ASSAULT,
    ARTILLERY,
    SUPPORT
}
public class ObjectPool : MonoBehaviour
{
    public PoolObjectType poolType;

    public Queue<GameObject> pool = new Queue<GameObject>();
    [SerializeField] private Func<GameObject> getNewPoolItem;

    [SerializeField] private int maxPoolSize = 100;
    [SerializeField] private int activePoolItems = 0;

    private void Start()
    {
        switch (poolType)
        {
            case PoolObjectType.ASSAULT:
                getNewPoolItem = GameManager.GameEnviromentScript.GetRandomAssultEnemy;
                break;

            case PoolObjectType.ARTILLERY:
                getNewPoolItem = GameManager.GameEnviromentScript.GetRandomArtilleryEnemy;
                break;

            case PoolObjectType.SUPPORT:
                getNewPoolItem = GameManager.GameEnviromentScript.GetRandomSupportEnemy;
                break;
        }

        GrowPool();
    }

    #region Pool Functions
    public GameObject TakeItem()
    {
        if (pool.Count == 0) GrowPool();

        GameObject item = pool.Dequeue();
        item.SetActive(true);

        activePoolItems++;
        return item;
    }

    public GameObject TakeItem(Vector3 _position, Quaternion _rotation)
    {
        if (pool.Count == 0) GrowPool();

        GameObject item = pool.Dequeue();
        item.SetActive(true);
        item.transform.position = _position;
        item.transform.rotation = _rotation;

        activePoolItems++;
        return item;
    }

    public GameObject TakeItem(Transform _targetTransform)
    {
        if (pool.Count == 0) GrowPool();

        if (pool.Count > 0)
        {
            GameObject item = pool.Dequeue();
            item.SetActive(true);
            item.transform.position = _targetTransform.position;
            item.transform.rotation = _targetTransform.rotation;

            activePoolItems++;

            return item;           
        }
        else
        {
            Debug.Log("Pool Max Size Reached");
            return null;
        }
    }

    public void ReturnItem(GameObject _itemToReturn)
    {
        activePoolItems--;
        pool.Enqueue(_itemToReturn);
    }

    private void GrowPool()
    {
        if (activePoolItems + pool.Count < maxPoolSize)
        {
            for (int i = 0; i < (maxPoolSize / 20); i++)
            {
                Debug.Log(string.Format("Adding Item To - {0} Pool", poolType.ToString()));
                GameObject newItem = Instantiate(getNewPoolItem(), GameManager.SpawnPoint.position, GameManager.SpawnPoint.rotation, GameManager.EnemyContainer);                newItem.SetActive(false);
                newItem.SetActive(false);
                pool.Enqueue(newItem);
            }
        }
    }
    #endregion
}
