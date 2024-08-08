using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody), typeof(Colorizer), typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _minCubeLifeTime;
    [SerializeField] private float _maxCubeLifeTime;

    private Color _defaultColor;
    private Renderer _renderer;
    private bool _isColorChanged = false;
    private Coroutine _deathTimer;
    private Colorizer _colorizer;

    public event Action<Cube> Destroyed;

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
                _colorizer.Colorize(this);
                _isColorChanged = true;
            }
        }
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

    public void Reset()
    {
        _isColorChanged = false;
        _renderer.material.color = _defaultColor;

        if (_deathTimer != null)
            StopCoroutine(_deathTimer);
    }
}
