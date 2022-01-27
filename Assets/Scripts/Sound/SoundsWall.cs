using System;
using UnityEngine;

[RequireComponent(typeof(WallSlicer))]
public class SoundsWall : MonoBehaviour
{
    [SerializeField] private AudioSource _soundBoom;

    private WallSlicer _sclicedObject;

    private void OnEnable()
    {
        if (_soundBoom == null)
            throw new NullReferenceException(nameof(SoundsWall));

        _sclicedObject = GetComponent<WallSlicer>();
        _sclicedObject.Destroyed += OnDied;
    }

    private void OnDisable()
    {
        _sclicedObject.Destroyed -= OnDied;
    }

    private void OnDied()
    {
        _soundBoom.Play();
    }
}
