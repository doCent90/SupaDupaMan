using UnityEngine;

[RequireComponent(typeof(AimRenderer))]
public class ObjectsSelector : MonoBehaviour
{
    [SerializeField] private LasersActivator _laser;

    private SpriteRenderer _spriteRenderer;
    private PlayerMover _playerMover;
    private AimRenderer _aimRenderer;
    private AimMain _aimMain;
    private GameWin _gameWin;

    private bool _isFire = false;

    public readonly float MaxLength = 60f;

    private void OnEnable()
    {
        _gameWin = FindObjectOfType<GameWin>();
        _aimRenderer = GetComponent<AimRenderer>();
        _aimMain = GetComponentInChildren<AimMain>();
        _playerMover = GetComponentInParent<PlayerMover>();
        _spriteRenderer = _aimMain.GetComponent<SpriteRenderer>();

        _aimRenderer.enabled = true;

        _playerMover.Moved += OnMoved;
        _laser.Fired += OnLaserFired;
        _gameWin.Won += OnWonGame;
    }

    private void OnDisable()
    {
        _aimMain.gameObject.SetActive(false);

        _playerMover.Moved -= OnMoved;
        _laser.Fired -= OnLaserFired;
        _gameWin.Won -= OnWonGame;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && !_isFire)
        {
            RaycastHit hit = GetRaycast();

            if (hit.collider == null)
                return;
            else
            {
                TryMove(hit);
                TryDestroyEnemy(hit);
                TryDestroyWall(hit);
                TryDestroyObject(hit);
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

    private RaycastHit GetRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, MaxLength))
        {
            return hit;
        }
        else
            return hit;
    }

    private void OnWonGame()
    {
        enabled = false;
    }
}
