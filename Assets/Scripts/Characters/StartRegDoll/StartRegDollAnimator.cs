using UnityEngine;

public class StartRegDollAnimator : MonoBehaviour
{
    private Animator _animator;

    private const string Run = "Run";

    private void OnEnable()
    {
        _animator = GetComponentInChildren<Animator>();
        _animator.SetTrigger(Run);
    }
}
