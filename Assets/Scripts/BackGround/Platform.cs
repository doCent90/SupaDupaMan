using UnityEngine;
using UnityEngine.Events;

public class Platform : MonoBehaviour
{
    private Transform _transform;

    public event UnityAction<Transform> OnPlatformClicked;

    public void SetTransformPoint()
    {
        OnPlatformClicked?.Invoke(_transform);
    }

    private void OnEnable()
    {
        _transform = transform;
    }
}
