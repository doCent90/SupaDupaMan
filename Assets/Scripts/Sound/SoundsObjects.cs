using System;
using UnityEngine;

[RequireComponent(typeof(ObjectsSlicer))]
public class SoundsObjects : MonoBehaviour
{
    [SerializeField] private AudioSource _soundBoom;

    private ObjectsSlicer _sclicedObject;

    private void OnEnable()
    {
        if (_soundBoom == null)
            throw new NullReferenceException(nameof(SoundsObjects));

        _sclicedObject = GetComponent<ObjectsSlicer>();
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
