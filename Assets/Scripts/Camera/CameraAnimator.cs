using System.Collections;
using UnityEngine;
using DG.Tweening;

public class CameraAnimator : MonoBehaviour
{
    private Enemy[] _enemies;
    private LasersActivator _lasers;
    private Transform _targetPosition;
    private Vector3 _originalPosition;

    private bool _isShake;

    private const float Range = 0.3f;
    private const float Duration = 3f;
    private const float Delay = 0.025f;

    private const float Multyply = 10f;

    public void StopShake()
    {
        StopCoroutine(Shake());
    }

    private void OnEnable()
    {
        _enemies = FindObjectsOfType<Enemy>();
        _targetPosition = GetComponent<Transform>();
        _lasers = FindObjectOfType<LasersActivator>();

        _originalPosition = _targetPosition.transform.localEulerAngles;

        _lasers.Fired += TryShake;
        _isShake = false;

        foreach (var enemy in _enemies)
        {
            enemy.Died += OnEmeyDied;
        }

        PlayIdle();
    }

    private void OnDisable()
    {
        _lasers.Fired -= TryShake;

        foreach (var enemy in _enemies)
        {
            enemy.Died -= OnEmeyDied;
        }
    }

    private void TryShake(bool isAttack)
    {
        _isShake = isAttack;

        if (!isAttack)
            StopCoroutine(Shake());
        else if (isAttack)
            StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        var waitForSecond = new WaitForSeconds(Delay);

        while (_isShake)
        {
            _targetPosition.localEulerAngles += GetDirection();
            yield return waitForSecond;
        }

        _targetPosition.localEulerAngles = _originalPosition;
    }

    private void PlayIdle()
    {
        Vector3 rotateDirection = GetDirection(Multyply);

        var tweenRotate = transform.DOLocalRotate(new Vector3(_originalPosition.x + rotateDirection.x, 
                                                    rotateDirection.y, rotateDirection.z), Duration);
        tweenRotate.SetEase(Ease.InOutSine);
        tweenRotate.OnComplete(ResetPosition);
    }

    private Vector3 GetDirection(float multyply = 1f)
    {
        float x;
        float y;
        float z;

        x = Random.Range(-Range, Range) * multyply;
        y = Random.Range(-Range, Range) * multyply;
        z = Random.Range(-Range, Range) * multyply;

        return new Vector3(x, y, z);
    }

    private void ResetPosition()
    {
        var tweenResetPosition = transform.DOLocalRotate(_originalPosition, Duration);
        tweenResetPosition.SetEase(Ease.InOutSine);
        tweenResetPosition.OnComplete(PlayIdle);
    }

    private void OnEmeyDied()
    {
        StopShake();
    }
}
