using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private bool _isLastQueuEnemy;

    private WayPoint _enemiesPoint;
    private Enemy[] _aliveEnemies;
    private Enemy _enemy;
    private bool _isDead = false;
    private bool _hasSprintedForward = false;
    private bool _hasSprintedSide = false;

    private const int Chance = 99;
    private const float Speed = 0f;
    private const float DurationZ = 2f;
    private const float DurationX = 0.8f;
    private const float DistanceZ = 12f;
    private const float DistanceX = 3f;

    private void RandomMove()
    {
        int randomNumber = Random.Range(0, 100);
        int typeRandomMove = Random.Range(0, 2);

        if (typeRandomMove == 0)
            SprintForward(randomNumber);
        else
            SprintSide(randomNumber);


    }

    private void SprintForward(int randomNumber)
    {
        if(Chance >= randomNumber && !_isDead && !_isLastQueuEnemy && !_hasSprintedForward)
        {
            var currentPosition = transform.position;

            _enemy.SetTempInvisible(true);
            transform.DOMoveZ(currentPosition.z + DistanceZ, DurationZ).OnComplete(SetEnemyInvis);

            _hasSprintedForward = true;
            _hasSprintedSide = true;
        }
    }

    private void SprintSide(int randomNumber)
    {
        var currentPosition = transform.position;
        float direction = DistanceX;

        if (Chance >= randomNumber && !_isDead && !_hasSprintedSide)
        {
            _enemy.SetTempInvisible(true);

            if (currentPosition.x < 0)
                direction *= -1;

            transform.DOMoveX(currentPosition.x - direction, DurationX).OnComplete(SetEnemyInvis);

            _hasSprintedSide = true;
            _hasSprintedForward = true;
        }
    }

    private void SetEnemyInvis()
    {
        _enemy.SetTempInvisible(false);
    }

    private void OnEnable()
    {
        _enemy = GetComponent<Enemy>();
        _enemiesPoint = GetComponentInParent<WayPoint>();
        _aliveEnemies = _enemiesPoint.GetComponentsInChildren<Enemy>();

        _enemy.Died += OnDied;

        foreach (var enemy in _aliveEnemies)
        {
            enemy.Died += RandomMove;
        }
    }

    private void OnDisable()
    {
        foreach (var enemy in _aliveEnemies)
        {
            enemy.Died -= RandomMove;
        }

        _enemy.Died -= OnDied;
    }

    private void OnDied()
    {
        _isDead = true;
    }

    private void Update()
    {
        if (!_isDead)
            return;
        else
            transform.Translate(Vector3.back * Speed * Time.deltaTime);
    }
}
