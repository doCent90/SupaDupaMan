using UnityEngine;
using DG.Tweening;

public class PlayerHandMover : MonoBehaviour
{
    private GameWin _gameWin;

    private const float Distance = -10f;
    private const float Duration = 1f;

    private void OnEnable()
    {
        _gameWin = FindObjectOfType<GameWin>();

        _gameWin.Win += OnWinned;
    }

    private void OnDisable()
    {
        _gameWin.Win += OnWinned;
    }

    private void OnWinned()
    {
        var tweenMove = transform.DOLocalMoveZ(Distance, Duration);
    }
}
