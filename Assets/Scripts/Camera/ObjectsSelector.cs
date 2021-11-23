using UnityEngine;

[RequireComponent(typeof(AimRenderer))]
public class ObjectsSelector : MonoBehaviour
{
    [SerializeField] private LasersActivator _laser;

    private SpriteRenderer _spriteRenderer;
    private PlayerMover _playerMover;
    private AimRenderer _aimRenderer;
    private AimMain _aimMain;
    private RaycastHit _hit;

    private bool _isFire = false;

    public readonly float MaxLength = 70f;

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
        if (Input.GetMouseButtonUp(0) && !_isFire)
        {
            SetRaycast();

            if (_hit.collider == null)
                return;
            else
            {
                TryMove(_hit);
                TryDestroyEnemy(_hit);
                TryDestroyWall(_hit);
                TryDestroyObject(_hit);
            }
        }
    }

    private void OnLaserFired(bool isFire)
    {
        _isFire = isFire;

        if (isFire)        
            _spriteRenderer.enabled = false;        
    }

    private void OnMoved(bool isMoving)
    {
        if(isMoving)
            _spriteRenderer.enabled = false;
    }

    private void TryMove(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent(out Platform platform))
            _playerMover.Move(hit.point);
    }

    private void TryDestroyEnemy(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent(out Enemy enemy))
            enemy.TakeDamage();
    }

    private void TryDestroyWall(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent(out WallSlicer2 wall2))
            wall2.TakeDamage();
    }

    private void TryDestroyObject(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent(out ObjectsSclicer objectSliced))
            objectSliced.TakeDamage();
    }

    private void SetRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, MaxLength))
        {
            _hit = hit;
        }
    }
}
