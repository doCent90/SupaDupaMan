using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class WallSlicer2 : SlicerType1
{
    private BoxCollider _boxCollider;
    private MeshRenderer _meshRenderer;
    private ParticleSystem _particleSystem;
    private CellsDestroyer _cellsDestroyer;
    private Transform _damagePointPosition;
    private DamagePointWallSlicerMover _damagePoint;

    public event UnityAction<Transform> ApplyDamage;
    public event UnityAction Destroyed;

    protected override void InitDamage()
    {
        ElapsedTime = DestroingWallTime;
        _boxCollider.enabled = false;

        ApplyDamage?.Invoke(_damagePointPosition);
        _damagePoint.Init();
    }

    protected override void DisableDafaultObjects()
    {
        _cellsDestroyer.enabled = true;
        _meshRenderer.enabled = false;
        _damagePoint.enabled = false;

        _particleSystem.Play();
        Destroyed?.Invoke();        
    }

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _cellsDestroyer = GetComponent<CellsDestroyer>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _damagePoint = GetComponentInChildren<DamagePointWallSlicerMover>();
        _damagePointPosition = GetComponentInChildren<DamagePointWallSlicerMover>().transform;
    }
}