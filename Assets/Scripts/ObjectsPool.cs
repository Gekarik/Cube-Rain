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
        Cube cube = null;

        if (_pool.Count > 0)
        {
            cube = _pool.Dequeue();
            cube.gameObject.SetActive(true);
            return cube;
        }

        cube = Instantiate(_cubePrefab, _container);
        return cube;
    }

    public void PutObject(Cube cube)
    {
        cube.gameObject.SetActive(false);
        _pool.Enqueue(cube);
    }
}
