using UnityEngine;

public class ObjectCounter : MonoBehaviour
{
    [SerializeField] private ObjectsPool _pool;

    public int GetInsantiatedCount<T>() where T : Component
    {
        if (typeof(T) == typeof(Cube))
            return _pool.CubeCreatedCount;

        else if (typeof(T) == typeof(Bomb))
            return _pool.BombCreatedCount;

        return 0;
    }

    public int GetActiveCount<T>() where T : Component
    {
        if (typeof(T) == typeof(Cube))
            return _pool.CubeCreatedCount - _pool.GetCount<Cube>();

        else if (typeof(T) == typeof(Bomb))
            return _pool.BombCreatedCount - _pool.GetCount<Bomb>();

        return 0;
    }

    public int GetSpawnedCount<T>() where T : Component
    {
        if (typeof(T) == typeof(Cube))
            return _pool.CubeSpawnedCount;

        else if (typeof(T) == typeof(Bomb))
            return _pool.BombSpawnedCount;

        return 0;
    }
}
