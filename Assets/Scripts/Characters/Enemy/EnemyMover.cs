using UnityEngine;
using DG.Tweening;

public class EnemyMover : MonoBehaviour
{
    private Vector3 _targetPosition;
    private Vector3 _originalPosition;

    private int _direction;
    private bool _onTargetPosition = false;

    private const float RangeMax = 20f;
    private const float RangeMin = 10f;
    private const float Duration = 0.2f;
    private const float Speed = 4f;
    private const int Multiply = 100;

    private void OnEnable()
    {
        _originalPosition = transform.localPosition;
        _targetPosition = GetPosition();

        _direction = Random.Range(0, 2);
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
            targetLookPoint = transform.localPosition + new Vector3(_targetPosition.x, 0, _targetPosition.z) * Multiply;
            LookAtPoint(targetLookPoint);
        }

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, _targetPosition, Speed * Time.deltaTime);

        if (transform.localPosition == _targetPosition)
            _onTargetPosition = true;
    }

    private Vector3 GetPosition()
    {
        _direction++;
        float x;

        if(_direction % 2 == 0)
            x = Random.Range(RangeMin, RangeMax);
        else
            x = Random.Range(RangeMin, RangeMax) * -1;

        Vector3 targetPosition = new Vector3(x, _originalPosition.y, _originalPosition.z);
        return targetPosition;
    }

    private void LookAtPoint(Vector3 target)
    {
        var tweeLookAtPoint = transform.DOLookAt(target, Duration);
        tweeLookAtPoint.SetEase(Ease.InOutSine);
    }
}