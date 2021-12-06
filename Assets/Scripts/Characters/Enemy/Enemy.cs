using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(CapsuleCollider))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Material _dieMaterial;
    [SerializeField] private StartGame _startGame;

    private bool _isGigant;
    private EnemyMover _mover;
    private Vector4 _currentColor;
    private SlicedRegDoll _slicedRegDoll;
    private ParticleSystem[] _particalFX;

    private ShotPointCharacter _shotPoint;
    private SkinnedMeshRenderer _renderer;
    private StickmanSlicer _stickmanSliced;
    private EnemyParticals _enemyParticals;

    private PrisonerRegDoll _prisonerRegDoll;
    private CapsuleCollider _capsuleCollider;
    private SkinnedMeshRenderer _meshRenderer;
    private Vector4 _targetColor = Color.black;

    private float _elapsedTime;
    private bool _isDamaged = false;

    private const float DestroyTime = 1f;

    public event UnityAction Died;
    public event UnityAction Damaged;
    public event UnityAction<Transform> ShotPointSeted;

    public bool IsGigant => _isGigant;

    public void TakeDamage()
    {
        SetShotPoint();

        _elapsedTime = DestroyTime;
        _mover.enabled = false;
        _isDamaged = true;

        Damaged?.Invoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent(out Car car) && enabled)
        {
            _mover.enabled = false;
            Die();
        }
    }

    private void Update()
    {
        if (!_isDamaged)
            return;        
        else
        {
            _elapsedTime -= Time.deltaTime;

            if (_elapsedTime <= 0)
                Die();

            ChangeColor();
        }
    }

    private void SetShotPoint()
    {
        var shotPoint = _shotPoint.transform;

        ShotPointSeted?.Invoke(shotPoint);
    }

    private void Die()
    {
        enabled = false;

        Died?.Invoke();
        SetDieMaterial();

        PlayFX();
        _capsuleCollider.enabled = false;

        _prisonerRegDoll.gameObject.SetActive(false);
        _slicedRegDoll.gameObject.SetActive(true);
        _stickmanSliced.TakeDamage();
    }

    private void ChangeColor()
    {
        _meshRenderer.material.color = Vector4.Lerp(_currentColor, _targetColor, _elapsedTime);
    }

    private void PlayFX()
    {
        foreach (var partical in _particalFX)
        {
            partical.Play();
        }
    }

    private void Awake()
    {
        if (_startGame == null || _dieMaterial == null)
            throw new InvalidOperationException();

        if (GetComponent<EnemyGigant>())
            _isGigant = true;
        else
            _isGigant = false;

        _mover = GetComponent<EnemyMover>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _slicedRegDoll = GetComponentInChildren<SlicedRegDoll>();
        _shotPoint = GetComponentInChildren<ShotPointCharacter>();

        _enemyParticals = GetComponentInChildren<EnemyParticals>();
        _prisonerRegDoll = GetComponentInChildren<PrisonerRegDoll>();
        _meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        _stickmanSliced = _slicedRegDoll.GetComponentInChildren<StickmanSlicer>();

        _renderer = GetComponentInChildren<SkinnedMeshRenderer>();
        _particalFX = _enemyParticals.GetComponentsInChildren<ParticleSystem>();

        _currentColor = _meshRenderer.material.color;
        _slicedRegDoll.gameObject.SetActive(false);

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
