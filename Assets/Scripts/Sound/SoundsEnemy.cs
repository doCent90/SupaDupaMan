using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class SoundsEnemy : MonoBehaviour
{
    [SerializeField] private AudioSource _soundDeath;

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
        _soundDeath.Play();
    }
}
