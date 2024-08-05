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
        Debug.Log(_pool.Count);

        if (_pool.Count > 0)
        {
            var @object = _pool.Dequeue();
            @object.gameObject.SetActive(true);
            return @object;
        }

        var cube = Instantiate(_cubePrefab, _container);
        return cube;
    }

    public void PutObject(Cube cube)
    {
        cube.gameObject.SetActive(false);
        _pool.Enqueue(cube);
    }
}
