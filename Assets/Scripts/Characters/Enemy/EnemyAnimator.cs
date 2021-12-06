using System;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(EnemyMover))]
public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private StartGame _startGame;

    private Enemy _enemy;
    private Animator _animator;
    private EnemyMover _enemyMover;

    private const string Run = "Run";
    private const string TakeDamage = "TakeDamage";

    private void Awake()
    {
        if (_startGame == null)
            throw new InvalidOperationException();

        _enemy = GetComponent<Enemy>();
        _animator = GetComponent<Animator>();
        _enemyMover = GetComponent<EnemyMover>();

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
        if(!_enemyMover.IsStanding)
            _animator.SetTrigger(Run);
    }

    private void OnDied()
    {
        _animator.SetBool(TakeDamage, false);
    }
}
