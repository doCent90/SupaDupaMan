using UnityEngine;

[RequireComponent(typeof(AimLineRenderer))]
public class RayCastObjectsSelector : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _aimSpriteRenderer;
    [SerializeField] private Lasers _laser;

    private AimLineRenderer _aimLineRenderer;
    private LineRenderer _lineRenderer;
    private PlayerMover _playerMover;
    private bool _isFired = false;

    private const float _maxLength = 60f;

    private void OnEnable()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _playerMover = GetComponentInParent<PlayerMover>();
        _aimLineRenderer = GetComponent<AimLineRenderer>();

        _aimLineRenderer.enabled = true;

        _playerMover.Moved += OnMoved;
        _laser.Fired += OnLaserFired;
    }

    private void OnDisable()
    {
        _aimLineRenderer.enabled = false;

        _playerMover.Moved -= OnMoved;
        _laser.Fired -= OnLaserFired;
    }

    private void Update()
    {
        SelectPlaceMove();
        SelectEnemy();
    }

    private void OnLaserFired(bool isFired)
    {
        _isFired = isFired;

        if (isFired)
        {
            _lineRenderer.enabled = false;
            _aimSpriteRenderer.enabled = false;
        }
    }

    private void OnMoved(bool isMoving)
    {
        if(isMoving)
            _aimSpriteRenderer.enabled = false;
    }

    private void SelectPlaceMove()
    {
        if (Input.GetMouseButtonUp(0) && !_isFired)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, _maxLength))
            {
                if (hit.collider != null)
                {
                    if (hit.collider && hit.collider.TryGetComponent(out Platform platform))
                    {
                        hit.collider.TryGetComponent(out Platform point);
                        _playerMover.Move(hit.point);
                    }
                }
            }
        }
    }

    private void SelectEnemy()
    {
        if (Input.GetMouseButtonUp(0) && !_isFired)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, _maxLength))
            {
                if (hit.collider != null)
                {
                    if (hit.collider && hit.collider.TryGetComponent(out Enemy other))
                    {
                        hit.collider.TryGetComponent(out Enemy enemy);
                        enemy.LockTarget();
                    }
                }
            }
        }
    }
}
