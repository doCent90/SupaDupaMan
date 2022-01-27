using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class SoundsPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _soundShot;
    [SerializeField] private AudioSource _soundFly;

    private PlayerMover _player;
    private LasersActivator _attack;

    private void OnEnable()
    {
        if (_soundFly == null || _soundShot == null)
            throw new NullReferenceException(nameof(SoundsPlayer));

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
        ActivateSound(isFired, _soundShot);
    }

    private void OnFly(bool isFly)
    {
        ActivateSound(isFly, _soundFly);
    }

    private void ActivateSound(bool isReady, AudioSource sound)
    {
        if (isReady)
            sound.Play();
        else
            sound.Stop();
    }
}
