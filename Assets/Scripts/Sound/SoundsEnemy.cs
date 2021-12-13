using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class SoundsEnemy : MonoBehaviour
{
    [SerializeField] private AudioSource[] _effects;

    private Enemy _enemy;

    private void OnEnable()
    {
        _enemy = GetComponent<Enemy>();
        _enemy.Died += OnDied;
    }

    private void OnDisable()
    {
        _enemy.Died -= OnDied;
    }

    private void OnDied()
    {
        foreach (var sound in _effects)
        {
            sound.Play();
        }
    }
}
