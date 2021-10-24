using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private bool _isLastQueuEnemy;

    private WayPointData _enemiesPoint;
    private Enemy _enemy;

    private const float Speed = 0f;
    private const float DurationZ = 2f;
    private const float DurationX = 0.8f;
    private const float DistanceZ = 12f;
    private const float DistanceX = 3f;

    private void OnEnable()
    {
        _enemy = GetComponent<Enemy>();
        _enemiesPoint = GetComponentInParent<WayPointData>();
    }
}
