using UnityEngine;

public class CellsDestroyer : MonoBehaviour
{
    private MeshCollider[] _meshColliders;

    private float _elapsedTime;
    private bool _isMeshesTrigger = false;

    private const float DelayDisable = 2f;
    private const float DelayCollidersTriggerOn = 3f;

    private void OnEnable()
    {
        _meshColliders = GetComponentsInChildren<MeshCollider>();
        _elapsedTime = DelayCollidersTriggerOn;
    }

    private void Update()
    {
        if (_isMeshesTrigger == false)
        {
            if (_elapsedTime <= 0)
                SetCollidersTriggerOn();

            _elapsedTime -= Time.deltaTime;
        }
        else if (_isMeshesTrigger == true)
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
        foreach (MeshCollider mesh in _meshColliders)
        {
            mesh.isTrigger = true;
        }

        _isMeshesTrigger = true;
        _elapsedTime = DelayDisable;
    }

    private void Disable()
    {
        foreach (MeshCollider mesh in _meshColliders)
        {
            mesh.gameObject.SetActive(false);
        }

        enabled = false;
    }
}
