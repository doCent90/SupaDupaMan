using UnityEngine;

public class CameraObjectsSelector : MonoBehaviour
{
    private Camera _camera;

    private void OnEnable()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    if(hit.collider)
                        hit.collider.GetComponentInParent<WayPointData>().SetTransformWayPoint();
                }
            }
        }
    }
}
