using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerRotater))]
public class PlayerMover : MonoBehaviour
{
    private Transform _finishPoint;
    private PlayerRotater _rotater;

    private const float DurationMoveFinish = 6f;
    private const float Duration = 0.5f;

    public event UnityAction<bool> Moved;

    public void Move(Vector3 pointToMove)
    {
        Moved?.Invoke(true);

        var tweenMove = transform.DOMove(new Vector3(pointToMove.x, transform.position.y, pointToMove.z), Duration);
        tweenMove.SetEase(Ease.InOutSine);
        tweenMove.OnComplete(LookAtClosetstEnemy);
    }

    public void LookAtFinish(Transform finishPosition)
    {
        _finishPoint = finishPosition;
        var tweeLookAt = transform.DOLookAt(_finishPoint.position, Duration * 3);
        tweeLookAt.OnComplete(MoveFinishPoint);
    }

    private void OnEnable()
    {
        _rotater = GetComponent<PlayerRotater>();

        _rotater.enabled = true;
    }


    private void OnDisable()
    {
        _rotater.enabled = false;
    }

    private void LookAtClosetstEnemy()
    {
        _rotater.LookAtClosetstEnemy();
    }

    private void MoveFinishPoint()
    {
        var tweenMove = transform.DOMove(_finishPoint.position, DurationMoveFinish);

        enabled = false;
    }
}
