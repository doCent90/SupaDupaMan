using System;
using UnityEngine;

[RequireComponent(typeof(CoinsSpawner))]
public class Reward : MonoBehaviour
{
    [SerializeField] private ButtonsUI _buttonsUI;

    private Enemy[] _enemies;
    private Enemies _allEnemies;
    private CoinsSpawner _coinsSpawner;

    private void OnEnable()
    {
        if (_buttonsUI == null)
            throw new NullReferenceException(nameof(Reward));

        _coinsSpawner = GetComponent<CoinsSpawner>();
        _allEnemies = _buttonsUI.PlayerRotater.Enemies;
        _enemies = _allEnemies.GetComponentsInChildren<Enemy>();

        foreach (Enemy enemy in _enemies)
        {
            enemy.DiedPosition += OnEnemyDied;
        }
    }

    private void OnDisable()
    {
        foreach (Enemy enemy in _enemies)
        {
            enemy.DiedPosition -= OnEnemyDied;
        }
    }

    private void OnEnemyDied(Transform enemy)
    {
        _coinsSpawner.StartSpawn(enemy);
    }
}
