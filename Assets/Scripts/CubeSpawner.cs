using System.Collections;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnTime;
    [SerializeField] private ObjectsPool _pool;
    [SerializeField] private Colorizer _colorizer;
    [SerializeField] private BoxCollider _spawnArea;

    private CollisionManager _collisionManager = new();

    private void Start()
    {
        StartCoroutine(nameof(SpawnCubes));
    }

    private void OnCubeTouchedPlatform(Cube cube, Platform platform)
    {
        if (_collisionManager.TryRegistryNewPlatformCollision(cube, platform))
        {
            _colorizer.Colorize(cube);
            cube.UpdateTimer();
        }
    }

    private void OnCubeDestroy(Cube cube)
    {
        cube.TouchedPlatform -= OnCubeTouchedPlatform;
        cube.Destroyed -= OnCubeDestroy;
        _pool.PutObject(cube);
        _collisionManager.Reset(cube);
    }

    private Vector3 CalculatePosition()
    {
        Bounds bounds = _spawnArea.bounds;
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    private IEnumerator SpawnCubes()
    {
        var wait = new WaitForSeconds(_spawnTime);

        while (true)
        {
            var cube = _pool.GetObject();

            cube.transform.position = CalculatePosition();
            cube.TouchedPlatform += OnCubeTouchedPlatform;
            cube.Destroyed += OnCubeDestroy;
            yield return wait;
        }
    }
}