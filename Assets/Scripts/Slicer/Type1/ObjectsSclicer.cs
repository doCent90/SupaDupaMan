using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CellsDestroyer))]
public class ObjectsSclicer : SlicerType1
{
    private MeshCollider _collider;
    private MeshRenderer _meshRenderer;
    private CellsDestroyer _cellsDestroyer;
    private ParticleSystem _particleSystem;

    public event UnityAction<Transform> ApplyDamage;
    public event UnityAction Destroyed;

    protected override void InitDamage()
    {
        _elapsedTime = DestroingObjectsTime;

        ApplyDamage?.Invoke(transform);
    }

    protected override void DisableDafaultObjects()
    {
        _cellsDestroyer.enabled = true;
        _meshRenderer.enabled = false;
        _collider.enabled = false;
        _particleSystem.Play();
        Destroyed?.Invoke();
    }

    private void Start()
    {
        _collider = GetComponent<MeshCollider>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _cellsDestroyer = GetComponent<CellsDestroyer>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }
}
