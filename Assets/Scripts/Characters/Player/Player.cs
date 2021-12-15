using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerRotater))]
public class Player : MonoBehaviour
{
    [SerializeField] private GameWin _gameWin;
    [SerializeField] private StartGame _startGame;
    [SerializeField] private EndLevelFyPoint _endLevelFyPoint;

    private PlayerMover _mover;
    private PlayerRotater _rotater;
    private ObjectsSelector _objectsSelector;

    private void Awake()
    {
        if (_gameWin == null || _startGame == null || _endLevelFyPoint == null)
            throw new InvalidOperationException();

        _mover = GetComponent<PlayerMover>();
        _rotater = GetComponent<PlayerRotater>();
        _objectsSelector = GetComponentInChildren<ObjectsSelector>();

        _startGame.Started += OnStarted;
        _gameWin.Won += OnGameWon;
    }

    private void OnDisable()
    {
        _startGame.Started -= OnStarted;
        _gameWin.Won += OnGameWon;
    }

    private void OnStarted()
    {
        _mover.enabled = true;
        _rotater.enabled = true;
        _objectsSelector.enabled = true;
    }

    private void OnGameWon()
    {
        _mover.LookAtFinish(_endLevelFyPoint.transform);
        _objectsSelector.enabled = false;
        _rotater.enabled = false;
    }
}
