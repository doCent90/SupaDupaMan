using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]
public class PlayerMover : MonoBehaviour
{
    private Transform _wayPoint;
    private WayPointData[] _wayPoints;
    private LaserActivator[] _laserActivators;

    private const float Duration = 2f;
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
        _laserActivators = GetComponentsInChildren<LaserActivator>();

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

    private void Move(Transform wayPoint)
    {
        Moved?.Invoke(true);
        _wayPoint = wayPoint;

        var tweenMove = transform.DOMove(_wayPoint.position, Duration);
        tweenMove.SetEase(Ease.InOutSine);

        LookAtWayPoint(wayPoint);
        tweenMove.OnComplete(DisableMover);
    }

    private void LookAtWayPoint(Transform wayPoint)
    {
        var tweenRotate = transform.DOLookAt(new Vector3(0, _wayPoint.position.y, 0), Duration);
        tweenRotate.SetEase(Ease.InOutSine);
        tweenRotate.OnComplete(LookAtEnemies);
    }

    private void LookAtEnemies()
    {
        var tweenRotate = transform.DORotate(new Vector3(0, _wayPoint.position.y, 0), Duration / 2);
        tweenRotate.SetEase(Ease.InOutSine);
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