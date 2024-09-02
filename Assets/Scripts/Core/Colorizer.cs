using System.Collections;
using UnityEngine;

public class Colorizer : MonoBehaviour
{
    public void Colorize(Material material)
    {
        material.color = Random.ColorHSV();
    }

    public IEnumerator Fade(Material material, float fadeDuration)
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);

            Color color = material.color;
            color.a = alpha;
            material.color = color;

            yield return null;
        }
    }    
}
