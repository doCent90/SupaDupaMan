using UnityEngine;
using UnityEngine.Events;

public class WayPointData : MonoBehaviour
{
    private Transform _transform;
    private Transform _aimPosition;

    public event UnityAction<Transform, Transform> Clicked;

    public void SetTransformWayPoint()
    {
        Clicked?.Invoke(_transform, _aimPosition);
    }

    private void OnEnable()
    {
        _transform = transform;
    }
}
