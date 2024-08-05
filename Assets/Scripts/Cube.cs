using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _minCubeLifeTime;
    [SerializeField] private float _maxCubeLifeTime;

    public event Action<Cube, Platform> TouchedPlatform;
    public event Action<Cube> Destroyed;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Platform platform))
        {
            StartTimer();
            TouchedPlatform?.Invoke(this, platform);
        }
    }

    private float GetRanomCubeLifeTime() => Random.Range(_minCubeLifeTime, _maxCubeLifeTime);

    private IEnumerator StartTimer()
    {
        float time = GetRanomCubeLifeTime();
        var wait = new WaitForSeconds(time);
        yield return wait;
        Destroyed?.Invoke(this);
    }

    public void UpdateTimer()
    {
        StopCoroutine(nameof(StartTimer));
        StartCoroutine(nameof(StartTimer));
    }
}
