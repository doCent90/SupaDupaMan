using UnityEngine;

public class CellsDestroyer : MonoBehaviour
{
    private MeshCollider[] _meshColliders;

    private float _elapsedTime;
    private bool _isDestroy = false;
    private bool _isMeshesTrigger = false;

    private const float DelayDisable = 2f;
    private const float DelayCollidersTriggerOn = 4f;

    private void OnEnable()
    {
        _meshColliders = GetComponentsInChildren<MeshCollider>();
        _elapsedTime = DelayCollidersTriggerOn;
    }

    private void Update()
    {
        if (!_isMeshesTrigger)
        {
            if (_elapsedTime <= 0)
                SetCollidersTriggerOn();

            _elapsedTime -= Time.deltaTime;
        }
        else if (!_isDestroy)
        {
            if (_elapsedTime <= 0)
                Disable();

            _elapsedTime -= Time.deltaTime;
        }
        else
            return;
    }

    private void SetCollidersTriggerOn()
    {
        foreach (var mesh in _meshColliders)
        {
            mesh.isTrigger = true;
        }

        _isMeshesTrigger = true;
        _elapsedTime = DelayDisable;
    }

    private void Disable()
    {
        foreach (var mesh in _meshColliders)
        {
            mesh.gameObject.SetActive(false);
        }

        enabled = false;
    }
}