using UnityEngine;
using DG.Tweening;
using System;

public class PlayerRotater : MonoBehaviour
{
    private Enemy[] _enemies;

    private const string MouseX = "Mouse X";
    private const string MouseY = "Mouse Y";
    private const float Duration = 0.35f;
    private const float Distance = 100f;
    private const float Delay = 0.6f;
    private const int Multiply = 2;

    public event Action Started;
    public event Action<bool> Rotated;

    public void LookAtClosetstEnemy()
    {
        foreach (var enemy in _enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < Distance && enemy.enabled)
            {
                Vector3 lookAtPoint;
                lookAtPoint = enemy.transform.position;

                var tweenRotate = transform.DOLookAt(lookAtPoint, Duration);
                continue;
            }
        }
    }

    public void LookAtFlyPoint(Transform point)
    {
        var tweenRotate = transform.DOLookAt(point.position, Duration * Multiply).SetDelay(Delay);        
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
            float y;

            x = Input.GetAxis(MouseX);
            y = Input.GetAxis(MouseY);

            transform.localEulerAngles += new Vector3(y, x, 0);

            Started?.Invoke();
            Rotated?.Invoke(true);
        }
        else
            Rotated?.Invoke(false);
    }
}
