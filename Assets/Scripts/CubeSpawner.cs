using System.Collections;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnTime;
    [SerializeField] private float _minCubeLifeTime;
    [SerializeField] private float _maxCubeLifeTime;
    [SerializeField] private ObjectsPool _pool;
    [SerializeField] private Colorizer _colorizer;

    private CollisionManager _collisionManager = new();

    private void Start()
    {
        StartCoroutine(nameof(SpawnCubes));
    }

    private void OnCubeTouchedPlatform(Cube cube, Platform platform)
    {
        if (_collisionManager.TryRegistryFirstCollision(cube, platform))
        {
            StartCoroutine(DestroyCube(cube));
            _colorizer.Colorize(cube);
        }
    }

    private IEnumerator SpawnCubes()
    {
        var wait = new WaitForSeconds(_spawnTime);

        while (true)
        {
            var cube = _pool.GetObject();
            cube.transform.position = transform.position;
            cube.TouchedPlatform += OnCubeTouchedPlatform;
            yield return wait;
        }
    }

    private IEnumerator DestroyCube(Cube cube)
    {
        yield return new WaitForSeconds(GetRanomCubeLifeTime());
        cube.TouchedPlatform -= OnCubeTouchedPlatform;
        _pool.PutObject(cube);
    }

    private float GetRanomCubeLifeTime() => Random.Range(_minCubeLifeTime, _maxCubeLifeTime);
}