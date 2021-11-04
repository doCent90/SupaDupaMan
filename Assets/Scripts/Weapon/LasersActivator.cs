using UnityEngine;
using UnityEngine.Events;

public class LasersActivator : MonoBehaviour
{
    [Header("Lasers")]
    [SerializeField] private int _laserNumber;
    [SerializeField] private GameObject[] _lasers;
    [Header("Settings of Shot")]
    [SerializeField] private Transform _shootPosition1;
    [SerializeField] private Transform _shootPosition2;

    private Enemy[] _enemies;
    private Transform _aimPoint;
    private WallSliced[] _walls;
    private GameObject _laserPrefab1;
    private GameObject _laserPrefab2;
    private LaserRenderer2 _reset1;
    private LaserRenderer2 _reset2;
    private PlayerMover _playerMover;
    private Vector3 _originalPosition1;
    private Vector3 _originalPosition2;

    private bool _isReady = false;

    private const float Delay = 0.6f;

    public event UnityAction<bool> ReadyToAttacked;
    public event UnityAction<bool> Fired;
    public event UnityAction Shoted;

    private void OnEnable()
    {
        _enemies = FindObjectsOfType<Enemy>();
        _walls = FindObjectsOfType<WallSliced>();
        _playerMover = GetComponentInParent<PlayerMover>();

        ReadyToAttacked?.Invoke(true);
        _originalPosition1 = _shootPosition1.localEulerAngles;
        _originalPosition2 = _shootPosition2.localEulerAngles;

        foreach (var wall in _walls)
        {
            wall.ApplyDamage += AimTarget;
            wall.Destroyed += Stop;
        }

        foreach (var enemy in _enemies)
        {
            enemy.ShotPointSeted += AimTarget;
            enemy.Died += Stop;
        }
    }

    private void OnDisable()
    {
        Stop();

        ReadyToAttacked?.Invoke(false);
        Fired?.Invoke(false);

        foreach (var wall in _walls)
        {
            wall.ApplyDamage -= AimTarget;
            wall.Destroyed -= Stop;
        }

        foreach (var enemy in _enemies)
        {
            enemy.ShotPointSeted -= AimTarget;
            enemy.Died -= Stop;
        }
    }

    private void AimTarget(Transform aimPoint)
    {
        _isReady = true;
        _aimPoint = aimPoint;

        Attack();
    }

    private void Attack()
    {   
        if(_isReady)
        {
            RotateShootPosition();
            Activate();
        }

        Deactivate();
    }

    private void Activate()
    {
        if (_isReady)
        {
            _playerMover.enabled = false;
            Destroy(_laserPrefab1);
            Destroy(_laserPrefab2);

            _laserPrefab1 = Instantiate(_lasers[_laserNumber], _shootPosition1.position, _shootPosition1.rotation);
            _laserPrefab2 = Instantiate(_lasers[_laserNumber], _shootPosition2.position, _shootPosition2.rotation);

            _laserPrefab1.transform.LookAt(_aimPoint);
            _laserPrefab2.transform.LookAt(_aimPoint);

            _reset1 = _laserPrefab1.GetComponent<LaserRenderer2>();
            _reset2 = _laserPrefab2.GetComponent<LaserRenderer2>();

            Fired?.Invoke(true);
            Shoted?.Invoke();
        }
    }

    private void Stop()
    {
        if (_reset1)
        {
            _reset1.DisablePrepare();
            _reset2.DisablePrepare();
        }

        _playerMover.enabled = true;
        Fired?.Invoke(false);
        Destroy(_laserPrefab1, Delay);
        Destroy(_laserPrefab2, Delay);
    }

    private void Deactivate()
    {     
        if (!_isReady)
        {
            _playerMover.enabled = true;
            ResetPosition();

            if (_reset1)
            {
                _reset1.DisablePrepare();
                _reset2.DisablePrepare();
            }

            Destroy(_laserPrefab1, Delay);
            Destroy(_laserPrefab2, Delay);

            Fired?.Invoke(false);
        }
    }

    private void RotateShootPosition()
    {
        _shootPosition1.LookAt(_aimPoint);
        _shootPosition2.LookAt(_aimPoint);
    }

    private void ResetPosition()
    {
        _shootPosition1.localPosition = _originalPosition1;
        _shootPosition2.localPosition = _originalPosition2;
    }
}