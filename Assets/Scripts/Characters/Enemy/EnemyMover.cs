using UnityEngine;
using DG.Tweening;
using System;

[RequireComponent(typeof(FreeWayChecker))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private Transform _wayPoint1;
    [SerializeField] private Transform _wayPoint2;
    [SerializeField] private bool _isStanding = false;
    [SerializeField] private bool _isFollowing = false;

    private Player _player;
    private Vector3 _targetPosition;
    private FreeWayChecker _freeWayChecker;

    private int _direction;
    private bool _onTargetPosition = false;

    private const float Speed = 3f;
    private const float Duration = 0.5f;

    public bool IsStanding => _isStanding;

    private void OnEnable()
    {
        if (_wayPoint1 == null || _wayPoint2 == null)
            throw new NullReferenceException(nameof(EnemyMover));

        _player = FindObjectOfType<Player>();
        _freeWayChecker = GetComponent<FreeWayChecker>();

        if (_isStanding == false && _isFollowing == false)
        {
            _targetPosition = GetPosition();
            LookAtDirectionSlowly(_targetPosition);

            _direction = UnityEngine.Random.Range(0, 2);
        }
        else if (_isStanding == false && _isFollowing)
        {
            _targetPosition = new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z);
        }

        _freeWayChecker.EdgePlatformReached += DisableFollowing;
    }

    private void OnDisable()
    {
        _freeWayChecker.EdgePlatformReached -= DisableFollowing;
    }

    private void Update()
    {
        if (_isStanding == false)
            TryMove();
    }

    private void TryMove()
    {
        if (_onTargetPosition && _isFollowing == false)
        {
            _onTargetPosition = false;

            _targetPosition = GetPosition();

            LookAtDirectionSlowly(_targetPosition);
        }
        else if(_isStanding == false && _isFollowing)
        {
            LookAtDirectionFaster(_targetPosition);
            _targetPosition = new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z);
        }

        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, Speed * Time.deltaTime);

        if (transform.position == _targetPosition)
        {
            _onTargetPosition = true;
            _isFollowing = false;
        }
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

    private void DisableFollowing()
    {
        _isFollowing = false;
        _targetPosition = GetPosition();
        LookAtDirectionSlowly(_targetPosition);
    }

    private void LookAtDirectionSlowly(Vector3 target)
    {
        var tweeLookAtPoint = transform.DOLookAt(target, Duration);
    }

    private void LookAtDirectionFaster(Vector3 target)
    {
        transform.LookAt(target);
    }
}
