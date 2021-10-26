using UnityEngine;
using DG.Tweening;

public class EnemyMover : MonoBehaviour
{
    private Vector3 _targetPosition;
    private bool _onTargetPosition = false;

    private const float Range = 0.4f;
    private const float Duration = 0.2f;
    private const float Speed = 0.4f;
    private const int Multiply = 100;

    private void OnEnable()
    {
        _targetPosition = GetPosition();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 targetLookPoint;

        if (_onTargetPosition)
        {
            _onTargetPosition = false;
            _targetPosition = GetPosition();
            targetLookPoint = transform.position + new Vector3(_targetPosition.x, 0, _targetPosition.z) * Multiply;
            LookAtPoint(targetLookPoint);
        }

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, _targetPosition, Speed * Time.deltaTime);

        if (transform.localPosition == _targetPosition)
            _onTargetPosition = true;
    }

    private Vector3 GetPosition()
    {
        float x;
        float z;

        x = Random.Range(-Range, Range);
        z = Random.Range(-Range, Range);

        Vector3 targetPosition = new Vector3(x, transform.localPosition.y, z);
        return targetPosition;
    }

    private void LookAtPoint(Vector3 target)
    {
        var tweeLookAtPoint = transform.DOLookAt(target, Duration);
        tweeLookAtPoint.SetEase(Ease.InOutSine);
    }
}