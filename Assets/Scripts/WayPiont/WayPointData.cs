using UnityEngine;
using UnityEngine.Events;

public class WayPointData : MonoBehaviour
{
    private Transform _transform;
    private Environments _platform;
    private Transform _cubePosition;

    public event UnityAction<Transform, Transform> Clicked;

    public void SetTransformWayPoint()
    {
        Clicked?.Invoke(_transform, _cubePosition);
        _platform.GetComponent<BoxCollider>().enabled = false;
    }

    private void OnEnable()
    {
        _platform = GetComponentInChildren<Environments>();
        _transform = transform;

        if(GetComponentInChildren<AimPoint>() != null)
            _cubePosition = GetComponentInChildren<AimPoint>().transform;
    }
}
