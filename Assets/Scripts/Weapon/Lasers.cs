using UnityEngine;
using UnityEngine.Events;

public class Lasers : MonoBehaviour
{
    [Header("Laser")]
    [SerializeField] private int _laserNumber;
    [SerializeField] private GameObject _rayCast;
    [SerializeField] private GameObject[] _lasers;
    [Header("Settings of Shot")]
    [SerializeField] private Transform _shootPosition;

    private Enemy[] _enemies;
    private LaserRenderer2 _reset;
    private GameObject _instance;
    private PlayerMover _playerMover;
    private Vector3 _originalPosition;
    private Transform _aimPoint;

    private bool _isReady = false;

    private const float Delay = 0.6f;

    public event UnityAction<bool> ReadyToAttacked;
    public event UnityAction<bool> Fired;
    public event UnityAction Shoted;

    private void OnEnable()
    {
        _enemies = FindObjectsOfType<Enemy>();
        _playerMover = GetComponentInParent<PlayerMover>();

        ReadyToAttacked?.Invoke(true);

        _rayCast.SetActive(false);
        _originalPosition = _shootPosition.localEulerAngles;

        foreach (var enemy in _enemies)
        {
            enemy.TargetLocked += AimTarget;
            enemy.Died += Stop;
        }
    }

    private void OnDisable()
    {
        Stop();

        ReadyToAttacked?.Invoke(false);
        Fired?.Invoke(false);

        _rayCast.SetActive(false);

        foreach (var enemy in _enemies)
        {
            enemy.TargetLocked -= AimTarget;
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
            Destroy(_instance);
            _instance = Instantiate(_lasers[_laserNumber], _rayCast.transform.position, _rayCast.transform.rotation);
            _instance.transform.LookAt(_aimPoint);
            _reset = _instance.GetComponent<LaserRenderer2>();

            _rayCast.SetActive(true);
            Fired?.Invoke(true);
            Shoted?.Invoke();
        }
    }

    private void Stop()
    {
        if (_reset)
            _reset.DisablePrepare();

        _playerMover.enabled = true;
        _rayCast.SetActive(false);
        Fired?.Invoke(false);
        Destroy(_instance, Delay);
    }

    private void Deactivate()
    {     
        if (!_isReady)
        {
            _playerMover.enabled = true;
            ResetPosition();

            if (_reset)
                _reset.DisablePrepare();

            Destroy(_instance, Delay);
            Fired?.Invoke(false);
            _rayCast.SetActive(false);
        }
    }

    private void RotateShootPosition()
    {
        _shootPosition.LookAt(_aimPoint);        
    }

    private void ResetPosition()
    {
        _shootPosition.localPosition = _originalPosition;
    }
}