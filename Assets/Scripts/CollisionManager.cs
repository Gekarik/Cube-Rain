using System.Collections.Generic;

public class CollisionManager
{
    private Dictionary<Cube, HashSet<Platform>> _cubeCollisions = new Dictionary<Cube, HashSet<Platform>>();

    public bool TryRegistryNewPlatformCollision(Cube cube, Platform platform)
    {
        if (_cubeCollisions.ContainsKey(cube) == false)
            _cubeCollisions[cube] = new HashSet<Platform>();

        if (_cubeCollisions[cube].Contains(platform))
            return false;

        _cubeCollisions[cube].Add(platform);
        return true;
    }

    public void Reset(Cube cube)
    {
        _cubeCollisions.Remove(cube);
    }
}