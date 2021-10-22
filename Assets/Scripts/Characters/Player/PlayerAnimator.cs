using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private PlayerMover _mover;
    private AttackState _attack;

    private const string RunAnimation = "Run";
    private const string ShootAnimation = "Shoot";
    private const string TargetAnimation = "LockTarget";

    private void OnEnable()
    {
        _animator = GetComponentInChildren<Animator>();
        _mover = GetComponent<PlayerMover>();
        _attack = GetComponent<AttackState>();

        _mover.Moved += Move;
        _attack.Shoted += Shot;
        _attack.ReadyToAttacked += LockTarget;
    }

    private void Move(bool isMoving)
    {
        _animator.SetBool(RunAnimation, isMoving);
    }

    private void LockTarget(bool isLockedTarget)
    {
        _animator.SetBool(TargetAnimation, isLockedTarget);
    }

    private void Shot()
    {
        _animator.SetTrigger(ShootAnimation);
    }
}
