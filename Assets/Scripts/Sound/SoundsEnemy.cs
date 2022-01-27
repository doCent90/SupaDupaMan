using System;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class SoundsEnemy : MonoBehaviour
{
    [SerializeField] private AudioSource[] _effects;

    private Enemy _enemy;

    private void OnEnable()
    {
        if (_effects == null)
            throw new NullReferenceException(nameof(SoundsEnemy));

        _enemy = GetComponent<Enemy>();
        _enemy.Died += OnDied;
    }

    private void OnDisable()
    {
        _enemy.Died -= OnDied;
    }

    private void OnDied()
    {
        foreach (AudioSource sound in _effects)
        {
            sound.Play();
        }
    }
}
