public class SoundsPlayer : SoundsPlaying
{
    private PlayerMover _player;
    private LaserActivator _attack;

    private void Start()
    {
        _player = GetComponentInParent<PlayerMover>();
        _attack = GetComponentInParent<LaserActivator>();

        _attack.Shoted += Shot;
        _player.Moved += Fly;
    }

    private void OnDisable()
    {
        _attack.Shoted -= Shot;
        _player.Moved -= Fly;
    }
}