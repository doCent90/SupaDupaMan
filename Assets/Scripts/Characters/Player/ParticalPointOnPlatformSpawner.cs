using System;
using UnityEngine;

[RequireComponent(typeof(ObjectsSelector))]
public class ParticalPointOnPlatformSpawner : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    private ObjectsSelector _objectsSelector;

    private void OnEnable()
    {
        _objectsSelector = GetComponent<ObjectsSelector>();

        _objectsSelector.TargetPointSelected += Play;
    }

    private void OnDisable()
    {
        _objectsSelector.TargetPointSelected -= Play;
    }

    private void Play(Vector3 position)
    {
        ParticleSystem fx = Instantiate(_particleSystem, position, Quaternion.identity);
        fx.Play();
        fx.gameObject.AddComponent<Destroyer>();
    }
}
