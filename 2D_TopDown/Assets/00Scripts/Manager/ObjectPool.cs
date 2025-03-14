using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private Queue<T> pool;
    private T prefab;
    private int initialPoolSize;

    public ObjectPool(T prefab, int initialPoolSize)
    {
        this.prefab = prefab;
        this.initialPoolSize = initialPoolSize;
        pool = new Queue<T>();

        for (int i = 0; i < initialPoolSize; i++)
        {
            T obj = Object.Instantiate(prefab);
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public T GetObject()
    {
        if (pool.Count > 0)
        {
            T obj = pool.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            return null;
        }
    }

    public void ReturnObject(T obj)
    {
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }
}
