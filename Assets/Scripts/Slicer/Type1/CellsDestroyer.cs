using UnityEngine;

public class CellsDestroyer : MonoBehaviour
{
    private MeshCollider[] _meshColliders;

    private float _elapsedTime;
    private bool _isMeshesTrigger = false;
    private bool _isDestroy = false;

    private const float DelayColliderTriggerOff = 4f;
    private const float DelayDestroy = 2f;

    private void OnEnable()
    {
        _meshColliders = GetComponentsInChildren<MeshCollider>();
        _elapsedTime = DelayColliderTriggerOff;
    }

    private void Update()
    {
        if (!_isMeshesTrigger)
        {
            if (_elapsedTime <= 0)
                SetColliderTrigger();

            _elapsedTime -= Time.deltaTime;
        }
        else if (!_isDestroy)
        {
            if (_elapsedTime <= 0)
                Destroy();

            _elapsedTime -= Time.deltaTime;
        }
    }

    private void SetColliderTrigger()
    {
        foreach (var mesh in _meshColliders)
        {
            mesh.isTrigger = true;
        }

        _isMeshesTrigger = true;
        _elapsedTime = DelayDestroy;
    }

    private void Destroy()
    {
        foreach (var mesh in _meshColliders)
        {
            mesh.gameObject.SetActive(false);
        }

        enabled = false;
    }
}