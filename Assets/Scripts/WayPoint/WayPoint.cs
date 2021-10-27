using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] private Material _targetMaterial;

    private EnemyGigant _enemyGigant;
    private MeshRenderer _meshRenderer;
    private SphereCollider _sphereCollider;

    private void OnEnable()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _enemyGigant = FindObjectOfType<EnemyGigant>();
        _sphereCollider = GetComponent<SphereCollider>();

        _enemyGigant.Activated += SetActiveGigantPoint;
    }

    private void OnDisable()
    {
        _enemyGigant.Activated -= SetActiveGigantPoint;
    }

    private void SetActiveGigantPoint()
    {
        _sphereCollider.enabled = true;
        _meshRenderer.material = _targetMaterial;
    }
}
