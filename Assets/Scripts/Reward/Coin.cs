using UnityEngine;
using DG.Tweening;

public class Coin : MonoBehaviour
{
    private Transform _scorePosition;
    private CoinScaler _coinScaler;

    private const float Duration = 1f;
    private const float CanvasSize = 0.15f;

    private void OnEnable()
    {
        _scorePosition = FindObjectOfType<ScoreCoinPlace>().transform;
        _coinScaler = FindObjectOfType<CoinScaler>();

        Rotate();
        Move();
    }

    private void Move()
    {
        var size = new Vector3(CanvasSize, CanvasSize, 0);

        var tweenScale = transform.DOScale(size, Duration);
        var tweenMove = transform.DOMove(_scorePosition.position, Duration).SetEase(Ease.InOutSine);

        tweenMove.OnComplete(ScaleDown);
    }

    private void ScaleDown()
    {
        _coinScaler.Increase();

        var tweenScale = transform.DOScale(0, Duration);
        tweenScale.OnComplete(Delete);
    }

    private void Rotate()
    {
        transform.rotation = _scorePosition.rotation;
    }

    private void Delete()
    {
        Destroy(gameObject);
    }
}
