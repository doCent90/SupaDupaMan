using UnityEngine;
using System.Linq;

[RequireComponent(typeof(CapsuleCollider))]
public class EnemyGigant : MonoBehaviour
{
    [SerializeField] private Material _targetMaterial;

    private Enemy[] _smallAliveEnemies;
    private CapsuleCollider _capsuleCollider;
    private SkinnedMeshRenderer _meshRenderer;

    private void OnEnable()
    {
        var enemies = FindObjectsOfType<Enemy>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        _smallAliveEnemies = enemies.Where(enemy => enemy.IsGigant == false).ToArray();

        foreach (var enemy in _smallAliveEnemies)
        {
            enemy.Died += DisableCollider;
        }
    }

    private void OnDisable()
    {
        foreach (var enemy in _smallAliveEnemies)
        {
            enemy.Died -= DisableCollider;
        }
    }

    private void DisableCollider()
    {
        var diedEnemies = _smallAliveEnemies.Where(enemy => enemy.enabled == false).ToArray();

        if (diedEnemies.Length == _smallAliveEnemies.Length)
        {
            _capsuleCollider.isTrigger = false;
            SetMaterial();
        }
        else
        {
            _capsuleCollider.isTrigger = true;
        }
    }

    private void SetMaterial()
    {
        _meshRenderer.material = _targetMaterial;
    }
}
