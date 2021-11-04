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
    private SlicedRegDoll _slicedRegDoll;
    private ParticleSystem[] _particalFX;

    private ShotPointCharacter _shotPoint;
    private SkinnedMeshRenderer _renderer;
    private StickmanSliced _stickmanSliced;
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
        _mover.enabled = false;

        _emoji.Stop();
        _capsuleCollider.enabled = false;

        _prisonerRegDoll.gameObject.SetActive(false);
        _slicedRegDoll.gameObject.SetActive(true);
        _stickmanSliced.StartSclice();
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
        _slicedRegDoll = GetComponentInChildren<SlicedRegDoll>();
        _shotPoint = GetComponentInChildren<ShotPointCharacter>();

        _enemyParticals = GetComponentInChildren<EnemyParticals>();
        _prisonerRegDoll = GetComponentInChildren<PrisonerRegDoll>();
        _meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        _stickmanSliced = _slicedRegDoll.GetComponentInChildren<StickmanSliced>();

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
