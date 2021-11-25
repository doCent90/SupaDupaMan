using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class GameWin : MonoBehaviour
{
    private Enemy _enemy;
    private EnemyGigant _enemyGigant;
    private PlayerMover _playerMover;
    private PlayerRotater _playerRotater;
    private ObjectsSelector _objectsSelector;

    public event UnityAction Won;

    private void OnEnable()
    {
        _enemyGigant = FindObjectOfType<EnemyGigant>();
        _enemy = _enemyGigant.GetComponent<Enemy>();
        _playerMover = FindObjectOfType<PlayerMover>();
        _playerRotater = _playerMover.GetComponent<PlayerRotater>();
        _objectsSelector = _playerMover.GetComponentInChildren<ObjectsSelector>();

        _enemy.Died += OnEnemysDied;
    }

    private void OnDisable()
    {
        _enemy.Died -= OnEnemysDied;
        DOTween.Clear();
    }

    private void OnEnemysDied()
    {
        Debug.Log("Win");
        _playerRotater.enabled = false;
        _objectsSelector.gameObject.SetActive(false);

        Won?.Invoke();
    }
}
