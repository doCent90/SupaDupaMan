using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Lasers : MonoBehaviour
{
    [Header("Laser")]
    [SerializeField] private int _laserNumber;
    [SerializeField] private GameObject _rayCast;
    [SerializeField] private GameObject[] _lasers;
    [Header("Settings of Shot")]
    [SerializeField] private Transform _shootPosition;

    private Enemy[] _enemies;
    private Hovl_Laser2 _reset;
    private GameObject _instance;
    private PlayerMover _playerMover;
    private Vector3 _originalPosition;

    private bool _isOverHeated = false;
    private bool _isReady = false;

    private const float Duration = 0.8f;
    private const float Delay = 0.3f;
    private const float Euler = 2f;

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
            enemy.Died += Stop;
        }
    }

    private void AimTarget(Transform target)
    {
        _isReady = true;

        _shootPosition.LookAt(target);
        Attack();
    }

    private void Attack()
    {   
        if(!_isOverHeated && _isReady)
            Activate();

        Deactivate();
    }

    private void Activate()
    {
        if (_isReady)
        {
            _playerMover.enabled = false;
            Destroy(_instance);
            _instance = Instantiate(_lasers[_laserNumber], _rayCast.transform.position, _rayCast.transform.rotation);
            _reset = _instance.GetComponent<Hovl_Laser2>();

            RotateShootPosition();
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

            if (_reset)
                _reset.DisablePrepare();

            Destroy(_instance, Delay);
            Fired?.Invoke(false);
            _rayCast.SetActive(false);
        }
    }

    private void RotateShootPosition()
    {
        var tweeRotate = _shootPosition.DOLocalRotate
            (new Vector3(-Euler, _shootPosition.localEulerAngles.y, _shootPosition.localEulerAngles.z), Duration);
        tweeRotate.SetEase(Ease.InOutSine);
        var tweenRotateLaser = _instance.transform.DOLocalRotate
            (new Vector3(-Euler, _instance.transform.localEulerAngles.y, _instance.transform.localEulerAngles.z), Duration);

        tweeRotate.OnComplete(ResetPosition);
    }

    private void ResetPosition()
    {
        _shootPosition.localPosition = _originalPosition;
    }
}