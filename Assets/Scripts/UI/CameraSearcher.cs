using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class CameraSearcher : MonoBehaviour
{
    private CameraMover _cameraMover;
    private Canvas _canvas;

    private void OnEnable()
    {
        _cameraMover = FindObjectOfType<CameraMover>();
        _canvas = GetComponent<Canvas>();

        _canvas.worldCamera = _cameraMover.GetComponent<Camera>();
    }
}
