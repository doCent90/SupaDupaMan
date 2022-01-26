using System;
using UnityEngine;

public class GlassWall : Selectable
{
    private float _elapsedTime;
    private bool _isDamaged = false;

    private ParticleSystem[] _particleSystems;
    private DamagePointWallSlicerMover _damagePointWallSlicerMover;

    private const float DestroingObjectsTime = 0.5f;

    public event Action Destroyed;
    public event Action<Transform> ApplyDamage;

    public override void TakeDamage()
    {
        _isDamaged = true;
        _elapsedTime = DestroingObjectsTime;
        ApplyDamage?.Invoke(_damagePointWallSlicerMover.transform);
    }

    private void OnEnable()
    {
        _particleSystems = GetComponentsInChildren<ParticleSystem>();
        _damagePointWallSlicerMover = GetComponentInChildren<DamagePointWallSlicerMover>();
    }

    private void Die()
    {
        foreach (ParticleSystem part in _particleSystems)
        {
            part.Play();
        }

        _isDamaged = false;
        Destroyed?.Invoke();
    }

    private void Update()
    {
        if (!_isDamaged)
            return;
        else
        {
            _elapsedTime -= Time.deltaTime;

            if (_elapsedTime <= 0)
                Die();
        }
    }
}
