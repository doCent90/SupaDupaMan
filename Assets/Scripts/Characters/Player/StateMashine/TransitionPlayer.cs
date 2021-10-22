using UnityEngine;

public abstract class TransitionPlayer : MonoBehaviour
{
    [SerializeField] private StatePlayer _targetState;

    public bool NeedTransit { get; protected set; }

    public StatePlayer TargetState => _targetState;

    private void OnEnable()
    {
        NeedTransit = false;
    }
}
