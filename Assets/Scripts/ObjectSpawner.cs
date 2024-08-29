using System;
using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnTime;
    [SerializeField] private ObjectsPool _pool;
    [SerializeField] private BoxCollider _spawnArea;

    private void Start()
    {
        StartCoroutine(nameof(SpawnObjects));
    }

    private void OnObjectDestroy(IDestroyable destroyableObject)
    {
        destroyableObject.Destroyed -= OnObjectDestroy;
        destroyableObject.Reset();
        _pool.PutObject(destroyableObject);

        if (destroyableObject is Cube)
        {
            Vector3 position = (destroyableObject as Component).transform.position;
            Spawn<Bomb>(position);
        }
    }

    private Vector3 CalculatePosition()
    {
        Bounds bounds = _spawnArea.bounds;
        return new Vector3(
            UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
            UnityEngine.Random.Range(bounds.min.y, bounds.max.y),
            UnityEngine.Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    private void Spawn<T>(Vector3 position) where T : MonoBehaviour, IDestroyable
    {
        T obj = _pool.GetObject<T>();
        obj.transform.position = position;
        obj.Destroyed += OnObjectDestroy;
    }

    private IEnumerator SpawnObjects()
    {
        var wait = new WaitForSeconds(_spawnTime);

        while (true)
        {
            var position = CalculatePosition();
            Spawn<Cube>(position);
            yield return wait;
        }
    }
}
