using System;
using UnityEngine;

public class GlassWall : MonoBehaviour
{
    private float _elapsedTime;
    private bool _isDamaged = false;
    private DamagePointWallSlicerMover _damagePointWallSlicerMover;

    private const float DestroingObjectsTime = 0.5f;

    public event Action Destroyed;
    public event Action<Transform> ApplyDamage;

    public void TakeDamage()
    {
        _isDamaged = true;
        _elapsedTime = DestroingObjectsTime;
        ApplyDamage?.Invoke(_damagePointWallSlicerMover.transform);
    }

    private void OnEnable()
    {
        _damagePointWallSlicerMover = GetComponentInChildren<DamagePointWallSlicerMover>();
    }

    private void Die()
    {
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
