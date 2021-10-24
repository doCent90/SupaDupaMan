using System.Collections;
using UnityEngine;

public class CameraAnimator : MonoBehaviour
{
    private Transform _camera;
    private Vector3 _originalPosition;
    private LaserActivator _lasers;

    private bool _isAttack;

    private const float Range = 0.3f;
    private const float Delay = 0.025f;

    private void OnEnable()
    {
        _lasers = FindObjectOfType<LaserActivator>();
        _camera = GetComponent<Transform>();

        _originalPosition = _camera.transform.localEulerAngles;

        _lasers.Fired += StartShake;
        _isAttack = false;
    }

    private void OnDisable()
    {
        _lasers.Fired -= StartShake;
    }

    private void StartShake(bool isAttack)
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
}
