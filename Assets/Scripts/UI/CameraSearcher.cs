using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class CameraSearcher : MonoBehaviour
{
    private Camera _camera;
    private Canvas _canvas;

    private void OnEnable()
    {
        _camera = FindObjectOfType<Camera>();
        _canvas = GetComponent<Canvas>();

        _canvas.worldCamera = _camera.GetComponent<Camera>();
    }
}
