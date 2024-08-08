using UnityEngine;

public class Colorizer : MonoBehaviour
{
    public void Colorize(Material material)
    {
        material.color = Random.ColorHSV();
    }
}
