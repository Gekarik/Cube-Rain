using TMPro;
using UnityEngine;

[RequireComponent(typeof(ObjectCounter))]
public class ObjectCounterView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _activeField;
    [SerializeField] private TextMeshProUGUI _spawnedField;
    [SerializeField] private TextMeshProUGUI _instantiatedField;
    [SerializeField] private CountingObject _countingObject;
    
    private const string Active = nameof(Active);
    private const string Spawned = nameof(Spawned);
    private const string Instantiated = nameof(Instantiated);

    private ObjectCounter _objectCounter;
    private int _active;
    private int _spawned;
    private int _instantiated;

    private enum CountingObject
    {
        Cube = 0,
        Bomb
    }

    private void Start()
    {
        _objectCounter = GetComponent<ObjectCounter>();
    }

    private void Update()
    {
        UpdateCounters();
        UpdateUI();
    }

    private void UpdateCounters()
    {
        switch (_countingObject)
        {
            case CountingObject.Cube:
                _spawned = _objectCounter.GetSpawnedCount<Cube>();
                _active = _objectCounter.GetActiveCount<Cube>();
                _instantiated = _objectCounter.GetInsantiatedCount<Cube>();
                break;

            case CountingObject.Bomb:
                _spawned = _objectCounter.GetSpawnedCount<Bomb>();
                _active = _objectCounter.GetActiveCount<Bomb>();
                _instantiated = _objectCounter.GetInsantiatedCount<Bomb>();
                break;
        }
    }

    private void UpdateUI()
    {
        _activeField.text = $"{Active}: {_active}";
        _spawnedField.text = $"{Spawned}: {_spawned}";
        _instantiatedField.text = $"{Instantiated}: {_instantiated}";
    }
}