using UnityEngine;
using DG.Tweening;
using System;

[RequireComponent(typeof(PlayerRotater))]
public class PlayerMover : MonoBehaviour
{
    private Transform _finishPoint;
    private PlayerRotater _rotater;

    private const float Duration = 0.5f;
    private const float DurationMoveFinish = 6f;
    private const float DistanceToGround = 35f;

    public event Action<bool> Moved;

    public void Move(Vector3 pointToMove)
    {
        float distanceToGround;
        distanceToGround = pointToMove.y + DistanceToGround;

        Moved?.Invoke(true);

        var tweenMove = transform.DOMove(new Vector3(pointToMove.x, distanceToGround, pointToMove.z), Duration);
        tweenMove.SetEase(Ease.InOutSine);
        tweenMove.OnComplete(LookAtClosetstEnemy);
    }

    public void Fly(Transform pointToMove, Transform flyPoint)
    {
        Moved?.Invoke(true);

        var tweenMove = transform.DOMove(pointToMove.position, Duration);
        tweenMove.SetEase(Ease.InOutSine);

        LookAtNextFlyPoint(flyPoint);
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

    private void LookAtNextFlyPoint(Transform transform)
    {
        _rotater.LookAtFlyPoint(transform);
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
