using UnityEngine;

public class ParticalCollisions : MonoBehaviour
{
    private readonly float _damage = 0.05f;

    private void OnParticleCollision(GameObject collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            if(enemy.enabled)
                enemy.TakeDamage(_damage);
        }
    }
}
