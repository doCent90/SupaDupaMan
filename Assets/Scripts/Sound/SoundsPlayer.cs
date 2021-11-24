public class SoundsPlayer : SoundsPlaying
{
    private PlayerMover _player;
    private Lasers _attack;

    private void Start()
    {
        _player = GetComponentInParent<PlayerMover>();
        _attack = GetComponentInParent<Lasers>();

        _attack.Shoted += Shot;
        _player.Moved += Fly;
    }

    private void OnDisable()
    {
        _attack.Shoted -= Shot;
        _player.Moved -= Fly;
    }
}