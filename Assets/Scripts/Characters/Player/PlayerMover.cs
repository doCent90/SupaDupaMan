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
    private const float DistanceToGround = 35f;

    public event UnityAction<bool> Moved;

    public void Move(Vector3 pointToMove)
    {
        float distanceToGround;
        distanceToGround = pointToMove.y + DistanceToGround;

        Moved?.Invoke(true);

        var tweenMove = transform.DOMove(new Vector3(pointToMove.x, distanceToGround, pointToMove.z), Duration);
        tweenMove.SetEase(Ease.InOutSine);
        tweenMove.OnComplete(LookAtClosetstEnemy);
    }

    public void Fly(Vector3 point)
    {
        Moved?.Invoke(true);

        var tweenMove = transform.DOMove(point, Duration);
        tweenMove.SetEase(Ease.InOutSine);
    }

    public void LookAtFinish(Transform finishPosition)
    {
        _finishPoint = finishPosition;
        var tweeLookAt = transform.DOLookAt(_finishPoint.position, Duration * 3).SetDelay(Duration * 2);
        tweeLookAt.SetEase(Ease.InOutSine).OnComplete(MoveFinishPoint);
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
        tweenMove.SetEase(Ease.InOutSine);

        enabled = false;
    }
}
