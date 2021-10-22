using System.Collections;
using UnityEngine;

public class CameraAnimator : MonoBehaviour
{
    private Transform _camera;
    private Vector3 _originalPosition;
    private bool _isAttack;

    private const float Range = 0.4f;
    private const float Delay = 0.025f;

    private void OnEnable()
    {
        _isAttack = false;
    }

    private void StartShake(bool isAttack)
    {
        _isAttack = isAttack;

        if (!isAttack)
            StopCoroutine(Shake());
        else if (isAttack)
            StartCoroutine(Shake());
    }

    private void Start()
    {
        _camera = GetComponent<Transform>();
        _originalPosition = _camera.transform.localEulerAngles;
    }

    private IEnumerator Shake()
    {
        float y;
        float z;

        var waitForSecond = new WaitForSeconds(Delay);

        while (_isAttack)
        {
            y = Random.Range(-Range, Range);
            z = Random.Range(-Range, Range);

            _camera.localEulerAngles = new Vector3(_originalPosition.x, y, z);
            yield return waitForSecond;
        }

        _camera.localEulerAngles = _originalPosition;
    }
}
