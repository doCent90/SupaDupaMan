using System;
using UnityEngine;

[RequireComponent(typeof(CoinsSpawner))]
public class Reward : MonoBehaviour
{
    [SerializeField] private ComponentHandler _componentHandler;

    private Enemy _enemy;
    private EnemyGigant _enemyGigant;
    private CoinsSpawner _coinsSpawner;

    private void Awake()
    {
        if (_componentHandler == null)
            throw new InvalidOperationException();

        _enemyGigant = _componentHandler.Enemies.GetComponentInChildren<EnemyGigant>();
        _coinsSpawner = GetComponent<CoinsSpawner>();
        _enemy = _enemyGigant.GetComponent<Enemy>();

        _enemy.Died += OnEnemyDied;
    }

    private void OnDisable()
    {
        _enemy.Died -= OnEnemyDied;
    }

    private void OnEnemyDied()
    {
        _coinsSpawner.StartSpawn(_enemyGigant);
    }
}
