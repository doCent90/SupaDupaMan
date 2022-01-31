using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerAnimator : MonoBehaviour
{
    private Player _player;
    private GameWin _gameWin;
    private Animator _animator;
    private PlayerHand _handMover;
    private LasersActivator _lasersActivator;

    private const string Attack = "Attack";
    private const string Victory = "Victory";

    private void Awake()
    {
        _player = GetComponent<Player>();
        _handMover = GetComponentInChildren<PlayerHand>();
        _animator = _handMover.GetComponentInChildren<Animator>();
        _lasersActivator = GetComponentInChildren<LasersActivator>();

        _gameWin = _player.GameWin;

        _gameWin.Won += OnGameWin;
        _lasersActivator.Fired += OnAttack;
    }

    private void OnDisable()
    {
        _gameWin.Won -= OnGameWin;
        _lasersActivator.Fired -= OnAttack;
    }

    private void OnAttack(bool isAttack)
    {
        _animator.SetBool(Attack, isAttack);
    }

    private void OnGameWin()
    {
        _animator.SetTrigger(Victory);
    }
}
