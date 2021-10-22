using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(AttackState))]
[RequireComponent(typeof(StateMachinePlayer))]
[RequireComponent(typeof(TargetDieTransition))]
public class Player : MonoBehaviour
{
    private PlayerMover _mover;
    //private GameOverField _gameOver;
    private AttackState _attackState;
    private TargetDieTransition _targetDie;
    private StateMachinePlayer _stateMachine;

    private int _countEnemies = 0;

    public event UnityAction Started;

    private void OnEnable()
    {
        Started?.Invoke();

        _mover = GetComponent<PlayerMover>();
        _attackState = GetComponent<AttackState>();
        //_gameOver = FindObjectOfType<GameOverField>();
        _targetDie = GetComponent<TargetDieTransition>();
        _stateMachine = GetComponent<StateMachinePlayer>();

        //_gameOver.Defeated += StopGame;
        _stateMachine.enabled = true;
    }

    private void OnDisable()
    {
        //_gameOver.Defeated -= StopGame;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
            

        if (collision.collider.TryGetComponent(out Enemy enemy))
            _countEnemies++;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Enemy enemy))
        {
            _countEnemies--;
            OnTargetsDie();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Enemy enemy))
        {
            _countEnemies++;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.TryGetComponent(out Enemy enemy))
        {
            _countEnemies--;
            OnTargetsDie();
        }
    }
    private void OnTargetsDie()
    {
        if (_countEnemies <= 0)
            _targetDie.OnTargetDied();
    }

    private void StopGame()
    {
        enabled = false;
        _mover.enabled = false;
        _stateMachine.enabled = false;
        _attackState.enabled = false;
        _targetDie.enabled = false;
    }
}
