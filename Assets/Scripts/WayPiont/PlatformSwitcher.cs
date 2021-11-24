using UnityEngine;
using UnityEngine.Events;

public class PlatformSwitcher : MonoBehaviour
{
    private Enemy[] _enemies;
    private PlayerMover _playerMover;
    private BoxCollider _boxCollider;
    private CameraAnimator _cameraAnimator;
    private CameraObjectsSelector _objectsSelector;

    private int _enemyCount;

    public event UnityAction Switched;

    private void OnEnable()
    {
        _playerMover = FindObjectOfType<PlayerMover>();
        _boxCollider = GetComponentInChildren<BoxCollider>();
        _cameraAnimator = FindObjectOfType<CameraAnimator>();
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
            _cameraAnimator.StopShake();

            _playerMover.enabled = true;
            _boxCollider.enabled = false;
            _objectsSelector.enabled = true;

            Switched?.Invoke();
        }
    }
}