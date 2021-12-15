using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private Transform _point;

    private ObjectsSelector _objectsSelector;

    public bool IsPlayerOnBuilding { get; private set; }

    public Transform PointFirst => _point;

    public void OnBuildingSelect()
    {
        IsPlayerOnBuilding = true;
    }

    private void OnEnable()
    {
        _objectsSelector = FindObjectOfType<ObjectsSelector>();
        _objectsSelector.TargetPointSelected += OnRoadSelector;
    }

    private void OnDisable()
    {
        _objectsSelector.TargetPointSelected -= OnRoadSelector;
    }

    private void OnRoadSelector()
    {
        IsPlayerOnBuilding = false;
    }
}
