using UnityEngine;

public class LaserParticalCollisions : MonoBehaviour
{
    private readonly float _damage = 0.01f;

    private void OnParticleCollision(GameObject collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            if(enemy.enabled)
                enemy.TakeDamage(_damage);
        }
    }
}
