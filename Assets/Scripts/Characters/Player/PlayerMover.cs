using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _moveDuration;

    private EnemiesOnPoint _enemiiesOnPoint;
    private WayPoint[] _wayPoints;
    private int _currentPointIndex = 0;

    private Rigidbody _rigidbodySpaceShip;

    private const float RotateSpeed = 50f;

    public bool IsLastWayPoint { get; private set; }
    public bool HasCurrentPositions { get; private set; }

    public event UnityAction LastPointCompleted;
    public event UnityAction<bool> Moved;

    private void OnEnable()
    {
        _rigidbodySpaceShip = GetComponent<Rigidbody>();
        _enemiiesOnPoint = FindObjectOfType<EnemiesOnPoint>();
        FillWayPoints();

        HasCurrentPositions = false;
        Move();
    }

    private void OnDisable()
    {
        Moved?.Invoke(false);
        HasCurrentPositions = false;
    }

    private void FillWayPoints()
    {
        _wayPoints = _enemiiesOnPoint.GetComponentsInChildren<WayPoint>();
    }

    private void ChangeCurrentIndexPosition()
    {
        if (_currentPointIndex == (_wayPoints.Length - 2))
        {
            _currentPointIndex++;
            Move();
            LastPointCompleted?.Invoke();
            HasCurrentPositions = false;
        }
        else
        {
            HasCurrentPositions = true;         
            _currentPointIndex++;
        }
    }

    private void Move()
    {
        if (_currentPointIndex != _wayPoints.Length)
        {
            Moved?.Invoke(true);
            var tweenMove = _rigidbodySpaceShip.DOMove(_wayPoints[_currentPointIndex].transform.position, _moveDuration);
            tweenMove.SetEase(Ease.InOutSine);
            tweenMove.OnComplete(ChangeCurrentIndexPosition);
        }
    }

    private void Rotation(int index)
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _wayPoints[_currentPointIndex].transform.rotation, RotateSpeed * Time.deltaTime);
    }

    private void Update()
    {
        Rotation(_currentPointIndex);
    }
}