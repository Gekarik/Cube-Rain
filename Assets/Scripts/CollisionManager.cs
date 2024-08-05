using System.Collections.Generic;

public class CollisionManager
{
    private Dictionary<Cube, Platform> _cubeByPlatform = new Dictionary<Cube, Platform>();

    public bool TryRegistryFirstCollision(Cube cube, Platform platform)
    {
        if (_cubeByPlatform.ContainsKey(cube))
            return false;

        _cubeByPlatform[cube] = platform;
        return true;
    }
}
