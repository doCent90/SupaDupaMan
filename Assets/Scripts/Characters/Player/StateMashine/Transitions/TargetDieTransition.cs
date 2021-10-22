public class TargetDieTransition : TransitionPlayer
{
    public void OnTargetDied()
    {
        NeedTransit = true;
    }
}
