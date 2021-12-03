using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ComponentHandler))]
public class GameWin : MonoBehaviour
{
    private Enemy _enemy;
    private EnemyGigant _enemyGigant;
    private ComponentHandler _componentHandler;

    public event UnityAction Won;

    private void OnEnable()
    {
        _componentHandler = GetComponent<ComponentHandler>();
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
