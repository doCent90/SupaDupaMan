using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerRotater))]
public class PlayerMover : MonoBehaviour
{
    private Exit _exit;
    private Enemy[] _enemies;
    private GameWin _gameWin;
    private PlayerRotater _rotater;

    private const float Duration = 0.5f;
    private const float Distance = 65f;

    public event UnityAction<bool> Moved;

    public void Move(Vector3 pointToMove)
    {
        Moved?.Invoke(true);

        var tweenMove = transform.DOMove(new Vector3(pointToMove.x, transform.position.y, pointToMove.z), Duration);
        tweenMove.SetEase(Ease.InOutSine);
        tweenMove.OnComplete(LookAtClosetstEnemy);
    }

    private void OnEnable()
    {
        _exit = FindObjectOfType<Exit>();
        _enemies = FindObjectsOfType<Enemy>();
        _gameWin = FindObjectOfType<GameWin>();
        _rotater = GetComponent<PlayerRotater>();

        _rotater.enabled = true;
        _gameWin.Win += MoveFinishView;
    }


    private void OnDisable()
    {
        _rotater.enabled = false;
        _gameWin.Win -= MoveFinishView;
    }

    private void LookAtClosetstEnemy()
    {
        Vector3 lookAtPoint = _exit.transform.position;

        foreach (var enemy in _enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < Distance && enemy.enabled)
            {
                lookAtPoint = enemy.transform.position;
                continue;
            }
        }

        var tweenRotate = transform.DOLookAt(lookAtPoint, Duration / 2);
        tweenRotate.SetEase(Ease.InOutSine);
    }

    private void MoveFinishView()
    {
        var tweeLookAt = transform.DOLookAt(_exit.transform.position, Duration);
        var tweenMove = transform.DOMove(_gameWin.transform.position, Duration);

        enabled = false;
    }
}