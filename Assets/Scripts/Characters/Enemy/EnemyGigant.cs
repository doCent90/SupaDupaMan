using UnityEngine;
using System.Linq;
using UnityEngine.Events;

[RequireComponent(typeof(CapsuleCollider))]
public class EnemyGigant : MonoBehaviour
{
    private Enemy[] _smallAliveEnemies;
    private CapsuleCollider _capsuleCollider;

    public event UnityAction Activated;

    private void OnEnable()
    {
        var enemies = FindObjectsOfType<Enemy>();
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
            _capsuleCollider.enabled = true;
            Activated?.Invoke();
        }
        else
        {
            _capsuleCollider.enabled = false;
        }
    }
}