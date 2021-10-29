using UnityEngine;

[RequireComponent(typeof(AimLineRenderer))]
public class RayCastObjectsSelector : MonoBehaviour
{
    [SerializeField] private Lasers _laser;

    private AimLineRenderer _lineRenderer;
    private PlayerMover _playerMover;
    private bool _isFired = false;

    private const float _maxLength = 60f;

    private void OnEnable()
    {
        _playerMover = GetComponentInParent<PlayerMover>();
        _lineRenderer = GetComponent<AimLineRenderer>();

        _lineRenderer.enabled = true;
        _laser.Fired += OnLaserFired;
    }

    private void OnDisable()
    {
        _lineRenderer.enabled = false;
        _laser.Fired += OnLaserFired;
    }

    private void Update()
    {
        SelectPlaceMove();
        SelectEnemy();
    }

    private void OnLaserFired(bool isFired)
    {
        _isFired = isFired;
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
