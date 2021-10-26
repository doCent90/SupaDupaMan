using UnityEngine;
using System.Linq;

[RequireComponent(typeof(CapsuleCollider))]
public class EnemyGigant : MonoBehaviour
{
    [SerializeField] private Material _targetMaterial;

    private Environments _cube;
    private BoxCollider _cubeCollider;
    private Enemy[] _smallAliveEnemies;
    private CapsuleCollider _capsuleCollider;

    private void OnEnable()
    {
        var enemies = FindObjectsOfType<Enemy>();

        _cube = GetComponentInParent<Environments>();
        _cubeCollider = _cube.GetComponentInParent<BoxCollider>();
        _capsuleCollider = GetComponent<CapsuleCollider>();

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
            _cubeCollider.enabled = true;

            _cube.MoveDown();
        }
        else
        {
            _capsuleCollider.isTrigger = true;
        }
    }
}