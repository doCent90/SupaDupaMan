using UnityEngine;

public class ObjectsSelector : MonoBehaviour
{
    [SerializeField] private Lasers _laser;

    private Camera _camera;

    private bool _isFired = false;

    private void OnEnable()
    {
        _camera = GetComponent<Camera>();
        _laser.Fired += OnLaserFired;
    }

    private void OnDisable()
    {
        _laser.Fired += OnLaserFired;
    }

    private void Update()
    {
        SelectPlatform();
        SelectEnemy();
    }

    private void OnLaserFired(bool isFired)
    {
        _isFired = isFired;
    }

    private void SelectPlatform()
    {
        if (Input.GetMouseButtonDown(0) && !_isFired)
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    if (hit.collider && hit.collider.TryGetComponent(out WayPoint wayPoint))
                        hit.collider.GetComponentInParent<WayPointData>().SetTransformWayPoint();
                }
            }
        }
    }

    private void SelectEnemy()
    {
        if (Input.GetMouseButtonDown(0) && !_isFired)
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    if (hit.collider && hit.collider.TryGetComponent(out Enemy enemys))
                    {
                        hit.collider.TryGetComponent(out Enemy enemy);
                        enemy.LockTarget();
                    }
                }
            }
        }
    }
}
