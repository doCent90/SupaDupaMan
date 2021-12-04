using UnityEngine;
using DG.Tweening;
using System;

[RequireComponent(typeof(ButtonsUI))]
public class ButtonsAnimator : MonoBehaviour
{
    [SerializeField] private GameWin _gameWin;
    [SerializeField] private RectTransform _start;
    [SerializeField] private RectTransform _continue;

    private ButtonsUI _buttonsUI;
    private Vector3 _backPosition;
    private Vector3 _startPosition;

    private const float Duration = 1f;

    private void Awake()
    {
        if (_gameWin == null || _start == null || _continue == null)
            throw new InvalidOperationException();

        _buttonsUI = GetComponent<ButtonsUI>();

        _startPosition = _start.localPosition;
        _backPosition = _continue.localPosition;

        _gameWin.Won += OnGameWon;
        _buttonsUI.StartButtonClicked += OnStartClicked;
        _buttonsUI.ContinueButtonClicked += OnContinueClicked;

        Move();
    }

    private void OnDisable()
    {
        _gameWin.Won -= OnGameWon;
        _buttonsUI.StartButtonClicked -= OnStartClicked;
        _buttonsUI.ContinueButtonClicked -= OnContinueClicked;
    }

    private void Move()
    {
        _start.localPosition = _continue.localPosition;
        var tweeMove = _start.DOLocalMoveY(_startPosition.y, Duration);
        tweeMove.SetEase(Ease.InOutBack);
    }

    private void OnStartClicked()
    {
        var tweeMove = _start.DOLocalMoveY(_backPosition.y, Duration);
        tweeMove.SetEase(Ease.InOutBack);
        tweeMove.OnComplete(OnStartButtonAnimated);
    }

    private void OnContinueClicked()
    {
        var tweeMove = _continue.DOLocalMoveY(_backPosition.y, Duration);
        tweeMove.SetEase(Ease.InOutBack);
        tweeMove.OnComplete(OnContinueButtonAnimated);
    }

    private void OnGameWon()
    {
        var tweeMove = _continue.DOLocalMoveY(_startPosition.y, Duration);
        tweeMove.SetEase(Ease.InOutBack);
    }

    private void OnContinueButtonAnimated()
    {
        _buttonsUI.NextLevel();
    }

    private void OnStartButtonAnimated()
    {
        _buttonsUI.StartCurrentLevel();
    }
}
