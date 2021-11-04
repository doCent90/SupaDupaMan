using UnityEngine;

public class SlicedWallTile : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private MeshCollider _meshCollider;

    private float _elapsedTime;

    private bool _isMeshesTrigger = false;
    private bool _isDestroy = false;

    private const float Range = 10f;
    private const float Multyply = 10f;
    private const float DestroyTime = 4f;
    private const float TriggerActiveTime = 3f;

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _meshCollider = GetComponent<MeshCollider>();

        AddForce();

        _rigidbody.mass = _rigidbody.mass * Multyply;
        _elapsedTime = TriggerActiveTime;
    }

    private void Update()
    {
        if (!_isMeshesTrigger)
        {
            if (_elapsedTime <= 0)
                SetColliderTrigger();

            _elapsedTime -= Time.deltaTime;
        }
        else if (_isDestroy)
        {
            if (_elapsedTime <= 0)
                Destroy();

            _elapsedTime -= Time.deltaTime;
        }
    }

    private void AddForce()
    {
        float x;
        float y;
        float z;

        x = Random.Range(-Range, Range) * 2;
        y = Random.Range(-Range, Range);
        z = Random.Range(-Range, Range) * 2;

        _rigidbody.AddForce(new Vector3(x, y, z), ForceMode.Impulse);
    }

    private void SetColliderTrigger()
    {
        _meshCollider.isTrigger = true;
        _isDestroy = true;
        _isMeshesTrigger = true;
        _elapsedTime = DestroyTime;
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
        enabled = false;
    }
}
