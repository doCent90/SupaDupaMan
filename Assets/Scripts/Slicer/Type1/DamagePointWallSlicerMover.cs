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

    private const float Positive = 1f;
    private const float Negative = -1f;

    public void Init()
    {
        _laserRenderers = FindObjectsOfType<LaserRenderer2>();
        _isReady = true;
    }

    private void OnEnable()
    {
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
        if (!_isReady)
            return;
        else
        {
            Move();
            RotateLasers();
        }
    }

    private void RotateLasers()
    {
        foreach (var laser in _laserRenderers)
        {
            laser.transform.LookAt(transform.position);
        }
    }

    private void Move()
    {
        if (_onTargetPosition)
        {
            _direction++;
            _onTargetPosition = false;

            if (_direction % 2 == 0)
                _targetPosition = new Vector3(-_rangeX, _rangeY * Random.Range(Negative, Positive), 0);
            else
                _targetPosition = new Vector3(_rangeX, _rangeY * Random.Range(Negative, Positive), 0);

            _direction = _direction > 100 ? _direction = 0 : _direction;
        }

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, _targetPosition, _speed * Time.deltaTime);

        if (transform.localPosition == _targetPosition)
            _onTargetPosition = true;
    }
}
