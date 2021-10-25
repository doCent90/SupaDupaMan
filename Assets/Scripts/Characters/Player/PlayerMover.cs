using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]
public class PlayerMover : MonoBehaviour
{
    private Transform _platform;
    private WayPointData[] _wayPoints;
    private Lasers[] _laserActivators;

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
        _laserActivators = GetComponentsInChildren<Lasers>();

        foreach (var laser in _laserActivators)
        {
            laser.enabled = false;
        }

        foreach (var point in _wayPoints)
        {
            point.Clicked += Move;
        }
    }

    private void OnDisable()
    {
        Moved?.Invoke(false);

        foreach (var laser in _laserActivators)
        {
            laser.enabled = true;
        }

        foreach (var point in _wayPoints)
        {
            point.Clicked -= Move;
        }
    }

    private void Update()
    {
        Rotate();
    }

    private void Move(Transform wayPoint, Transform platform)
    {
        Moved?.Invoke(true);
        _platform = platform;

        var tweenMove = transform.DOMove(wayPoint.position, Duration);
        tweenMove.SetEase(Ease.InOutSine);
        tweenMove.OnComplete(LookAtPlatform);
    }

    private void LookAtPlatform()
    {
        var tweenRotate = transform.DOLookAt(_platform.position, Duration / 2);
        tweenRotate.SetEase(Ease.InOutSine);

        DisableMover();
    }

    private void Rotate()
    {
        float x;
        float y;

        x = Input.GetAxis(MouseX);
        y = Input.GetAxis(MouseY);

        if(Input.GetMouseButton(0))
            transform.localEulerAngles += new Vector3(y, x, 0);
    }

    private void DisableMover()
    {
        enabled = false;
    }
}