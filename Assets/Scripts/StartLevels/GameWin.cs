using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class GameWin : MonoBehaviour
{
    private Enemy _enemy;
    private EnemyGigant _enemyGigant;

    public event UnityAction Won;

    private void OnEnable()
    {
        _enemyGigant = FindObjectOfType<EnemyGigant>();
        _enemy = _enemyGigant.GetComponent<Enemy>();

        _enemy.Died += OnEnemysDied;
    }

    private void OnDisable()
    {
        _enemy.Died -= OnEnemysDied;
    }

    private void OnEnemysDied()
    {
        Won?.Invoke();
    }
}
