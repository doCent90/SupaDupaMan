using UnityEngine;
using UnityEngine.Events;

public class WayPointData : MonoBehaviour
{
    private Enemy[] _allEnemy; 
    private Enemy[] _enemiesOnPoint; 
    private Transform _transform;
    private Transform _aimPosition;

    public event UnityAction<Transform, Transform> Clicked;

    public void SetTransformWayPoint()
    {
        Clicked?.Invoke(_transform, _aimPosition);
        SetDeactiveColliders();
        SetActiveCollider();
    }

    public void SetActiveCollider()
    {
        foreach (var enemy in _enemiesOnPoint)
        {
            enemy.GetComponent<CapsuleCollider>().enabled = true;
        }
    }

    public void SetDeactiveColliders()
    {
        foreach (var enemy in _allEnemy)
        {
            enemy.GetComponent<CapsuleCollider>().enabled = false;
        }
    }

    private void OnEnable()
    {
        _transform = transform;
        _allEnemy = FindObjectsOfType<Enemy>();
        _enemiesOnPoint = GetComponentsInChildren<Enemy>();

        if(GetComponentInChildren<AimPoint>() != null)
            _aimPosition = GetComponentInChildren<AimPoint>().transform;
    }
}
