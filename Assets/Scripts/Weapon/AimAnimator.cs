using UnityEngine;

[RequireComponent(typeof(AimRenderer))]
public class AimAnimator : MonoBehaviour
{
    private AimMain _aimMain;
    private AimOutOfRangeView _aimOutOfRange;

    private Transform _main;
    private Transform _outOfRange;
    private AimRenderer _renderer;

    private const float OriginaMainlScale = 0.1f;
    private const float OriginaOutlScale = 0.1f;
    private const float OutScale = 0.12f;
    private const float MainScale = 0.12f;
    private const float Speed = 1f;

    private void OnEnable()
    {
        _renderer = GetComponent<AimRenderer>();
        _aimMain = GetComponentInChildren<AimMain>();
        _aimOutOfRange = GetComponentInChildren<AimOutOfRangeView>();

        _main = _aimMain.transform;
        _outOfRange = _aimOutOfRange.transform;

        _renderer.MainEnabled += OnMainActivated;
        _renderer.OutOfRangeEnabled += OnOutOfRangeActivated;
    }

    private void OnDisable()
    {
        _renderer.MainEnabled -= OnMainActivated;
        _renderer.OutOfRangeEnabled -= OnOutOfRangeActivated;
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
