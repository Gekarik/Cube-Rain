using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Exploser), typeof(Colorizer))]
[RequireComponent(typeof(Renderer), typeof(RenderSwitcher))]
public class Bomb : MonoBehaviour, IDestroyable
{
    [SerializeField] private float _minLifeTime = 2f;
    [SerializeField] private float _maxlLfeTime = 5f;

    private Exploser _exploser;
    private Colorizer _colorizer;
    private Renderer _renderer;
    private RenderSwitcher _renderSwitcher;
    private Coroutine _deathTimer;
    private Coroutine _fadeRoutine;

    public event Action<IDestroyable> Destroyed;

    private void Awake()
    {
        _renderSwitcher = GetComponent<RenderSwitcher>();
        _renderer = GetComponent<Renderer>();
        _exploser = GetComponent<Exploser>();
        _colorizer = GetComponent<Colorizer>();
    }

    private void OnEnable()
    {
        if (_deathTimer == null)
            _deathTimer = StartCoroutine(SetDeathTime());
    }

    private IEnumerator SetDeathTime()
    {
        _renderSwitcher.SetMaterialRenderingMode(_renderer.material, RenderSwitcher.RenderingMode.Fade);
        yield return _fadeRoutine = StartCoroutine(_colorizer.Fade(_renderer.material, _maxlLfeTime));
        _exploser.Explose();
        Destroyed?.Invoke(this);
    }

    public void Reset()
    {
        if (_deathTimer != null)
        {
            StopCoroutine(_deathTimer);
            _deathTimer = null;
        }

        if (_fadeRoutine != null)
        {
            StopCoroutine(_fadeRoutine);
            _fadeRoutine = null;
        }

        ResetAlpha(_renderer.material);
    }
    
    private float GetRandomLifetTime() => UnityEngine.Random.Range(_minLifeTime, _maxlLfeTime);
    
    private void ResetAlpha(Material material)
    {
        _renderSwitcher.SetMaterialRenderingMode(material, RenderSwitcher.RenderingMode.Opaque);
        Color color = material.color;
        color.a = 1f;
        material.color = color;
    }
}
