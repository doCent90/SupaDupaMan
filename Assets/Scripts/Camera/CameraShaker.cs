using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    private Enemy[] _enemies;
    private LasersActivator _lasers;

    public void Stop()
    {
    }

    private void OnEnable()
    {
        _enemies = FindObjectsOfType<Enemy>();
        _lasers = FindObjectOfType<LasersActivator>();

        _lasers.Fired += TryShake;

        foreach (var enemy in _enemies)
        {
            enemy.Died += OnEmeyDied;
        }
    }

    private void OnDisable()
    {
        _lasers.Fired -= TryShake;

        foreach (var enemy in _enemies)
        {
            enemy.Died -= OnEmeyDied;
        }
    }

    private void TryShake(bool isAttack)
    {
        if (!isAttack)
            Stop();
        else if (isAttack)
            Shake();
    }

    private void Shake()
    {

    }

    private void OnEmeyDied()
    {
        Stop();
    }
}
