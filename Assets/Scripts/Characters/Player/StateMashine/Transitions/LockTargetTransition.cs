public class LockTargetTransition : TransitionPlayer
{
    private PlayerMover _playerMover;

    private void Start()
    {
        _playerMover = GetComponent<PlayerMover>();        
    }

    private void Update()
    {
        if (_playerMover.HasCurrentPositions)
            NeedTransit = true;
        else
            NeedTransit = false;
    }
}
