using System;
using UnityEngine;

[RequireComponent(typeof(ObjectsSelector))]
public class AimRenderer : MonoBehaviour
{
    [SerializeField] private LasersActivator _laser;

    private AimMain _mainPrefab;
    private PlayerRotater _playerRotater;
    private ObjectsSelector _objectsSelector;
    private AimOutOfRangeView _outOfViewPrefab;

    private bool _isReady;
    private float _maxLength;
    private readonly Color _red = Color.red;
    private readonly Color _green = Color.green;

    private const float Scale = 0.4f;
    private const float MinDistanceView = 5f;

    public event Action<bool> MainEnabled;
    public event Action<bool> OutOfRangeEnabled;

    private void Awake()
    {
        if (_laser == null)
            throw new NullReferenceException(nameof(AimRenderer));

        _mainPrefab = GetComponentInChildren<AimMain>();
        _objectsSelector = GetComponent<ObjectsSelector>();
        _playerRotater = GetComponentInParent<PlayerRotater>();
        _outOfViewPrefab = GetComponentInChildren<AimOutOfRangeView>();

        _maxLength = _objectsSelector.MaxLength;
        _playerRotater.Rotated += OnPlayerRotated;
        _laser.Fired += OnLasersFired;
    }

    private void OnDisable()
    {
        _playerRotater.Rotated -= OnPlayerRotated;
        _laser.Fired -= OnLasersFired;
    }

    private void Update()
    {
        TryDraw();
    }

    private void OnPlayerRotated(bool isReady)
    {
        _isReady = isReady;
    }

    private void OnLasersFired(bool isFire)
    {
        _isReady = true ? isFire = false : isFire = true;
    }

    private void TryDraw()
    {
        if (_isReady)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, _maxLength))
            {
                SetSwitchMain(isEnable: true);
                SetSwitchOutOfRange(isEnable: false);

                _mainPrefab.transform.position = hit.point + hit.normal;
                var spriteRenderer = _mainPrefab.GetComponent<SpriteRenderer>();

                if (hit.collider.TryGetComponent(out WallSlicer wall) && wall.enabled)
                {
                    RotateAtNormal(hit, spriteRenderer, _red);
                }
                else if (hit.collider.TryGetComponent(out Enemy enemy) || hit.collider.TryGetComponent(out ObjectsSlicer objectSliced))
                {
                    RotateAtLook(spriteRenderer, _red);
                }
                else if (hit.collider.TryGetComponent(out Platform platform) || (hit.collider.TryGetComponent(out FlyPoint flyPoint) && flyPoint.enabled))
                {
                    RotateAtNormal(hit, spriteRenderer, _green);
                }
                else
                {
                    SetSwitchMain(isEnable: false);
                    SetSwitchOutOfRange(isEnable: true);
                }
            }
            else
            {
                SetSwitchMain(isEnable: false);
                SetSwitchOutOfRange(isEnable: true);
            }
        }
        else
        {
            SetSwitchMain(isEnable: false);
            SetSwitchOutOfRange(isEnable: false);
        }
    }

    private void RotateAtLook(SpriteRenderer spriteRenderer, Color color)
    {
        spriteRenderer.color = color;
        _mainPrefab.transform.rotation = transform.rotation;
        _mainPrefab.transform.localPosition = new Vector3(0, 0, MinDistanceView);
    }

    private void RotateAtNormal(RaycastHit hit, SpriteRenderer spriteRenderer, Color color)
    {
        spriteRenderer.color = color;
        _mainPrefab.transform.rotation = Quaternion.FromToRotation(_mainPrefab.transform.forward, hit.normal) * _mainPrefab.transform.rotation;
        _mainPrefab.transform.localScale = new Vector3(Scale, Scale, Scale);
    }

    private void SetSwitchMain(bool isEnable)
    {
        _mainPrefab.GetComponent<SpriteRenderer>().enabled = isEnable;
        MainEnabled?.Invoke(isEnable);
    }

    private void SetSwitchOutOfRange(bool isEnable)
    {
        _outOfViewPrefab.GetComponent<SpriteRenderer>().enabled = isEnable;
        OutOfRangeEnabled?.Invoke(isEnable);
    }
}
