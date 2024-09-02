using UnityEngine;

public class BombSpawner : ObjectSpawner<Bomb>
{
    public void SpawnBomb(Vector3 position)
    {
        SpawnObject(position);
    }
}
