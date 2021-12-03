using UnityEngine;
using System.Linq;

[RequireComponent(typeof(CapsuleCollider))]
public class EnemyGigant : MonoBehaviour
{
    private Enemy[] _smallAliveEnemies;
    private CapsuleCollider _capsuleCollider;

    private void Start()
    {
        var enemies = FindObjectsOfType<Enemy>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _capsuleCollider.enabled = false;

        _smallAliveEnemies = enemies.Where(enemy => enemy.IsGigant == false).ToArray();

        foreach (var enemy in _smallAliveEnemies)
        {
            enemy.Died += SetActiveCollider;
        }
    }

    private void OnDisable()
    {
        foreach (var enemy in _smallAliveEnemies)
        {
            enemy.Died -= SetActiveCollider;
        }
    }

    private void SetActiveCollider()
    {
        var diedEnemies = _smallAliveEnemies.Where(enemy => enemy.enabled == false).ToArray();

        if (diedEnemies.Length == _smallAliveEnemies.Length)
            _capsuleCollider.enabled = true;        
    }
}
