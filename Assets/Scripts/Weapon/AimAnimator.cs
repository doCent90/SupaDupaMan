using UnityEngine;

public class AimAnimator : MonoBehaviour
{
    private AimMain _aimMain;
    private AimOutOfRange _aimOutOfRange;

    private Transform _main;
    private Transform _outOfRange;
    private AimRenderer _renderer;

    private const float OriginaMainlScale = 0.4f;
    private const float OriginaOutlScale = 0.06f;
    private const float OutScale = 0.1f;
    private const float MainScale = 0.8f;
    private const float Speed = 1f;

    private void OnEnable()
    {
        _renderer = GetComponent<AimRenderer>();
        _aimMain = GetComponentInChildren<AimMain>();
        _aimOutOfRange = GetComponentInChildren<AimOutOfRange>();

        _main = _aimMain.transform;
        _outOfRange = _aimOutOfRange.transform;

        _renderer.MainAimActivated += OnMainActivated;
        _renderer.OutOfRangeAimActivated += OnOutOfRangeActivated;
    }

    private void OnDisable()
    {
        _renderer.MainAimActivated -= OnMainActivated;
        _renderer.OutOfRangeAimActivated -= OnOutOfRangeActivated;
    }

    private void OnMainActivated(bool isActivate)
    {
        Animate(isActivate, _main, MainScale, OriginaMainlScale);
    }

    private void OnOutOfRangeActivated(bool isActivate)
    {
        Animate(isActivate, _outOfRange, OutScale, OriginaOutlScale);
    }

    private void Animate(bool isReady, Transform aimTransform, float scale, float originalScale)
    {
        if (isReady)
        {
            if (aimTransform.localScale.x != scale)
            {
                aimTransform.localScale = Vector3.MoveTowards(aimTransform.localScale, new Vector3(scale, scale, scale), Speed * Time.deltaTime);
            }
        }
        else
        {
            aimTransform.localScale = new Vector3(originalScale, originalScale, originalScale);
        }
    }
}
