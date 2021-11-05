using UnityEngine;
using DG.Tweening;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private Transform _wayPoint1;
    [SerializeField] private Transform _wayPoint2;

    private Vector3 _targetPosition;

    private int _direction;
    private bool _onTargetPosition = false;

    private const float Speed = 8f;
    private const int Multiply = 10;
    private const float Duration = 0.2f;

    private void OnEnable()
    {
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
            targetLookPoint = transform.localPosition + _targetPosition * Multiply;
            LookAtPoint(targetLookPoint);
        }

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, _targetPosition, Speed * Time.deltaTime);

        if (transform.localPosition == _targetPosition)
            _onTargetPosition = true;
    }

    private Vector3 GetPosition()
    {
        Vector3 targetPosition;
        _direction++;

        if(_direction % 2 == 0)
            targetPosition = new Vector3(_wayPoint1.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        else
            targetPosition = new Vector3(_wayPoint2.localPosition.x, transform.localPosition.y, transform.localPosition.z);

        return targetPosition;
    }

    private void LookAtPoint(Vector3 target)
    {
        var tweeLookAtPoint = transform.DOLookAt(target, Duration);
        tweeLookAtPoint.SetEase(Ease.InOutSine);
    }
}