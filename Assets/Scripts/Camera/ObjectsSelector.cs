using System;
using UnityEngine;

[RequireComponent(typeof(AimRenderer))]
public class ObjectsSelector : MonoBehaviour
{
    [SerializeField] private LasersActivator _laser;
    [SerializeField] private GameWin _gameWin;

    private AimMain _aimMain;
    private PlayerMover _playerMover;
    private AimRenderer _aimRenderer;
    private SpriteRenderer _spriteRenderer;

    private bool _isFire = false;

    public float MaxLength { get; private set; } = 80f;

    public event Action<Vector3> TargetPointSelected;

    private void OnEnable()
    {
        if (_gameWin == null || _laser == null)
            throw new NullReferenceException(nameof(ObjectsSelector));

        if (_aimRenderer != null)
            _aimRenderer.enabled = true;        
    }

    private void Awake()
    {
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
        _aimRenderer.enabled = false;

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
                TryFly(hit);
                TryDestroyObject(hit);
            }
        }
    }

    private void TryMove(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent(out Platform platform) && platform.enabled)
        {
            TargetPointSelected?.Invoke(hit.point);
            _playerMover.Move(hit.point);
        }
    }

    private void TryFly(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent(out FlyPoint flyPoint) && flyPoint.enabled)
        {
            TargetPointSelected?.Invoke(flyPoint.Position.position);
            _playerMover.Fly(flyPoint.Position, flyPoint.NextPoint);            
        }
    }

    private void TryDestroyObject(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent(out Selectable item))
            item.TakeDamage();
    }

    private RaycastHit GetRaycast()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, MaxLength))
            return hit;
        else
            return hit;
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
    private void OnWonGame()
    {
        enabled = false;
    }
}
