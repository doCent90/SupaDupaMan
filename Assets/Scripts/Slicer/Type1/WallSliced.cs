using UnityEngine;
using UnityEngine.Events;

public class WallSliced : SlicerType1
{
    private BoxCollider _boxCollider;
    private MeshRenderer _meshRenderer;
    private Transform _damagePointPosition;
    private CellsDestroyer _cellsDestroyer;
    private WallDamagePointMover _damagePointMover;

    public event UnityAction<Transform> ApplyDamage;
    public event UnityAction Destroyed;

    public void TakeDamage()
    {
        _elapsedTime = DestroyTime;
        _isDamaged = true;

        _boxCollider.enabled = false;
        _meshRenderer.enabled = false;

        ApplyDamage?.Invoke(_damagePointPosition);
        _damagePointMover.Init();
    }

    protected override void DisableDafaultObjects()
    {
        _damagePointMover.enabled = false;
        Destroyed?.Invoke();
        
        _cellsDestroyer.enabled = true;
        enabled = false;
    }

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _cellsDestroyer = GetComponent<CellsDestroyer>();
        _damagePointMover = GetComponentInChildren<WallDamagePointMover>();

        _damagePointPosition = _damagePointMover.transform;
    }
}