using UnityEngine;
using UnityEngine.Events;

public class WayPointData : MonoBehaviour
{
    private Transform _transform;

    public event UnityAction<Transform> Clicked;

    public void SetTransformWayPoint()
    {
        Clicked?.Invoke(_transform);
    }

    private void OnEnable()
    {
        _transform = transform;
    }
}
