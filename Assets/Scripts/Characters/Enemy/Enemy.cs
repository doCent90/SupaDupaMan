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
    [SerializeField] private ParticleSystem _emojiLaugh;

    private EnemyMover _mover;
    private Vector4 _currentColor;
    private Vector4 _targetColor = Color.black;
    private ParticleSystem[] _particalFX;
    private SkinnedMeshRenderer _renderer;
    private EnemyParticals _enemyParticals;
    private CapsuleCollider _capsuleCollider;
    private SkinnedMeshRenderer _meshRenderer;

    private bool _hasInvisible = false;
    private float _hitPoints;

    private const float StandartHitPoints = 1f;
    private const float Multiply = 2f;

    public event UnityAction Died;

    public bool IsGigant => _isGigant;

    public void TakeDamage(float damage)
    {
        if (!_hasInvisible)
        {
            _hitPoints = !_isGigant? (_hitPoints -= damage) : (_hitPoints -= damage / Multiply);
        }

        ChangeColor();
        Die();
    }

    public void SetTempInvisible(bool hasInvis)
    {
        _hasInvisible = hasInvis;

        if (_hasInvisible)
            _emojiLaugh.Play();
        else
            _emojiLaugh.Stop();
    }

    private void Die()
    {
        if (!_hasInvisible && _hitPoints <= 0)
        {
            enabled = false;

            Died?.Invoke();
            SetDieMaterial();

            PlayFX();
            _mover.enabled = true;

            _emoji.Stop();
            _capsuleCollider.isTrigger = true;
        }
    }

    private void ChangeColor()
    {
        _meshRenderer.material.color = Vector4.Lerp(_targetColor, _currentColor, _hitPoints);
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
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _enemyParticals = GetComponentInChildren<EnemyParticals>();
        _meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        _renderer = GetComponentInChildren<SkinnedMeshRenderer>();
        _particalFX = _enemyParticals.GetComponentsInChildren<ParticleSystem>();

        _currentColor = _meshRenderer.material.color;

        _hitPoints = StandartHitPoints;
    }

    private void SetDieMaterial()
    {
        _renderer.material = _dieMaterial;
    }
}
