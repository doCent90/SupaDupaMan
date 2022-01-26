using System;
using UnityEngine;

public class FlyPoint : MonoBehaviour
{
    [SerializeField] private Transform _position;
    [SerializeField] private Transform _nextPoint;

    public Transform Position => _position;
    public Transform NextPoint => _nextPoint;

    private void OnEnable()
    {
        if (_position == null || _nextPoint == null)
            throw new NullReferenceException(nameof(FlyPoint));
    }
}
