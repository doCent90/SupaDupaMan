using System;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class CameraSearcher : MonoBehaviour
{
    [SerializeField] private ComponentHandler _componentHandler;

    private Camera _camera;
    private Canvas _canvas;

    private void OnEnable()
    {
        if (_componentHandler == null)
            throw new InvalidOperationException();

        _camera = _componentHandler.Camera;
        _canvas = GetComponent<Canvas>();

        _canvas.worldCamera = _camera.GetComponent<Camera>();
    }
}
