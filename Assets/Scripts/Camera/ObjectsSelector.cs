using UnityEngine;

[RequireComponent(typeof(AimRenderer))]
public class ObjectsSelector : MonoBehaviour
{
    [SerializeField] private LasersActivator _laser;

    private SpriteRenderer _spriteRenderer;
    private PlayerMover _playerMover;
    private AimRenderer _aimRenderer;
    private AimMain _aimMain;
    private bool _isFire = false;

    public readonly float MaxLength = 65f;

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
        TryMove();
        TryDestroyEnemy();
        TryDestroyWall();
        TryDestroyObject();
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

    private void TryMove()
    {
        if (Input.GetMouseButtonUp(0) && !_isFire)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, MaxLength))
            {
                if (hit.collider != null)
                {
                    if (hit.collider.TryGetComponent(out Platform platform))
                    {
                        _playerMover.Move(hit.point);
                    }
                }
            }
        }
    }

    private void TryDestroyEnemy()
    {
        if (Input.GetMouseButtonUp(0) && !_isFire)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, MaxLength))
            {
                if (hit.collider != null)
                {
                    if (hit.collider.TryGetComponent(out Enemy enemy))
                    {
                        enemy.TakeDamage();
                    }
                }
            }
        }
    }

    private void TryDestroyWall()
    {
        if (Input.GetMouseButtonUp(0) && !_isFire)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, MaxLength))
            {
                if (hit.collider != null)
                {
                    if (hit.collider.TryGetComponent(out WallSlicer wall))
                    {
                        wall.TakeDamage();
                    }
                }
            }
        }
    }

    private void TryDestroyObject()
    {
        if (Input.GetMouseButtonUp(0) && !_isFire)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, MaxLength))
            {
                if (hit.collider != null)
                {
                    if (hit.collider.TryGetComponent(out ObjectsSclicer objectSliced))
                    {
                        objectSliced.TakeDamage();
                    }
                }
            }
        }
    }
}