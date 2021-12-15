using UnityEngine;

public class FlyPoint : MonoBehaviour
{
    [SerializeField] private Transform _nextPoint;
    [SerializeField] private Transform _position;

    public Transform Position => _position;
    public Transform NextPoint => _nextPoint;
}
