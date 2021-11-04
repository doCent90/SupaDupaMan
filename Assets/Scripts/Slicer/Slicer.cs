using UnityEngine;

public abstract class Slicer : MonoBehaviour
{
    protected Rigidbody[] _rigidbodies;

    protected float _elapsedTime = 0;
    protected bool _isDamaged = false;

    protected const float DestroyTime = 1f;
    protected const float Range = 10f;

    protected abstract void DisableDafaultObjects();

    protected void OnEnable()
    {
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
    }

    private void Update()
    {
        if (_isDamaged)
        {
            if (_elapsedTime <= 0)
                Destroy();

            _elapsedTime -= Time.deltaTime;
        }
        else
            return;
    }

    private void Destroy()
    {
        float x;
        float y;
        float z;

        foreach (var cell in _rigidbodies)
        {
            x = Random.Range(-Range, Range) * 2;
            y = Random.Range(-Range, Range);
            z = Random.Range(-Range, Range) * 2;

            cell.isKinematic = false;
            cell.AddForce(new Vector3(x, y, z), ForceMode.Impulse);
        }

        DisableDafaultObjects();
    }
}
