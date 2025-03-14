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

        // 풀에 미리 객체 생성
        for (int i = 0; i < initialPoolSize; i++)
        {
            T obj = Object.Instantiate(prefab);
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    // 풀에서 객체를 가져오는 함수
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
            // 풀에 객체가 없으면 null 반환 (새로 생성하지 않음)
            return null;
        }
    }

    // 사용이 끝난 객체를 풀에 반환하는 함수
    public void ReturnObject(T obj)
    {
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }
}
