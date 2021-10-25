using UnityEngine;

public class ParticalCollisions : MonoBehaviour
{
    private readonly float _damage = 0.02f;

    private void OnParticleCollision(GameObject collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            if(enemy.enabled)
                enemy.TakeDamage(_damage);
        }
    }
}
