using UnityEngine;

public class StateMachinePlayer : MonoBehaviour
{
    [SerializeField] private StatePlayer _firstState;

    private StatePlayer _currentState;

    public StatePlayer Current => _currentState;

    private void Start()
    {
        ResetState(_firstState);
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        var nextState = _currentState.GetNextState();
        if (nextState != null)
            Transit(nextState);
    }

    private void ResetState(StatePlayer startState)
    {
        _currentState = startState;

        if (_currentState != null)
            _currentState.Enter();
    }

    private void Transit(StatePlayer nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState != null)
        {
            _currentState.Enter();
        }
    }
}
