using UnityEngine;
using UnityEngine.Events;

public class LasersActivator : MonoBehaviour
{
    [Header("Lasers")]
    [SerializeField] private int _laserNumber;
    [SerializeField] private GameObject[] _lasers;
    [Header("Settings of Shot")]
    [SerializeField] private Transform _shootPosition1;
    [SerializeField] private Transform _shootPosition2;

    private Enemy[] _enemies;
    private Transform _aimPoint;
    private WallSlicer[] _walls;
    private LaserRenderer2 _reset1;
    private LaserRenderer2 _reset2;
    private GameObject _laserPrefab1;
    private GameObject _laserPrefab2;
    private PlayerMover _playerMover;

    private const float Delay = 0.6f;

    public event UnityAction<bool> ReadyToAttacked;
    public event UnityAction<bool> Fired;
    public event UnityAction Shoted;

    private void OnEnable()
    {
        _enemies = FindObjectsOfType<Enemy>();
        _walls = FindObjectsOfType<WallSlicer>();
        _playerMover = GetComponentInParent<PlayerMover>();

        ReadyToAttacked?.Invoke(true);

        foreach (var wall in _walls)
        {
            wall.ApplyDamage += Attack;
            wall.Destroyed += Stop;
        }

        foreach (var enemy in _enemies)
        {
            enemy.ShotPointSeted += Attack;
            enemy.Died += Stop;
        }
    }

    private void OnDisable()
    {
        Stop();

        ReadyToAttacked?.Invoke(false);
        Fired?.Invoke(false);

        foreach (var wall in _walls)
        {
            wall.ApplyDamage -= Attack;
            wall.Destroyed -= Stop;
        }

        foreach (var enemy in _enemies)
        {
            enemy.ShotPointSeted -= Attack;
            enemy.Died -= Stop;
        }
    }

    private void Attack(Transform aimPoint)
    {
        _aimPoint = aimPoint;
        RotateShootPosition();
        Shoot();
    }

    private void Shoot()
    {
        _playerMover.enabled = false;

        _laserPrefab1 = Instantiate(_lasers[_laserNumber], _shootPosition1.position, _shootPosition1.rotation);
        _laserPrefab2 = Instantiate(_lasers[_laserNumber], _shootPosition2.position, _shootPosition2.rotation);

        LookAtAim(_laserPrefab1, _aimPoint);
        LookAtAim(_laserPrefab2, _aimPoint);

        _reset1 = _laserPrefab1.GetComponent<LaserRenderer2>();
        _reset2 = _laserPrefab2.GetComponent<LaserRenderer2>();

        Fired?.Invoke(true);
        Shoted?.Invoke();
    }

    private void LookAtAim(GameObject laserPrefab, Transform aimPoint)
    {        
        laserPrefab.transform.LookAt(aimPoint);
    }

    private void Stop()
    {
        if (_reset1)
        {
            _reset1.DisablePrepare();
            _reset2.DisablePrepare();
        }

        _playerMover.enabled = true;
        Fired?.Invoke(false);

        Destroy(_laserPrefab1, Delay);
        Destroy(_laserPrefab2, Delay);
    }

    private void RotateShootPosition()
    {
        _shootPosition1.LookAt(_aimPoint);
        _shootPosition2.LookAt(_aimPoint);
    }
}