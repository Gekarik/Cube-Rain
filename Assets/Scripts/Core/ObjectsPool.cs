using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool<T> : MonoBehaviour where T : Component, IDestroyable
{
    [SerializeField] private T _prefab;
    [SerializeField] private Transform _container;

    private Queue<T> _objects = new Queue<T>();
    public int Instantiated { get; private set; }
    public int Spawned { get; private set; }

    public event Action CountersValueChanged;

    public T GetObject()
    {
        IncrementSpawned();

        if (_objects.Count > 0)
        {
            T obj = _objects.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }

        IncrementInstantiated();
        return Instantiate(_prefab, _container);
    }

    public void ReturnObject(T obj)
    {
        obj.gameObject.SetActive(false);
        _objects.Enqueue(obj);
    }

    public int GetActiveCount() => Instantiated - _objects.Count;

    private void IncrementInstantiated()
    {
        Instantiated++;
        CountersValueChanged?.Invoke();
    }

    private void IncrementSpawned()
    {
        Spawned++;
        CountersValueChanged?.Invoke();
    }
}
