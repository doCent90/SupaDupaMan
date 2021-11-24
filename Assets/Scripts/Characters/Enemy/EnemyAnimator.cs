using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    private Enemy _enemy;
    private GameOver _gameOver;
    private Animator _animator;
    private StartGame _startGame;

    private const string Run = "Run";
    private const string Die = "Die";
    private const string Victory = "Victory";
    private const string TakeDamage = "TakeDamage";

    private void OnEnable()
    {
        _enemy = GetComponent<Enemy>();
        _animator = GetComponent<Animator>();
        _gameOver = FindObjectOfType<GameOver>();
        _startGame = FindObjectOfType<StartGame>();

        _gameOver.Defeated += OnGameOver;
        _startGame.Started += OnStarted;
        _enemy.Damaged += OnDamageTaked;
        _enemy.Died += OnDied;
    }

    private void OnDisable()
    {
        _gameOver.Defeated -= OnGameOver;
        _startGame.Started -= OnStarted;
        _enemy.Damaged -= OnDamageTaked;
        _enemy.Died -= OnDied;
    }

    private void OnDamageTaked()
    {
        _animator.SetBool(TakeDamage, true);
    }

    private void Celebrate()
    {
        _animator.SetBool(Run, false);
        _animator.SetTrigger(Victory);
    }

    private void OnStarted()
    {
        _animator.SetBool(Run, true);
    }

    private void OnDied()
    {
        _animator.SetBool(Run, false);
        _animator.SetBool(TakeDamage, false);
        _animator.SetTrigger(Die);
    }

    private void OnGameOver()
    {
        if (_enemy.enabled)
             Celebrate();
    }
}
