public class SoundsEnemy : SoundsPlaying
{
    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponentInParent<Enemy>();
        _enemy.Died += Death;
    }

    private void OnDisable()
    {
        _enemy.Died -= Death;
    }
}
