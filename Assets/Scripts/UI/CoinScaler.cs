using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class CoinScaler : MonoBehaviour
{
    private bool _isReady = true;

    private const float ScaleUp = 1.2f;
    private const float ScaleDown = 0.8f;
    private const float Duration = 0.15f;

    public event UnityAction Rewarded;

    public void Increase()
    {
        Rewarded?.Invoke();

        if (_isReady)
        {
            _isReady = false;
            Reduce();
            var tweeeUp = transform.DOScale(ScaleUp, Duration);
            tweeeUp.OnComplete(Reduce);
        }
    }

    private void Reduce()
    {
        var tweeeDown = transform.DOScale(ScaleDown, Duration / 2);
        tweeeDown.OnComplete(SetReady);
    }

    private void SetReady()
    {
        _isReady = true;
    }
}
