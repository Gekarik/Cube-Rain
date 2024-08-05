using UnityEngine;

public class Colorizer : MonoBehaviour
{
    public void Colorize(Cube cube)
    {
        if (cube.TryGetComponent(out Renderer renderer))
            renderer.sharedMaterial.color = Random.ColorHSV();
    }
}
