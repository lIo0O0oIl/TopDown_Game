using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : PoolableMono
{
    private Stack<T> pool = new Stack<T>();
    private T prefab;       // 모자랄 때 찍어내기 위해 값을 기억해둔다.
    private Transform parent;

    public Pool(T prefab, Transform parent, int count)
    {
        this.prefab = prefab;
        this.parent = parent;

        for (int i = 0; i < count; i++)
        {
            T obj = GameObject.Instantiate(prefab, parent);
            obj.gameObject.name = obj.gameObject.name.Replace("(Clone)", "");
            obj.gameObject.SetActive(false);
            pool.Push(obj);
        }
    }

    public T Pop()
    {
        T obj = null;
        if (pool.Count <= 0)
        {
            obj = GameObject.Instantiate(prefab, parent);
            obj.gameObject.name = obj.gameObject.name.Replace("(Clonee)", "");
        }
        else
        {
            obj = pool.Pop();
            obj.gameObject.SetActive(true);
        }
        return obj;
    }

    public void Push(T obj)
    {
        obj.gameObject.SetActive(false);
        pool.Push(obj);
    }
}
