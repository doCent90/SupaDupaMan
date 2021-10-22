using UnityEngine;
using UnityEngine.Events;

public class AttackState : StatePlayer
{
    [Header("Laser")]
    [SerializeField] private int _laserNumber;
    [SerializeField] private GameObject[] _lasers;
    [SerializeField] private GameObject _rayCast;
    [SerializeField] private GameObject _aim;
    [Header("Settings of Shot")]
    [SerializeField] private Transform _shootPosition;

    private LaserPlace _laserPlace;
    private OverHeatBar _overHeat;

    private bool _isOverHeated = false;

    private Camera _camera;
    private ButtonsUI _panelUI;
    private GameObject _instance;
    private Hovl_Laser2 _laserPrefab;

    private const float Delay = 0.3f;
    private const float MaxLength = 40f;
    private const float StartPositionCirlceGunPlace = 180f;

    public event UnityAction<bool> ReadyToAttacked;
    public event UnityAction<bool> Fired;
    public event UnityAction Shoted;

    private void OnEnable()
    {
        _panelUI = FindObjectOfType<ButtonsUI>();
        _camera = GetComponentInChildren<Camera>();
        _overHeat = FindObjectOfType<OverHeatBar>();
        _laserPlace = GetComponentInChildren<LaserPlace>();

        _overHeat.OverHeated += ResetAttake;
        ReadyToAttacked?.Invoke(true);

        _rayCast.SetActive(false);
        _aim.SetActive(true);
    }

    private void OnDisable()
    {
        _overHeat.OverHeated -= ResetAttake;
        DeactivateLaser();

        ReadyToAttacked?.Invoke(false);
        Fired?.Invoke(false);

        _rayCast.SetActive(false);
        _aim.SetActive(false);
    }

    private void Update()
    {
        AimTarget();

        if (Input.GetMouseButtonDown(0) && !_isOverHeated && !_panelUI.IsPanelOpen)
        {
            Attack(true);
        }
    }

    private void Attack(bool isShooting)
    {
        Fired?.Invoke(isShooting);

        if (isShooting)
        {
            ActivateLaser();
            Shoted?.Invoke();
            _rayCast.SetActive(true);
        }
        else
        {
            DeactivateLaser();
            _rayCast.SetActive(false);
        }
    }

    private void ResetAttake(bool isOverHeated)
    {
        _isOverHeated = isOverHeated;
    }

    private void ActivateLaser()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(_instance);
            _instance = Instantiate(_lasers[_laserNumber], _rayCast.transform.position, _rayCast.transform.rotation);

            _instance.transform.parent = _shootPosition;
            _laserPrefab = _instance.GetComponent<Hovl_Laser2>();
        }
    }

    private void DeactivateLaser()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (_laserPrefab)
                _laserPrefab.DisablePrepare();

            if (_instance != null)
                _instance.transform.parent = _shootPosition;

            Destroy(_instance, Delay);
        }
    }

    private void AimTarget()
    {
        RaycastHit hit;
        var mousePos = Input.mousePosition;
        var rayMouse = _camera.ScreenPointToRay(mousePos);
        
        if (Physics.Raycast(rayMouse.origin, rayMouse.direction, out hit, MaxLength))
        {
            RotateToMouseDirection(_laserPlace.gameObject, hit.point);
        }
        else
        {
            var pos = rayMouse.GetPoint(MaxLength);
            RotateToMouseDirection(_laserPlace.gameObject, pos);
        }
    }

    private void RotateToMouseDirection(GameObject obj, Vector3 destination)
    {
        var direction = destination - obj.transform.position;
        var rotation = Quaternion.LookRotation(direction);
        obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, rotation, 1);
    }
}