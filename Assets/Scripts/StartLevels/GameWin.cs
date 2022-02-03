using System;
using UnityEngine;

[RequireComponent(typeof(AdSettings))]
public class GameWin : MonoBehaviour
{
    [SerializeField] private Enemies _allEnemies;

    private Enemy[] _enemies;
    private AdSettings _adSettings;

    public event Action Won;

    private void OnEnable()
    {
        if (_allEnemies == null)
            throw new NullReferenceException(nameof(GameWin));

        _adSettings = GetComponent<AdSettings>();
        _enemies = _allEnemies.GetComponentsInChildren<Enemy>();

        foreach (Enemy enemy in _enemies)
        {
            enemy.Died += OnEnemyDied;
        }
    }

    private void OnDisable()
    {
        foreach (Enemy enemy in _enemies)
        {
            enemy.Died -= OnEnemyDied;
        }
    }

    private void OnEnemyDied()
    {
        int diedEnemyCount = 0;

        foreach (Enemy enemy in _enemies)
        {
            if (!enemy.enabled)
                diedEnemyCount++;
        }

        if (diedEnemyCount == _enemies.Length)
            OnGameWon();
    }

    private void OnGameWon()
    {
        Won?.Invoke();
        _adSettings.ShowInterstitial();
    }
}
