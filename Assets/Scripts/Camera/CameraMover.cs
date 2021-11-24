using UnityEngine;

public class CameraMover : MonoBehaviour
{
    private StartGame _startGame;

    private void OnEnable()
    {
        _startGame = FindObjectOfType<StartGame>();
        _startGame.Started += OnStarted;
    }

    private void OnDisable()
    {
        _startGame.Started -= OnStarted;
    }

    private void OnStarted()
    {
    }
}
