using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    private Enemy _enemy;
    private Animator _animator;
    private StartGame _startGame;

    private const string Run = "Run";
    private const string TakeDamage = "TakeDamage";

    private void OnEnable()
    {
        _enemy = GetComponent<Enemy>();
        _animator = GetComponent<Animator>();
        _startGame = FindObjectOfType<StartGame>();

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
