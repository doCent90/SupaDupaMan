using System;
using UnityEngine;

public class FreeWayChecker : MonoBehaviour
{
    [SerializeField] private BoxCollider _boxCollider;

    public event Action EdgePlatformReached;

    private void OnEnable()
    {
        if (_boxCollider == null)
            throw new NullReferenceException(nameof(FreeWayChecker));
    }

    private void OnTriggerExit(Collider other)
    {
        EdgePlatformReached?.Invoke();
    }
}
