using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class Player : MonoBehaviour
{
    private PlayerMover _mover;
    private GameOver _gameOver;
    private StartGame _startGame;

    private void OnEnable()
    {
        _mover = GetComponent<PlayerMover>();
        _gameOver = FindObjectOfType<GameOver>();
        _startGame = FindObjectOfType<StartGame>();

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
    }

    private void OnDefeatGame()
    {
        enabled = false;
        _mover.enabled = false;
    }
}
