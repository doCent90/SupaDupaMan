using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(CellsDestroyer))]
[RequireComponent(typeof(CapsuleCollider))]
public class ObjectsSlicer : Slicer
{
    [SerializeField] private Rigidbody _rigidbodyDownObject;

    private MeshRenderer _meshRenderer;
    private CellsDestroyer _cellsDestroyer;
    private CapsuleCollider _capsuleCollider;
    private ParticleSystem[] _particleSystems;

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

        foreach (var fx in _particleSystems)
        {
            fx.Play();
        }

        Destroyed?.Invoke();
    }

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _cellsDestroyer = GetComponent<CellsDestroyer>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _particleSystems = GetComponentsInChildren<ParticleSystem>();
    }
}
