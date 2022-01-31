using UnityEngine;
using Cinemachine;
using System;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] private LasersActivator _lasersActivator;

    private CinemachineVirtualCamera _virtualCamera;

    private const int On = 1;
    private const int Off = 0;

    private void OnEnable()
    {
        if (_lasersActivator == null)
            throw new NullReferenceException(nameof(LasersActivator));

        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _lasersActivator.Fired += TryShake;
    }

    private void OnDisable()
    {
        _lasersActivator.Fired -= TryShake;
    }

    private void TryShake(bool isFire)
    {
        if (isFire)
            _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = On;
        else
            _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = Off;
    }
}
