using System;
using UnityEngine;

[RequireComponent(typeof(CoinsSpawner))]
public class Reward : MonoBehaviour
{
    private Enemy[] _enemies;
    private CoinsSpawner _coinsSpawner;

    private void Awake()
    {
        _enemies = FindObjectsOfType<Enemy>();
        _coinsSpawner = GetComponent<CoinsSpawner>();

        foreach (var enemy in _enemies)
        {
            enemy.DiedPosition += OnEnemyDied;
        }
    }

    private void OnDisable()
    {
        foreach (var enemy in _enemies)
        {
            enemy.DiedPosition -= OnEnemyDied;
        }
    }

    private void OnEnemyDied(Transform enemy)
    {
        _coinsSpawner.StartSpawn(enemy);
    }
}
