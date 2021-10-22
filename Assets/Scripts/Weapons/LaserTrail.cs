using UnityEngine;

public class LaserTrail : MonoBehaviour
{
    [SerializeField] private GameObject _hitEffect;

    private bool _isReady = false;

    private AttackState _attack;
    private PlayerMover _playerMover;
    private GameOverField _gameOverField;
    private ParticleSystem[] _hits;

    private const float _maxLength = 25f;
    private const float _hitOffset = 0.1f;

    private void OnEnable()
    {
        _hits = GetComponentsInChildren<ParticleSystem>();
        _attack = GetComponentInParent<AttackState>();
        _playerMover = FindObjectOfType<PlayerMover>();
        _gameOverField = FindObjectOfType<GameOverField>();

        _attack.Fired += ActivatLaser;
        _playerMover.LastPointCompleted += OnLastPoint;
        _gameOverField.Defeated += OnLastPoint;
    }

    private void OnDisable()
    {
        _attack.Fired -= ActivatLaser;
        _playerMover.LastPointCompleted += OnLastPoint;
        _gameOverField.Defeated -= OnLastPoint;
    }

    private void Update()
    {
        StartTrailLaser();
    }

    private void ActivatLaser(bool isAttack)
    {
        _isReady = isAttack;
    }

    private void OnLastPoint()
    {
        foreach (var partical in _hits)
        {
            partical.Pause();
        }
    }

    private void StartTrailLaser()
    {
        if (_isReady == true)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, _maxLength))
            {
                _hitEffect.transform.position = hit.point + hit.normal * _hitOffset;
                _hitEffect.transform.rotation = Quaternion.identity;

                foreach (var partical in _hits)
                {
                    if (!partical.isPlaying)
                        partical.Play();
                }
            }
        }
        else
        {
            foreach (var partical in _hits)
            {
                if (partical.isPlaying) partical.Stop();
            }
        }
    }
}
