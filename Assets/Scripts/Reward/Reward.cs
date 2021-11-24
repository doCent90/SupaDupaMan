using UnityEngine;

[RequireComponent(typeof(CoinsSpawner))]
public class Reward : MonoBehaviour
{
    private EnemyGigant _enemyGigant;
    private CoinsSpawner _coinsSpawner;
    private Enemy _enemy;

    private void OnEnable()
    {
        _enemyGigant = FindObjectOfType<EnemyGigant>();
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
