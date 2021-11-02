using UnityEngine;
using DG.Tweening;

public class CameraMover : MonoBehaviour
{

    private StartGame _startGame;
    private Vector3 _playPosition;
    private CameraAnimator _cameraAnimator;

    private const float Duration = 0.4f;
    private const float StartEuler = 180f;
    private const float StartPositionY = -9f;

    private void OnEnable()
    {
        _playPosition = transform.localEulerAngles;
        _startGame = FindObjectOfType<StartGame>();
        _cameraAnimator = GetComponent<CameraAnimator>();

        LookAtStartRegDoll();
        _startGame.Started += OnStarted;
    }

    private void OnDisable()
    {
        _startGame.Started -= OnStarted;
    }

    private void LookAtStartRegDoll()
    {
        transform.localEulerAngles = new Vector3(0, StartEuler, 0);
        transform.localPosition = new Vector3(0, StartPositionY, 0);
    }

    private void LookAtPlayPosition()
    {
        var tweenMove = transform.DOLocalMoveY(0, Duration);
        tweenMove.SetEase(Ease.InOutSine);

        var tweenLook = transform.DOLocalRotate(_playPosition, Duration);
        tweenLook.SetEase(Ease.InOutSine);
        tweenLook.OnComplete(Disable);
    }

    private void OnStarted()
    {
        LookAtPlayPosition();
    }

    private void Disable()
    {
        _cameraAnimator.enabled = true;
        enabled = false;
    }
}
