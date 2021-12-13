using System;
using UnityEngine;
using UnityEngine.Events;

public class LasersActivator : MonoBehaviour
{
    [Header("Standart Lsaer")]
    [SerializeField] LaserRenderer2 _standartLaser;
    [Header("Settings of Shot")]
    [SerializeField] private Transform _shootPosition1;
    [SerializeField] private Transform _shootPosition2;

    private Transform _aimPoint;
    private LaserRenderer2 _currentLaser;
    private LaserRenderer2 _laserPrefab1;
    private LaserRenderer2 _laserPrefab2;
    private PlayerMover _playerMover;
    private Enemy[] _enemies;
    private GlassWall[] _glassWall;
    private WallSlicer[] _wallsSlicer;
    private ObjectsSlicer[] _objectsScliced;

    private const float Delay = 0.6f;

    public event UnityAction<bool> ReadyToAttacked;
    public event UnityAction<bool> Fired;
    public event UnityAction Shoted;

    public void SetLaser(LaserRenderer2 laser)
    {
        _currentLaser = laser;
    }

    private void Awake()
    {
        if (_standartLaser == null)
            throw new InvalidOperationException();

        _playerMover = GetComponentInParent<PlayerMover>();
        _enemies = FindObjectsOfType<Enemy>();
        _glassWall = FindObjectsOfType<GlassWall>();
        _wallsSlicer = FindObjectsOfType<WallSlicer>();
        _objectsScliced = FindObjectsOfType<ObjectsSlicer>();

        ReadyToAttacked?.Invoke(true);

        foreach (var wall in _glassWall)
        {
            wall.ApplyDamage += Attack;
            wall.Destroyed += Stop;
        }

        foreach (var prop in _objectsScliced)
        {
            prop.ApplyDamage += Attack;
            prop.Destroyed += Stop;
        }

        foreach (var wall in _wallsSlicer)
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
        ReadyToAttacked?.Invoke(false);
        Fired?.Invoke(false);

        foreach (var wall in _glassWall)
        {
            wall.ApplyDamage -= Attack;
            wall.Destroyed -= Stop;
        }

        foreach (var prop in _objectsScliced)
        {
            prop.ApplyDamage -= Attack;
            prop.Destroyed -= Stop;
        }

        foreach (var wall in _wallsSlicer)
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

    private void Start()
    {
        _currentLaser = _standartLaser;
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

        _laserPrefab1 = Instantiate(_currentLaser, _shootPosition1.position, _shootPosition1.rotation);
        _laserPrefab2 = Instantiate(_currentLaser, _shootPosition2.position, _shootPosition2.rotation);

        LookAtAim(_laserPrefab1, _aimPoint);
        LookAtAim(_laserPrefab2, _aimPoint);

        Fired?.Invoke(true);
        Shoted?.Invoke();
    }

    private void LookAtAim(LaserRenderer2 laserPrefab, Transform aimPoint)
    {        
        laserPrefab.transform.LookAt(aimPoint);
    }

    private void Stop()
    {
        if(_laserPrefab1 != null)
        {
            _laserPrefab1.DisablePrepare();
            _laserPrefab2.DisablePrepare();

            _playerMover.enabled = true;
            Fired?.Invoke(false);

            Destroy(_laserPrefab1.gameObject, Delay);
            Destroy(_laserPrefab2.gameObject, Delay);
        }
    }

    private void RotateShootPosition()
    {
        _shootPosition1.LookAt(_aimPoint);
        _shootPosition2.LookAt(_aimPoint);
    }
}
