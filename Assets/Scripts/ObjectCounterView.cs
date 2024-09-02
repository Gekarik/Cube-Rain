using TMPro;
using UnityEngine;

public class ObjectCounterView<T> : MonoBehaviour where T : Component, IDestroyable
{
    [SerializeField] private ObjectPool<T> _pool;
    [SerializeField] private TextMeshProUGUI _spawnedField;
    [SerializeField] private TextMeshProUGUI _activeField;
    [SerializeField] private TextMeshProUGUI _instantiatedField;

    private void Update()
    {
        _spawnedField.text = $" Spawned: {_pool.Spawned}";
        _activeField.text = $" Active: {_pool.GetActiveCount()}"; 
        _instantiatedField.text = $" Instantiated: {_pool.Instantiated}";
    }
}
