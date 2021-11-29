using UnityEngine;

public class SoundsPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _soundShot;
    [SerializeField] private AudioSource _soundFly;

    private PlayerMover _player;
    private LasersActivator _attack;

    private void Start()
    {
        _player = GetComponent<PlayerMover>();
        _attack = GetComponentInChildren<LasersActivator>();

        _attack.Fired += OnFired;
        _player.Moved += OnFly;
    }

    private void OnDisable()
    {
        _attack.Fired -= OnFired;
        _player.Moved -= OnFly;
    }

    private void OnFired(bool isFired)
    {
        if (isFired)
            _soundShot.Play();
        else
            _soundShot.Stop();
    }

    private void OnFly(bool isFly)
    {
        if (isFly)
            _soundFly.Play();
        else
            _soundFly.Stop();
    }
}
