using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class PlayerMover : MonoBehaviour
{
    private Enemy[] _enemies;

    private const float Duration = 0.5f;
    private const float Distance = 50f;

    public bool IsLastWayPoint { get; private set; }
    public bool HasCurrentPositions { get; private set; }

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
        _enemies = FindObjectsOfType<Enemy>();
    }

    private void LookAtClosetstEnemy()
    {
        Vector3 lookAtPoint = Vector3.forward;

        foreach (var enemy in _enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, transform.position);

            if (distance <= Distance)
                lookAtPoint = enemy.transform.position;
        }

        var tweenRotate = transform.DOLookAt(lookAtPoint, Duration / 2);
        tweenRotate.SetEase(Ease.InOutSine);
    }
}