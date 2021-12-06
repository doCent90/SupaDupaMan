using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(CellsDestroyer))]
[RequireComponent(typeof(CapsuleCollider))]
public class ObjectsSlicer : Slicer
{
    [SerializeField] private Rigidbody _rigidbodyDownObject;

    private MeshCollider _collider;
    private MeshRenderer _meshRenderer;
    private CellsDestroyer _cellsDestroyer;
    private ParticleSystem _particleSystem;
    private CapsuleCollider _capsuleCollider;

    public event UnityAction<Transform> ApplyDamage;
    public event UnityAction Destroyed;

    protected override void InitDamage()
    {
        ElapsedTime = DestroingObjectsTime;

        ApplyDamage?.Invoke(transform);
    }

    protected override void DisableDafaultObjects()
    {
        if (_rigidbodyDownObject != null)
            _rigidbodyDownObject.isKinematic = false;

        _capsuleCollider.enabled = false;
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
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }
}
