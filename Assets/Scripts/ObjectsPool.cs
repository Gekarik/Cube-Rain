using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    private Queue<Cube> _pool;

    private void Awake()
    {
        _pool = new Queue<Cube>();
    }

    public Cube GetObject()
    {
        if (_pool.Count == 0)
        {
            var cube = Instantiate(_cubePrefab);
            return cube;
        }

        return _pool.Dequeue();
    }

    public void PutObject(Cube cube)
    {
        cube.gameObject.SetActive(false);
        _pool.Enqueue(cube);
    }
}
