using UnityEngine;

public class NextBattle : MonoBehaviour
{
    private Enemy[] _enemies;
    private PlayerMover _playerMover;
    private BoxCollider _boxCollider;
    private CameraObjectsSelector _objectsSelector;

    private int _enemyCount;

    private void OnEnable()
    {
        _boxCollider = GetComponentInChildren<BoxCollider>();
        _playerMover = FindObjectOfType<PlayerMover>();
        _objectsSelector = _playerMover.GetComponentInChildren<CameraObjectsSelector>();
        FillEnemies();
    }

    private void OnDisable()
    {
        foreach (var enemy in _enemies)
        {
            enemy.Died -= OnEmeyDied;
        }
    }

    private void FillEnemies()
    {
        _enemies = GetComponentsInChildren<Enemy>();

        foreach (var enemy in _enemies)
        {
            enemy.Died += OnEmeyDied;
        }

        _enemyCount = _enemies.Length;
    }

    private void OnEmeyDied()
    {
        _enemyCount--;

        if (_enemyCount <= 0)
        {
            _playerMover.enabled = true;
            _boxCollider.enabled = false;
            _objectsSelector.enabled = true;
        }
    }
}