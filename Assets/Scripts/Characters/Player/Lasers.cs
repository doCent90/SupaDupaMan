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

    private ButtonsUI _panelUI;
    private GameObject _instance;
    private Hovl_Laser2 _laserPrefab;

    private const float Delay = 0.3f;
    private const float MaxLength = 40f;

    public event UnityAction<bool> ReadyToAttacked;
    public event UnityAction<bool> Fired;
    public event UnityAction Shoted;

    private void OnEnable()
    {
        _panelUI = FindObjectOfType<ButtonsUI>();
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
        StopLaser();

        ReadyToAttacked?.Invoke(false);
        Fired?.Invoke(false);

        _rayCast.SetActive(false);
        _aim.SetActive(false);

        foreach (var enemy in _enemies)
        {
            enemy.TargetLocked -= AimTarget;
            enemy.Died -= DisableLasers;
        }
    }

    private void Update()
    {
        if (!_panelUI.IsPanelOpen)
        {
            Attack();
        }
    }

    private void Attack()
    {   
        if(!_isOverHeated)
            ActivateLaser();

        DeactivateLaser();
    }

    private void ResetAttake(bool isOverHeated)
    {

        _isOverHeated = isOverHeated;

        if (_isOverHeated)
            StopLaser();
    }

    private void ActivateLaser()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(_instance);
            _instance = Instantiate(_lasers[_laserNumber], _rayCast.transform.position, _rayCast.transform.rotation);
            _instance.transform.parent = _shootPosition;
            _laserPrefab = _instance.GetComponent<Hovl_Laser2>();

            _rayCast.SetActive(true);
            Fired?.Invoke(true);
            Shoted?.Invoke();
        }
    }

    private void StopLaser()
    {
        if (_laserPrefab)
            _laserPrefab.DisablePrepare();

        _rayCast.SetActive(false);
        Fired?.Invoke(false);
        Destroy(_instance, Delay);
    }

    private void DeactivateLaser()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (_laserPrefab)
                _laserPrefab.DisablePrepare();

            _rayCast.SetActive(false);
            Fired?.Invoke(false);
            Destroy(_instance, Delay);
        }
    }

    private void AimTarget(Transform target)
    {
        enabled = true;
        _shootPosition.LookAt(target);
    }

    private void DisableLasers()
    {
        enabled = false;
    }
}