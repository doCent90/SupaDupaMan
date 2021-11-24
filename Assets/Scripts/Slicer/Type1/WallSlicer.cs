using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class WallSlicer : SlicerType1
{
    [SerializeField] private Material _targetMaterial;

    private Sliceable _sliceable;
    private BoxCollider _boxCollider;
    private MeshCollider _meshCollider;
    private MeshRenderer _meshRenderer;
    private Transform _damagePointPosition;
    private WallDamagePointMover _damagePointMover;

    public event UnityAction<Transform> ApplyDamage;
    public event UnityAction Destroyed;

    protected override void InitDamage()
    {
        ElapsedTime = DestroingWallTime;
        SetMaterial();

        _meshCollider.convex = true;
        _boxCollider.enabled = false;

        ApplyDamage?.Invoke(_damagePointPosition);
        _damagePointMover.Init();
    }

    protected override void DisableDafaultObjects()
    {
        _damagePointMover.enabled = false;
        Destroyed?.Invoke();        
    }

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _sliceable = GetComponentInChildren<Sliceable>();
        _meshRenderer = _sliceable.GetComponent<MeshRenderer>();
        _damagePointMover = GetComponentInChildren<WallDamagePointMover>();

        _meshCollider = GetComponentInChildren<MeshCollider>();
        _damagePointPosition = _damagePointMover.transform;
    }

    private void SetMaterial()
    {
        _meshRenderer.material = _targetMaterial;
    }
}