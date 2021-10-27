using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class PlayerMover : MonoBehaviour
{
    private Transform _aimPoint;
    private WayPointData[] _wayPoints;

    private const float Duration = 0.5f;
    private const string MouseX = "Mouse X";
    private const string MouseY = "Mouse Y";

    public bool IsLastWayPoint { get; private set; }
    public bool HasCurrentPositions { get; private set; }

    public event UnityAction<bool> Moved;

    public Transform GetWayPoinPosition(Transform wayPoint)
    {
        return wayPoint;
    }

    private void OnEnable()
    {
        _wayPoints = FindObjectsOfType<WayPointData>();

        foreach (var point in _wayPoints)
        {
            point.Clicked += Move;
        }
    }

    private void OnDisable()
    {
        Moved?.Invoke(false);

        foreach (var point in _wayPoints)
        {
            point.Clicked -= Move;
        }
    }

    private void Update()
    {
        RotateCamera();
    }

    private void Move(Transform wayPoint, Transform aimPoint)
    {
        Moved?.Invoke(true);
        _aimPoint = aimPoint;

        var tweenMove = transform.DOMove(wayPoint.position, Duration);
        tweenMove.SetEase(Ease.InOutSine);
        tweenMove.OnComplete(LookAtPlatform);
    }

    private void LookAtPlatform()
    {
        var tweenRotate = transform.DOLookAt(_aimPoint.position, Duration / 2);
        tweenRotate.SetEase(Ease.InOutSine);
    }

    private void RotateCamera()
    {
        float x;
        float y;

        x = Input.GetAxis(MouseX);
        y = Input.GetAxis(MouseY);

        if(Input.GetMouseButton(0))
            transform.localEulerAngles += new Vector3(y, x, 0);
    }
}