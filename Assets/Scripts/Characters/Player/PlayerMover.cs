using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerRotater))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Transform _lookPoint;

    private Enemy[] _enemies;
    private GameWin _gameWin;
    private PlayerRotater _rotater;
    private Vector3 _lookAtPoint;

    private const float DurationMoveFinish = 5f;
    private const float Duration = 0.5f;
    private const float Distance = 65f;

    public event UnityAction<bool> Moved;

    public void Move(Vector3 pointToMove)
    {
        Moved?.Invoke(true);

        var tweenMove = transform.DOMove(new Vector3(pointToMove.x, transform.position.y, pointToMove.z), Duration);
        tweenMove.SetEase(Ease.InOutSine);
        tweenMove.OnComplete(LookAtClosetstEnemy);
    }

    private void OnEnable()
    {
        _enemies = FindObjectsOfType<Enemy>();
        _gameWin = FindObjectOfType<GameWin>();
        _rotater = GetComponent<PlayerRotater>();

        _rotater.enabled = true;
        _gameWin.Win += LookAtFinish;
    }


    private void OnDisable()
    {
        _rotater.enabled = false;
        _gameWin.Win -= LookAtFinish;
    }

    private void LookAtClosetstEnemy() 
    {
        _lookAtPoint = _lookPoint.position;

        foreach (var enemy in _enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < Distance && enemy.enabled)
            {
                _lookAtPoint = enemy.transform.position;
                continue;
            }
        }

        var tweenRotate = transform.DOLookAt(_lookAtPoint, Duration / 2);
        tweenRotate.SetEase(Ease.InOutSine);
    }

    private void MoveFinishPoint()
    {
        var tweenMove = transform.DOMove(_gameWin.transform.position, DurationMoveFinish);

        enabled = false;
    }

    private void LookAtFinish()
    {
        var tweeLookAt = transform.DOLookAt(_gameWin.transform.position, DurationMoveFinish / 2);
        tweeLookAt.OnComplete(MoveFinishPoint);
    }
}