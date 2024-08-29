using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody), typeof(Colorizer), typeof(Renderer))]
public class Cube : MonoBehaviour, IDestroyable
{
    [SerializeField] private float _minCubeLifeTime;
    [SerializeField] private float _maxCubeLifeTime;

    private Color _defaultColor;
    private Renderer _renderer;
    private bool _isColorChanged = false;
    private Coroutine _deathTimer;
    private Colorizer _colorizer;

    public event Action<IDestroyable> Destroyed;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _colorizer = GetComponent<Colorizer>();
        _defaultColor = _renderer.material.color;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Platform platform))
        {
            StartDeathTimer();

            if (_isColorChanged == false)
            {
                _colorizer.Colorize(_renderer.material);
                _isColorChanged = true;
            }
        }
    }
    public void Reset()
    {
        _isColorChanged = false;
        _renderer.material.color = _defaultColor;

        if (_deathTimer != null)
            StopCoroutine(_deathTimer);
    }

    private void StartDeathTimer()
    {
        if (_deathTimer != null)
            StopCoroutine(_deathTimer);

        _deathTimer = StartCoroutine(nameof(SetDeathTime));
    }

    private float GetRanomCubeLifeTime() => Random.Range(_minCubeLifeTime, _maxCubeLifeTime);

    private IEnumerator SetDeathTime()
    {
        float time = GetRanomCubeLifeTime();
        var wait = new WaitForSeconds(time);
        yield return wait;
        Destroyed?.Invoke(this);
    }
}
