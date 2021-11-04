using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(LineRenderer))]
public class AimRenderer : MonoBehaviour
{
    private AimMain _aimMainPrefab;
    private PlayerRotater _playerRotater;
    private AimOutOfRange _aimOutOfRangePrefab;

    private bool _isReady;

    private const float _maxLength = 50f;

    public event UnityAction<bool> MainAimActivated;
    public event UnityAction<bool> OutOfRangeAimActivated;

    private void OnEnable()
    {
        _aimMainPrefab = GetComponentInChildren<AimMain>();
        _playerRotater = GetComponentInParent<PlayerRotater>();
        _aimOutOfRangePrefab = GetComponentInChildren<AimOutOfRange>();
        _playerRotater.AimMoved += OnAimMove;
    }

    private void OnDisable()
    {
        _playerRotater.AimMoved -= OnAimMove;
    }

    private void Update()
    {
        DrawAim();
    }

    private void OnAimMove(bool isReady)
    {
        _isReady = isReady;
    }

    private void DrawAim()
    {
        if (_isReady)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, _maxLength))
            {
                MainAimActivate(true);
                OutOfRangeAimActivate(false);

                _aimMainPrefab.transform.position = hit.point + hit.normal;
                var spriteRenderer = _aimMainPrefab.GetComponent<SpriteRenderer>();

                if (hit.collider.TryGetComponent(out Enemy enemy) || hit.collider.TryGetComponent(out WallSliced sclicer))
                {
                    spriteRenderer.color = Color.red;
                    _aimMainPrefab.transform.rotation = transform.rotation;
                }
                else if(hit.collider.TryGetComponent(out Platform platform))
                {
                    spriteRenderer.color = Color.green;
                    _aimMainPrefab.transform.rotation = Quaternion.FromToRotation(_aimMainPrefab.transform.forward, hit.normal) * _aimMainPrefab.transform.rotation;
                }                    
            }
            else
            {
                MainAimActivate(false);
                OutOfRangeAimActivate(true);
            }
        }
        else
        {
            MainAimActivate(false);
            OutOfRangeAimActivate(false);
        }
    }

    private void MainAimActivate(bool isActivate)
    {
        _aimMainPrefab.GetComponent<SpriteRenderer>().enabled = isActivate;
        MainAimActivated?.Invoke(isActivate);
    }

    private void OutOfRangeAimActivate(bool isActivate)
    {
        _aimOutOfRangePrefab.GetComponent<SpriteRenderer>().enabled = isActivate;
        OutOfRangeAimActivated?.Invoke(isActivate);
    }
}
