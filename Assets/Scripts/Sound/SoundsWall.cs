using UnityEngine;

[RequireComponent(typeof(WallSlicer))]
public class SoundsWall : MonoBehaviour
{
    [SerializeField] private AudioSource _soundBoom;

    private WallSlicer _sclicedObject;

    private void OnEnable()
    {
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
