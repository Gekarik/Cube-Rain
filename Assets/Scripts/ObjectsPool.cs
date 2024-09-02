using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool<T> : MonoBehaviour where T : Component, IDestroyable
{
    [SerializeField] private T _prefab;
    [SerializeField] private Transform _container;

    public int Instantiated { get; private set; }
    public int Spawned { get; private set; }

    private readonly Queue<T> _pool = new Queue<T>();

    public T GetObject()
    {
        Spawned++;

        if (_pool.Count > 0)
        {
            T obj = _pool.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }

        Instantiated++;
        return Instantiate(_prefab, _container);
    }

    public void ReturnObject(T obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Enqueue(obj);
    }

    public int GetActiveCount() => Instantiated - _pool.Count;
}
