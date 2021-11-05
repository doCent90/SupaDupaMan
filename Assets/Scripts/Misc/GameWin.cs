using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class GameWin : MonoBehaviour
{
    private Enemy _enemy;
    private YouWin _winText;
    private EnemyGigant _enemyGigant;
    private PlayerMover _playerMover;
    private PlayerRotater _playerRotater;
    private ObjectsSelector _objectsSelector;

    public event UnityAction Win;

    private void OnEnable()
    {
        _winText = FindObjectOfType<YouWin>();
        _enemyGigant = FindObjectOfType<EnemyGigant>();
        _enemy = _enemyGigant.GetComponent<Enemy>();
        _playerMover = FindObjectOfType<PlayerMover>();
        _playerRotater = _playerMover.GetComponent<PlayerRotater>();
        _objectsSelector = _playerMover.GetComponentInChildren<ObjectsSelector>();

        _winText.gameObject.SetActive(false);
        _enemy.Died += OnEnemyDied;
    }

    private void OnDisable()
    {
        _enemy.Died -= OnEnemyDied;
        DOTween.Clear();
    }

    private void OnEnemyDied()
    {
        _playerRotater.enabled = false;
        _winText.gameObject.SetActive(true);
        _objectsSelector.gameObject.SetActive(false);

        Win?.Invoke();
    }
}
