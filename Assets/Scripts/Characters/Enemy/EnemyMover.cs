using UnityEngine;
using DG.Tweening;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private Transform _wayPoint1;
    [SerializeField] private Transform _wayPoint2;
    [SerializeField] private bool _isStanding = false;

    private Vector3 _targetPosition;

    private int _direction;
    private bool _onTargetPosition = false;

    private const float Speed = 3f;
    private const float Duration = 0.5f;

    public bool IsStanding => _isStanding;

    private void OnEnable()
    {
        _targetPosition = GetPosition();
        LookAtDirection(_targetPosition);

        _direction = Random.Range(0, 2);
    }

    private void Update()
    {
        if(!_isStanding)
            TryMove();
    }

    private void TryMove()
    {
        if (_onTargetPosition)
        {
            _onTargetPosition = false;
            _targetPosition = GetPosition();

            LookAtDirection(_targetPosition);
        }

        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, Speed * Time.deltaTime);

        if (transform.position == _targetPosition)
            _onTargetPosition = true;
    }

    private Vector3 GetPosition()
    {
        Vector3 targetPosition;
        _direction++;

        if(_direction % 2 == 0)
            targetPosition = _wayPoint1.position;
        else
            targetPosition = _wayPoint2.position;

        return targetPosition;
    }

    private void LookAtDirection(Vector3 target)
    {
        var tweeLookAtPoint = transform.DOLookAt(target, Duration);
    }
}
