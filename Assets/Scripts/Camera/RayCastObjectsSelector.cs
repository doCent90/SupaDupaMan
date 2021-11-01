using UnityEngine;

[RequireComponent(typeof(AimRenderer))]
public class RayCastObjectsSelector : MonoBehaviour
{
    [SerializeField] private Lasers _laser;

    private SpriteRenderer _spriteRenderer;
    private PlayerMover _playerMover;
    private AimRenderer _aimRenderer;
    private AimMain _aimMain;
    private bool _isFired = false;

    private const float _maxLength = 60f;

    private void OnEnable()
    {
        _aimRenderer = GetComponent<AimRenderer>();
        _aimMain = GetComponentInChildren<AimMain>();
        _playerMover = GetComponentInParent<PlayerMover>();
        _spriteRenderer = _aimMain.GetComponent<SpriteRenderer>();

        _aimRenderer.enabled = true;
        _playerMover.Moved += OnMoved;
        _laser.Fired += OnLaserFired;
    }

    private void OnDisable()
    {
        _aimMain.gameObject.SetActive(false);

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
            _spriteRenderer.enabled = false;
        }
    }

    private void OnMoved(bool isMoving)
    {
        if(isMoving)
            _spriteRenderer.enabled = false;
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
                        enemy.TakeDamage();
                    }
                }
            }
        }
    }
}
