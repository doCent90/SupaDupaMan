using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class WallSlicer : SlicerType1
{
    private BoxCollider _boxCollider;
    private MeshCollider _meshCollider;
    private Transform _damagePointPosition;
    private WallDamagePointMover _damagePointMover;

    public event UnityAction<Transform> ApplyDamage;
    public event UnityAction Destroyed;

    public void TakeDamage()
    {
        _elapsedTime = DestroingTime;
        _isDamaged = true;

        _meshCollider.convex = true;
        _boxCollider.enabled = false;

        ApplyDamage?.Invoke(_damagePointPosition);
        _damagePointMover.Init();
    }

    protected override void DisableDafaultObjects()
    {
        _damagePointMover.enabled = false;
        Destroyed?.Invoke();
        
        enabled = false;
    }

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _damagePointMover = GetComponentInChildren<WallDamagePointMover>();

        _meshCollider = GetComponentInChildren<MeshCollider>();
        _damagePointPosition = _damagePointMover.transform;
    }
}