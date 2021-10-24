using UnityEngine;
using UnityEngine.Events;

public class LaserActivator : MonoBehaviour
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

    public event UnityAction<bool> ReadyToAttacked;
    public event UnityAction<bool> Fired;
    public event UnityAction Shoted;

    private void OnEnable()
    {
        _panelUI = FindObjectOfType<ButtonsUI>();
        _camera = FindObjectOfType<Camera>();
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
        StopLaser();

        ReadyToAttacked?.Invoke(false);
        Fired?.Invoke(false);

        _rayCast.SetActive(false);
        _aim.SetActive(false);
    }

    private void Update()
    {
        if (!_panelUI.IsPanelOpen)
        {
            Attack();
            AimTarget();
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