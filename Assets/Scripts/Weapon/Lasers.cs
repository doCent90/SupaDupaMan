using UnityEngine;
using UnityEngine.Events;

public class Lasers : MonoBehaviour
{
    [Header("Laser")]
    [SerializeField] private int _laserNumber;
    [SerializeField] private GameObject[] _lasers;
    [SerializeField] private GameObject _rayCast;
    [SerializeField] private GameObject _aim;
    [Header("Settings of Shot")]
    [SerializeField] private Transform _shootPosition;

    private Enemy[] _enemies;
    private OverHeatBar _overHeat;

    private bool _isOverHeated = false;
    private bool _isReady = false;

    private GameObject _instance;
    private Hovl_Laser2 _reset;

    private const float Delay = 0.3f;

    public event UnityAction<bool> ReadyToAttacked;
    public event UnityAction<bool> Fired;
    public event UnityAction Shoted;

    private void OnEnable()
    {
        _overHeat = FindObjectOfType<OverHeatBar>();
        _enemies = FindObjectsOfType<Enemy>();

        _overHeat.OverHeated += ResetAttake;
        ReadyToAttacked?.Invoke(true);

        _rayCast.SetActive(false);
        _aim.SetActive(true);

        foreach (var enemy in _enemies)
        {
            enemy.TargetLocked += AimTarget;
        }
    }

    private void OnDisable()
    {
        _overHeat.OverHeated -= ResetAttake;
        Stop();

        ReadyToAttacked?.Invoke(false);
        Fired?.Invoke(false);

        _rayCast.SetActive(false);
        _aim.SetActive(false);

        foreach (var enemy in _enemies)
        {
            enemy.TargetLocked -= AimTarget;
        }
    }

    private void AimTarget(Transform target)
    {
        enabled = true;
        _shootPosition.LookAt(target);
        _isReady = true;
        Attack();
    }

    private void Attack()
    {   
        if(!_isOverHeated && _isReady)
            Activate();

        Deactivate();
    }

    private void ResetAttake(bool isOverHeated)
    {
        _isOverHeated = isOverHeated;

        if (_isOverHeated)
            Stop();
    }

    private void Activate()
    {
        if (_isReady)
        {
            Destroy(_instance);
            _instance = Instantiate(_lasers[_laserNumber], _rayCast.transform.position, _rayCast.transform.rotation);
            _reset = _instance.GetComponent<Hovl_Laser2>();

            _rayCast.SetActive(true);
            Fired?.Invoke(true);
            Shoted?.Invoke();
        }
    }

    private void Stop()
    {
        if (_reset)
            _reset.DisablePrepare();

        _rayCast.SetActive(false);
        Fired?.Invoke(false);
        Destroy(_instance, Delay);
    }

    private void Deactivate()
    {
        if (!_isReady)
        {
            if (_reset)
                _reset.DisablePrepare();

            Destroy(_instance, Delay);
            Fired?.Invoke(false);
            _rayCast.SetActive(false);
        }
    }
}