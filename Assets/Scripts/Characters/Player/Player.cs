using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerRotater))]
public class Player : MonoBehaviour
{
    [SerializeField] private ComponentHandler _componentHandler;

    private GameWin _gameWin;
    private PlayerMover _mover;
    private StartGame _startGame;
    private PlayerRotater _rotater;
    private EndLevelFyPoint _endLevelFyPoint;
    private ObjectsSelector _objectsSelector;

    private void Awake()
    {
        if (_componentHandler == null)
            throw new InvalidOperationException();

        _gameWin = _componentHandler.GameWin;
        _endLevelFyPoint = _componentHandler.EndLevelFyPoint;

        _mover = GetComponent<PlayerMover>();
        _rotater = GetComponent<PlayerRotater>();
        _startGame = _componentHandler.StartGame;
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
