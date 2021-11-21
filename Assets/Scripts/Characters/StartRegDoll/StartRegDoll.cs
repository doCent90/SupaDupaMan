using UnityEngine;

[RequireComponent(typeof(StartRegDollMover))]
public class StartRegDoll : MonoBehaviour
{
    private RegDoll _regDoll;
    private GameWin _gameWin;
    private StartGame _startGame;
    private StartRegDollMover _dollMover;

    private void OnEnable()
    {
        _gameWin = FindObjectOfType<GameWin>();
        _startGame = FindObjectOfType<StartGame>();
        _regDoll = GetComponentInChildren<RegDoll>();
        _dollMover = GetComponent<StartRegDollMover>();

        _gameWin.Won += OnWinned;
        _startGame.Started += OnStarted;
    }

    private void OnDisable()
    {
        _gameWin.Won -= OnWinned;
        _startGame.Started -= OnStarted;
    }

    private void OnStarted()
    {
        _regDoll.gameObject.SetActive(false);
    }

    private void OnWinned()
    {
        _regDoll.gameObject.SetActive(true);
        _dollMover.enabled = true;
    }
}
