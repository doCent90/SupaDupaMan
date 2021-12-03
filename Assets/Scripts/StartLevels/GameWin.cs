using UnityEngine;
using UnityEngine.Events;

public class GameWin : MonoBehaviour
{
    [SerializeField] private ComponentHandler _componentHandler;

    private Enemy _enemy;
    private EnemyGigant _enemyGigant;

    public event UnityAction Won;

    private void OnEnable()
    {
        _enemyGigant = _componentHandler.Enemies.GetComponentInChildren<EnemyGigant>();
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
