using System;
using UnityEngine;

public class DamagePointWallSlicerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rangeY;
    [SerializeField] private float _rangeX;

    private LaserRenderer2[] _laserRenderers;
    private Vector3 _targetPosition;
    private Vector3 _startPosition;

    private bool _onTargetPosition = false;
    private bool _isReady = false;
    private int _direction;

    private const float UpEdgeWall = -0.5f;

    public void Init()
    {
        _laserRenderers = FindObjectsOfType<LaserRenderer2>();
        _isReady = true;
    }

    private void OnEnable()
    {
        if (_speed < 0)
            throw new ArgumentOutOfRangeException(nameof(DamagePointWallSlicerMover));

        _direction = 0;
        _startPosition = transform.localPosition;
        _targetPosition = _startPosition;
    }

    private void OnDisable()
    {
        transform.localPosition = _startPosition;
    }

    private void Update()
    {
        if (_isReady == false)
        {
            return;
        }
        else
        {
            TryMove();
            RotateLasers();
        }
    }

    private void RotateLasers()
    {
        foreach (LaserRenderer2 laser in _laserRenderers)
        {
            laser.transform.LookAt(transform.position);
        }
    }

    private void TryMove()
    {
        if (_onTargetPosition)
        {
            _direction++;
            _onTargetPosition = false;

            if (_direction % 2 == 0)
                _targetPosition = new Vector3(-_rangeX, UnityEngine.Random.Range(_rangeY, UpEdgeWall), 0);
            else
                _targetPosition = new Vector3(_rangeX, UnityEngine.Random.Range(_rangeY, UpEdgeWall), 0);

            _direction = _direction > 100 ? _direction = 0 : _direction;
        }

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, _targetPosition, _speed * Time.deltaTime);

        if (transform.localPosition == _targetPosition)
            _onTargetPosition = true;
    }
}
