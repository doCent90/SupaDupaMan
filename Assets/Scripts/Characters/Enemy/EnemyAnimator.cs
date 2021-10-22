using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    private Animator _animator;
    private Enemy _enemy;
    private Player _player;

    private const string Run = "Run";
    private const string Die = "Die";
    private const string Victory = "Victory";

    public void Celebrate()
    {
        _animator.SetBool(Run, false);
        _animator.SetTrigger(Victory);
    }

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _enemy = GetComponent<Enemy>();
        _player = FindObjectOfType<Player>();

        _player.Started += OnStarted;
        _enemy.Died += OnDied;
    }

    private void OnDisable()
    {
        _player.Started -= OnStarted;
        _enemy.Died -= OnDied;
    }

    private void OnStarted()
    {
        _animator.SetBool(Run, true);
    }

    private void OnDied()
    {
        _animator.SetBool(Run, false);
        _animator.SetTrigger(Die);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out GameOverField gameOverField))
        {
            EnemyAnimator[] enemies = FindObjectsOfType<EnemyAnimator>();

            foreach (var enemy in enemies)
            {
                if(enemy.GetComponent<Enemy>().enabled)
                    enemy.Celebrate();
            }
        }
    }
}
