using System.Collections;
using UnityEngine;

public class CubeSpawner : ObjectSpawner<Cube>
{
    [SerializeField] private BoxCollider _spawnArea;
    [SerializeField] private BombSpawner _bombSpawner;
    [SerializeField] private float _spawnInterval = 1f;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    protected override void OnObjectDestroyed(IDestroyable destroyable)
    {
        base.OnObjectDestroyed(destroyable);

        Component component = destroyable as Component;
        _bombSpawner.SpawnBomb(component.transform.position);
    }

    private IEnumerator SpawnRoutine()
    {
        var wait = new WaitForSeconds(_spawnInterval);

        while (true)
        {
            var position = GetSpawnPosition();
            SpawnObject(position);
            yield return wait;
        }
    }

    private Vector3 GetSpawnPosition()
    {
        Bounds bounds = _spawnArea.bounds;
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}
