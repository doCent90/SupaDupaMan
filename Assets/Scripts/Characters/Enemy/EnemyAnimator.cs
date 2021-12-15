using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(EnemyMover))]
public class EnemyAnimator : MonoBehaviour
{
    private Enemy _enemy;
    private Animator _animator;
    private StartGame _startGame;
    private EnemyMover _enemyMover;

    private const string Run = "Run";
    private const string TakeDamage = "TakeDamage";

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        _animator = GetComponent<Animator>();
        _enemyMover = GetComponent<EnemyMover>();
        _startGame = _enemy.StartGame;

        _startGame.Started += OnStarted;
        _enemy.Damaged += OnDamageTaked;
    }

    private void OnDisable()
    {
        _startGame.Started -= OnStarted;
        _enemy.Damaged -= OnDamageTaked;
    }

    private void OnDamageTaked()
    {
        _animator.SetTrigger(TakeDamage);
    }

    private void OnStarted()
    {
        if(!_enemyMover.IsStanding)
            _animator.SetTrigger(Run);
    }
}
