using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class Player : MonoBehaviour
{
    private GameOver _gameOver;
    private PlayerMover _mover;
    private StartGame _startGame;
    private PlayerRotater _rotater;
    private RayCastObjectsSelector _pointSelector;

    private void OnEnable()
    {
        _mover = GetComponent<PlayerMover>();
        _rotater = GetComponent<PlayerRotater>();
        _gameOver = FindObjectOfType<GameOver>();
        _startGame = FindObjectOfType<StartGame>();
        _pointSelector = GetComponentInChildren<RayCastObjectsSelector>();

        _startGame.Started += OnStart;
        _gameOver.Defeated += OnDefeatGame;
    }

    private void OnDisable()
    {
        _startGame.Started -= OnStart;
        _gameOver.Defeated -= OnDefeatGame;
    }

    private void OnStart()
    {
        _mover.enabled = true;
        _rotater.enabled = true;
        _pointSelector.enabled = true;
    }

    private void OnDefeatGame()
    {
        enabled = false;

        _mover.enabled = false;
        _rotater.enabled = false;
        _pointSelector.enabled = false;
    }
}
