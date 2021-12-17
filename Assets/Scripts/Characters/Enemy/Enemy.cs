using System;
using UnityEngine;

[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(CapsuleCollider))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private bool _cantDamageShrapnel = false;

    private EnemyMover _mover;
    private StartGame _startGame;
    private Vector4 _currentColor;
    private SlicedRegDoll _slicedRegDoll;
    private ParticleSystem[] _particalFX;
    private ShotPointCharacter _shotPoint;

    private StickmanSlicer _stickmanSliced;
    private PrisonerRegDoll _prisonerRegDoll;
    private CapsuleCollider _capsuleCollider;
    private SkinnedMeshRenderer _meshRenderer;
    private Vector4 _targetColor = Color.black;

    private float _elapsedTime;
    private bool _isDamaged = false;

    private const float DestroyTime = 0.5f;

    public StartGame StartGame => _startGame;

    public event Action Died;
    public event Action Damaged;
    public event Action<Transform> DiedPosition;
    public event Action<Transform> ShotPointSeted;

    public void TakeDamage()
    {
        SetShotPoint();

        _elapsedTime = DestroyTime;
        _mover.enabled = false;
        _isDamaged = true;

        Damaged?.Invoke();
    }

    private void Awake()
    {
        _startGame = FindObjectOfType<StartGame>();

        _mover = GetComponent<EnemyMover>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _particalFX = GetComponentsInChildren<ParticleSystem>();
        _slicedRegDoll = GetComponentInChildren<SlicedRegDoll>();

        _shotPoint = GetComponentInChildren<ShotPointCharacter>();
        _prisonerRegDoll = GetComponentInChildren<PrisonerRegDoll>();
        _meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        _stickmanSliced = _slicedRegDoll.GetComponentInChildren<StickmanSlicer>();

        _currentColor = _meshRenderer.material.color;
        _slicedRegDoll.gameObject.SetActive(false);

        _startGame.Started += EnableMover;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if((collision.collider.TryGetComponent(out Car car) || collision.collider.TryGetComponent(out Shrapnel shrapnel)) && enabled && !_cantDamageShrapnel)
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
        DiedPosition?.Invoke(transform);

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

    private void OnDisable()
    {
        _startGame.Started -= EnableMover;
    }

    private void EnableMover()
    {
        _mover.enabled = true;
    }
}
