using UnityEngine;
using UnityEngine.Events;

public class GameWin : MonoBehaviour
{
    private Enemy[] _enemies;
    private int _enemyCount;

    public event UnityAction Won;

    private void OnEnable()
    {
        _enemies = FindObjectsOfType<Enemy>();
        _enemyCount = _enemies.Length;

        foreach (var enemy in _enemies)
        {
            enemy.Died += OnEnemyDied;
        }
    }

    private void OnDisable()
    {
        foreach (var enemy in _enemies)
        {
            enemy.Died -= OnEnemyDied;
        }
    }

    private void OnEnemyDied()
    {
        int diedEnemyCount = 0;

        foreach (var enemy in _enemies)
        {
            if (!enemy.enabled)
                diedEnemyCount++;
        }

        if(diedEnemyCount == _enemies.Length)
            Won?.Invoke();
    }
}
