using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using System;

public class PlayerRotater : MonoBehaviour
{
    [SerializeField] private ComponentHandler _componentHandler;

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
                tweenRotate.SetEase(Ease.InOutSine);
                tweenRotate.OnComplete(SetOriginalAngle);

                continue;
            }
        }
    }

    private void Awake()
    {
        if (_componentHandler == null)
            throw new InvalidOperationException();

        _enemies = _componentHandler.Enemies.GetComponentsInChildren<Enemy>();
    }

    private void Update()
    {
        Rotate();
    }

    private void SetOriginalAngle()
    {
        Vector3 target = new Vector3(_originalPosition.x, transform.localEulerAngles.y, transform.localEulerAngles.z);
        transform.DORotate(target, Duration * Multiply);
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
