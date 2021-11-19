using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AimAnimator))]
[RequireComponent(typeof(ObjectsSelector))]
public class AimRenderer : MonoBehaviour
{
    private AimMain _mainPrefab;
    private LasersActivator _laser;
    private PlayerRotater _playerRotater;
    private ObjectsSelector _objectsSelector;
    private AimOutOfRangeView _outOfViewPrefab;

    private bool _isReady;
    private float _maxLength;
    private readonly Color _red = Color.red;
    private readonly Color _green = Color.green;

    private const float MinDistanceView = 5f;

    public event UnityAction<bool> MainEnabled;
    public event UnityAction<bool> OutOfRangeEnabled;

    private void OnEnable()
    {
        _laser = FindObjectOfType<LasersActivator>();
        _mainPrefab = GetComponentInChildren<AimMain>();
        _objectsSelector = GetComponent<ObjectsSelector>();
        _playerRotater = GetComponentInParent<PlayerRotater>();
        _outOfViewPrefab = GetComponentInChildren<AimOutOfRangeView>();

        _maxLength = _objectsSelector.MaxLength;
        _playerRotater.Rotate += OnPlayerRotated;
        _laser.Fired += OnLasersFired;
    }

    private void OnDisable()
    {
        _playerRotater.Rotate -= OnPlayerRotated;
        _laser.Fired -= OnLasersFired;
    }

    private void Update()
    {
        Draw();
    }

    private void OnPlayerRotated(bool isReady)
    {
        _isReady = isReady;
    }

    private void OnLasersFired(bool isFire)
    {
        _isReady = true ? isFire = false : isFire = true;
    }

    private void Draw()
    {
        if (_isReady)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, _maxLength))
            {
                SetSwitchMain(isEnable: true);
                SetSwitchOutOfRange(isEnable: false);

                _mainPrefab.transform.position = hit.point + hit.normal;
                var spriteRenderer = _mainPrefab.GetComponent<SpriteRenderer>();

                if (hit.collider.TryGetComponent(out WallSlicer wall) && wall.enabled)
                    RotateAtNormal(hit, spriteRenderer, _red);
                else if (hit.collider.TryGetComponent(out WallSlicer2 wall2) && wall2.enabled)
                    RotateAtNormal(hit, spriteRenderer, _red);
                else if (hit.collider.TryGetComponent(out Enemy enemy))
                    RotateAtLook(spriteRenderer, _red);
                else if (hit.collider.TryGetComponent(out ObjectsSclicer objectSliced))
                    RotateAtLook(spriteRenderer, _red);
                else if(hit.collider.TryGetComponent(out Platform platform))
                    RotateAtNormal(hit, spriteRenderer, _green);
                else if(hit.collider.TryGetComponent(out BaseGround baseGround))
                {
                    SetSwitchMain(isEnable: false);
                    SetSwitchOutOfRange(isEnable: true);
                }
                else
                    SetSwitchMain(isEnable: false);
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
        _mainPrefab.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
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