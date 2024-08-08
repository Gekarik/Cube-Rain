using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Transform _container;

    private Queue<Cube> _pool;

    private void Awake()
    {
        _pool = new Queue<Cube>();
    }

    public Cube GetObject()
    {
        if (_pool.Count > 0)
        {
            var cube = _pool.Dequeue();
            cube.gameObject.SetActive(true);
            return cube;
        }

        return Instantiate(_cubePrefab, _container);
    }

    public void PutObject(Cube cube)
    {
        cube.gameObject.SetActive(false);
        _pool.Enqueue(cube);
    }
}
