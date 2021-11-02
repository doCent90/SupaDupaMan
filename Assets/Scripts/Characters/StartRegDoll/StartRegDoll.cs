using UnityEngine;

public class StartRegDoll : MonoBehaviour
{
    [SerializeField] private GameObject _regDoll;

    private GameWin _gameWin;
    private StartGame _startGame;
    private StartRegDollMover _dollMover;

    private void OnEnable()
    {
        _gameWin = FindObjectOfType<GameWin>();
        _startGame = FindObjectOfType<StartGame>();
        _dollMover = GetComponent<StartRegDollMover>();

        _gameWin.Win += OnWinned;
        _startGame.Started += OnStarted;
    }

    private void OnDisable()
    {
        _gameWin.Win -= OnWinned;
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
