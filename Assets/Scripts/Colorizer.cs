using UnityEngine;

public class Colorizer : MonoBehaviour
{
    public Color Colorize(Cube cube)
    {
        var defaultColor = Color.white;

        if (cube.TryGetComponent(out Renderer renderer))
        {
            defaultColor = renderer.material.color;
            renderer.material.color = Random.ColorHSV();
        }

        return defaultColor;
    }
}
