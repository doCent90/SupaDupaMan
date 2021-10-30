using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(EnemyAnimator))]
[RequireComponent(typeof(CapsuleCollider))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Material _dieMaterial;
    [SerializeField] private bool _isGigant;
    [Header("Emoji Particals")]
    [SerializeField] private ParticleSystem _emoji;

    private EnemyMover _mover;
    private StartGame _startGame;
    private Vector4 _currentColor;
    private ParticleSystem[] _particalFX;
    private ShotPointCharacter _shotPoint;
    private SkinnedMeshRenderer _renderer;
    private EnemyParticals _enemyParticals;
    private CapsuleCollider _capsuleCollider;
    private SkinnedMeshRenderer _meshRenderer;
    private Vector4 _targetColor = Color.black;

    private float _elapsedTime;
    private bool _isDamaged = false;

    private const float Delay = 1f;

    public event UnityAction Died;
    public event UnityAction Damaged;
    public event UnityAction<Transform> TargetLocked;

    public bool IsGigant => _isGigant;

    public void TakeDamage()
    {
        _elapsedTime = Delay;
        _mover.enabled = false;
        _isDamaged = true;

        Damaged?.Invoke();
    }

    public void LockTarget()
    {
        var shotPoint = _shotPoint.transform;

        TargetLocked?.Invoke(shotPoint);
    }

    private void Update()
    {
        if (_isDamaged)
        {
            if (_elapsedTime <= 0)
                Die();

            _elapsedTime -= Time.deltaTime;
            ChangeColor();
        }
        else
            return;        
    }

    private void Die()
    {
        enabled = false;

        Died?.Invoke();
        SetDieMaterial();

        PlayFX();
        _mover.enabled = false;

        _emoji.Stop();
        _capsuleCollider.enabled = false;
    }

    private void ChangeColor()
    {
        _meshRenderer.material.color = Vector4.Lerp(_currentColor, _targetColor, _elapsedTime);
        _emoji.Play();
    }

    private void PlayFX()
    {
        foreach (var partical in _particalFX)
        {
            partical.Play();
        }
    }

    private void OnEnable()
    {
        _mover = GetComponent<EnemyMover>();
        _startGame = FindObjectOfType<StartGame>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _shotPoint = GetComponentInChildren<ShotPointCharacter>();
        _enemyParticals = GetComponentInChildren<EnemyParticals>();
        _meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        _renderer = GetComponentInChildren<SkinnedMeshRenderer>();
        _particalFX = _enemyParticals.GetComponentsInChildren<ParticleSystem>();

        _currentColor = _meshRenderer.material.color;

        _startGame.Started += EnableMover;
    }

    private void OnDisable()
    {
        _startGame.Started -= EnableMover;
    }

    private void SetDieMaterial()
    {
        _renderer.material = _dieMaterial;
    }

    private void EnableMover()
    {
        _mover.enabled = true;
    }
}
