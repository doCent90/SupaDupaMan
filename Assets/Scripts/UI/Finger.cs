using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class Finger : MonoBehaviour
{
    [SerializeField] private ButtonsUI _buttonsUI;

    private Animator _animator;
    private StartGame _startGame;
    private PlayerRotater _playerRotater;
    private SpriteRenderer _spriteRenderer;

    private void OnEnable()
    {
        if (_buttonsUI == null)
            throw new NullReferenceException(nameof(Finger));

        _startGame = _buttonsUI.StartGame;
        _playerRotater = _buttonsUI.PlayerRotater;

        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _startGame.Started += OnGameStarted;
        _playerRotater.Started += OnFirstAction;
    }

    private void OnDisable()
    {
        _startGame.Started -= OnGameStarted;
        _playerRotater.Started -= OnFirstAction;
    }

    private void OnGameStarted()
    {
        SetActivate(isActive: true);
    }

    private void OnFirstAction()
    {
        SetActivate(isActive: false);
    }

    private void SetActivate(bool isActive)
    {
        _animator.enabled = isActive;
        _spriteRenderer.enabled = isActive;
    }
}
