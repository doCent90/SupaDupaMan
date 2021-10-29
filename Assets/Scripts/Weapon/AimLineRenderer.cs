using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class AimLineRenderer : MonoBehaviour
{
    [SerializeField] private GameObject _aimPrefab;

    private LineRenderer _line;
    private PlayerRotater _playerRotater;

    private bool _isReady;

    private const float _maxLength = 50f;

    private void OnEnable()
    {
        _line = GetComponent<LineRenderer>();
        _playerRotater = GetComponentInParent<PlayerRotater>();

        _playerRotater.AimMoved += OnAimMove;

        ResetLaser();
    }

    private void OnDisable()
    {
        _playerRotater.AimMoved -= OnAimMove;
    }

    private void Update()
    {
        DrawLine();
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
                _aimPrefab.GetComponent<SpriteRenderer>().enabled = true;
                _aimPrefab.transform.position = hit.point + hit.normal;
                

                var spriteRenderer = _aimPrefab.GetComponent<SpriteRenderer>();

                if (hit.collider.TryGetComponent(out Enemy enemy))
                {
                    spriteRenderer.color = Color.red;
                    _aimPrefab.transform.rotation = new Quaternion(_aimPrefab.transform.rotation.x, transform.position.y,
                        transform.rotation.z, _aimPrefab.transform.rotation.w);
                }
                else
                {
                    spriteRenderer.color = Color.green;
                    _aimPrefab.transform.rotation = new Quaternion(hit.transform.rotation.x * 90, transform.rotation.y * 90,
                        _aimPrefab.transform.rotation.z, _aimPrefab.transform.rotation.w);
                }
                    
            }
            else
                _aimPrefab.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
            _aimPrefab.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void DrawLine()
    {
        if (_line != null && _isReady)
        {
            _line.SetPosition(0, transform.position);
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, _maxLength))
            {
                _line.SetPosition(1, hit.point);
            }
            else
            {
                var endPos = transform.position + transform.forward * _maxLength;
                _line.SetPosition(1, endPos);
            }

            if (_line.enabled == false && _isReady == true)
            {
                _line.enabled = true;
            }
        }
        else
        {
            _line.enabled = false;
        }
    }

    private void ResetLaser()
    {
        if (_line != null)
        {
            _line.enabled = false;
        }
    }
}
