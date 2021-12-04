using System;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private StartGame _startGame;

    private Enemy _enemy;
    private Animator _animator;

    private const string Run = "Run";
    private const string TakeDamage = "TakeDamage";

    private void Awake()
    {
        if (_startGame == null)
            throw new InvalidOperationException();

        _enemy = GetComponent<Enemy>();
        _animator = GetComponent<Animator>();

        _startGame.Started += OnStarted;
        _enemy.Damaged += OnDamageTaked;
        _enemy.Died += OnDied;
    }

    private void OnDisable()
    {
        _startGame.Started -= OnStarted;
        _enemy.Damaged -= OnDamageTaked;
        _enemy.Died -= OnDied;
    }

    private void OnDamageTaked()
    {
        _animator.SetBool(TakeDamage, true);
    }

    private void OnStarted()
    {
        _animator.SetBool(Run, true);
    }

    private void OnDied()
    {
        _animator.SetBool(Run, false);
        _animator.SetBool(TakeDamage, false);
    }
}
