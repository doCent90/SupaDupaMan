using System.Collections.Generic;
using UnityEngine;

public abstract class StatePlayer : MonoBehaviour
{
    [SerializeField] private List<TransitionPlayer> _transitions;

    public void Enter()
    {
        if (!enabled)
        {
            enabled = true;
            foreach (var transition in _transitions)
            {
                transition.enabled = true;
            }
        }
    }

    public void Exit()
    {
        if (enabled)
        {
            foreach (var transition in _transitions)
            {
                transition.enabled = false;
            }

            enabled = false;
        }
    }

    public StatePlayer GetNextState()
    {
        foreach (var transition in _transitions)
        {
            if (transition.NeedTransit)
            {
                return transition.TargetState;
            }
        }

        return null;
    }
}
