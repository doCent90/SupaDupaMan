using UnityEngine;
using DG.Tweening;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _viewPoint;

    private AttackState _attackState;
    private Transform _startPosition;

    private const float Duration = 0.3f;

    private void OnEnable()
    {
        _attackState = GetComponentInParent<AttackState>();
        _startPosition = transform;

        _attackState.ReadyToAttacked += FocusView;
    }

    private void OnDisable()
    {
        _attackState.ReadyToAttacked -= FocusView;
    }

    private void FocusView(bool isRaeady)
    {
        if (isRaeady)
            transform.DOMove(_viewPoint.position, Duration);
        else
            transform.DOMove(_startPosition.position, Duration);
    }
}
