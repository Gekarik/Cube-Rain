using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Bomb _bombPrefab;
    [SerializeField] private Transform _container;

    private Queue<Cube> _cubePool = new Queue<Cube>();
    private Queue<Bomb> _bombPool = new Queue<Bomb>();

    public int CubeCreatedCount { get; private set; }
    public int BombCreatedCount { get; private set; }
    public int CubeSpawnedCount { get; private set; }
    public int BombSpawnedCount { get; private set; }

    public T GetObject<T>() where T : Component, IDestroyable
    {
        if (typeof(T) == typeof(Cube))
        {
            CubeSpawnedCount++;
            return GetObjectFromPool(_cubePool, _cubePrefab) as T;
        }
        else if (typeof(T) == typeof(Bomb))
        {
            BombSpawnedCount++;
            return GetObjectFromPool(_bombPool, _bombPrefab) as T;
        }

        return null;
    }

    public void PutObject(IDestroyable obj)
    {
        var component = obj as Component;

        if (obj is Cube cube)
            _cubePool.Enqueue(cube);

        else if (obj is Bomb bomb)
            _bombPool.Enqueue(bomb);

        component.gameObject.SetActive(false);
    }

    public int GetCount<T>()
    {
        if (typeof(T) == typeof(Cube))
            return _cubePool.Count;

        else if (typeof(T) == typeof(Bomb))
            return _bombPool.Count;

        return 0;
    }

    private T GetObjectFromPool<T>(Queue<T> pool, T prefab) where T : Component, IDestroyable
    {
        if (pool.Count > 0)
        {
            T obj = pool.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }

        IncrementCreatedCount<T>();

        return Instantiate(prefab, _container);
    }

    private void IncrementCreatedCount<T>() where T : Component, IDestroyable
    {
        if (typeof(T) == typeof(Cube))
            CubeCreatedCount++;

        else if (typeof(T) == typeof(Bomb))
            BombCreatedCount++;
    }
}