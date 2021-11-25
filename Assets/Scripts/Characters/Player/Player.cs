using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerRotater))]
public class Player : MonoBehaviour
{
    private GameWin _gameWin;
    private PlayerMover _mover;
    private StartGame _startGame;
    private PlayerRotater _rotater;
    private ObjectsSelector _objectsSelector;

    private void OnEnable()
    {
        _mover = GetComponent<PlayerMover>();
        _gameWin = FindObjectOfType<GameWin>();
        _rotater = GetComponent<PlayerRotater>();
        _startGame = FindObjectOfType<StartGame>();
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
        _mover.LookAtFinish(_gameWin.transform);
        _objectsSelector.enabled = false;
        _rotater.enabled = false;
    }
}
