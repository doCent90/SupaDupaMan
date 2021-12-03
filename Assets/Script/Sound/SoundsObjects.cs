using UnityEngine;

[RequireComponent(typeof(ObjectsSlicer))]
public class SoundsObjects : MonoBehaviour
{
    [SerializeField] private AudioSource _soundBoom;

    private ObjectsSlicer _sclicedObject;

    private void OnEnable()
    {
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
