using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private ComponentHandler _componentHandler;

    private GameWin _gameWin;
    private Animator _animator;
    private PlayerHand _handMover;
    private LasersActivator _lasersActivator;

    private const string Attack = "Attack";
    private const string Victory = "Victory";

    private void Awake()
    {
        if (_componentHandler == null)
            throw new InvalidOperationException();

        _gameWin = _componentHandler.GameWin;
        _handMover = GetComponentInChildren<PlayerHand>();
        _animator = _handMover.GetComponentInChildren<Animator>();
        _lasersActivator = GetComponentInChildren<LasersActivator>();

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
