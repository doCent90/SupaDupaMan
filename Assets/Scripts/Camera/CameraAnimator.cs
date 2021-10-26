using System.Collections;
using UnityEngine;
using DG.Tweening;

public class CameraAnimator : MonoBehaviour
{
    private Transform _camera;
    private Vector3 _originalPosition;
    private Lasers _lasers;

    private bool _isAttack;

    private const float Range = 0.3f;
    private const float Duration = 3f;
    private const float Delay = 0.025f;

    private const float MultyplyX = 10f;
    private const float MultyplyY = 5f;
    private const float MultyplyZ = 2f;

    public void StopShake()
    {
        StopCoroutine(Shake());
    }

    private void OnEnable()
    {
        _lasers = FindObjectOfType<Lasers>();
        _camera = GetComponent<Transform>();

        _originalPosition = _camera.transform.localEulerAngles;

        _lasers.Fired += ShakeTransform;
        _isAttack = false;

        PlayIdle();
    }

    private void OnDisable()
    {
        _lasers.Fired -= ShakeTransform;
    }

    private void ShakeTransform(bool isAttack)
    {
        _isAttack = isAttack;

        if (!isAttack)
            StopCoroutine(Shake());
        else if (isAttack)
            StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        float x;
        float y;
        float z;

        var waitForSecond = new WaitForSeconds(Delay);

        while (_isAttack)
        {
            x = Random.Range(-Range, Range);
            y = Random.Range(-Range, Range);
            z = Random.Range(-Range, Range);

            _camera.localEulerAngles += new Vector3(x, y, z);
            yield return waitForSecond;
        }

        _camera.localEulerAngles = _originalPosition;
    }

    private void PlayIdle()
    {
        float x;
        float y;
        float z;

        x = Random.Range(-Range, Range) * MultyplyX;
        y = Random.Range(-Range, Range) * MultyplyY;
        z = Random.Range(-Range, Range) * MultyplyZ;

        var tweenRotate = transform.DOLocalRotate(new Vector3(_originalPosition.x + x, y, z), Duration);
        tweenRotate.SetEase(Ease.InOutSine);
        tweenRotate.OnComplete(ResetPosition);
    }

    private void ResetPosition()
    {
        var tweenResetPosition = transform.DOLocalRotate(_originalPosition, Duration);
        tweenResetPosition.SetEase(Ease.InOutSine);
        tweenResetPosition.OnComplete(PlayIdle);
    }
}
