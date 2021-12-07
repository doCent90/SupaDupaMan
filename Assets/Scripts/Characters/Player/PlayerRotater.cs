using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using System;

public class PlayerRotater : MonoBehaviour
{
    private Enemy[] _enemies;

    private Vector3 _originalPosition;
    private float _axisY;

    private const string MouseX = "Mouse X";
    private const string MouseY = "Mouse Y";
    private const float Duration = 0.25f;
    private const float Distance = 65f;
    private const float Range = 60f;
    private const float Multiply = 3f;

    public event UnityAction<bool> Rotated;

    public void LookAtClosetstEnemy()
    {
        foreach (var enemy in _enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < Distance && enemy.enabled)
            {
                Vector3 lookAtPoint;
                lookAtPoint = enemy.transform.position;
                _originalPosition = transform.localEulerAngles;

                var tweenRotate = transform.DOLookAt(lookAtPoint, Duration);
                continue;
            }
        }
    }

    private void Awake()
    {
        _enemies = FindObjectsOfType<Enemy>();
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        if (Input.GetMouseButton(0))
        {
            float x;
            x = Input.GetAxis(MouseX);
            _axisY += Input.GetAxis(MouseY);
            _axisY = Mathf.Clamp(_axisY, -Range, Range);

            var euler = transform.localEulerAngles;
            euler.x = _axisY;

            transform.localEulerAngles = euler;
            transform.localEulerAngles += new Vector3(0, x, 0);

            Rotated?.Invoke(true);
        }
        else
            Rotated?.Invoke(false);
    }
}
