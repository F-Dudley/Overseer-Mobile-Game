using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T>
{
    public Queue<T> pool = new Queue<T>();

    #region Pool Functions
    public T TakeItem()
    {
        if (pool.Count > 0)
        {
            for (int i = 0; i < 5; i++)
            {
                // Instantiae()   
            }
                      
        }

        return pool.Dequeue(); 
    }
    #endregion
}
