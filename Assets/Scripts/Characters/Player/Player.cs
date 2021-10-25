using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMover))]
public class Player : MonoBehaviour
{
    private PlayerMover _mover;
    private GameOver _gameOver;

    public event UnityAction Started;

    private void OnEnable()
    {
        Started?.Invoke();

        _mover = GetComponent<PlayerMover>();
        _gameOver = FindObjectOfType<GameOver>();

        _gameOver.Defeated += StopGame;
    }

    private void OnDisable()
    {
        _gameOver.Defeated -= StopGame;
    }

    private void StopGame()
    {
        enabled = false;
        _mover.enabled = false;
    }
}
