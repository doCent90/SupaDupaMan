using UnityEngine;

public class StartRegDoll : MonoBehaviour
{
    private RegDoll _regDoll;
    private StartGame _startGame;

    private void OnEnable()
    {
        _startGame = FindObjectOfType<StartGame>();
        _regDoll = GetComponentInChildren<RegDoll>();

        _startGame.Started += OnStarted;
    }

    private void OnDisable()
    {
        _startGame.Started -= OnStarted;
    }

    private void OnStarted()
    {
        _regDoll.gameObject.SetActive(false);
    }

    private void OnWinned()
    {
        _regDoll.gameObject.SetActive(true);
    }
}
