using System.Collections;
using UnityEngine;

public abstract class ObjectSpawner<T> : MonoBehaviour where T : Component, IDestroyable
{
    [SerializeField] private ObjectPool<T> _pool;

    protected void SpawnObject(Vector3 position)
    {
        T obj = _pool.GetObject();
        obj.transform.position = position;
        obj.Destroyed += OnObjectDestroyed;
    }

    protected virtual void OnObjectDestroyed(IDestroyable destroyable)
    {
        destroyable.Destroyed -= OnObjectDestroyed;
        destroyable.Reset();
        _pool.ReturnObject(destroyable as T);
    }
}
