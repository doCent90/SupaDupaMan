using UnityEngine;

public abstract class Slicer : MonoBehaviour
{
    protected Rigidbody[] Rigidbodies;

    protected float ElapsedTime = 0;
    protected bool IsDamaged = false;

    protected const float DestroingWallTime = 1.5f;
    protected const float DestroingObjectsTime = 0.5f;
    protected const float Range = 10f;

    public void TakeDamage()
    {
        IsDamaged = true;
        InitDamage();
    }

    protected abstract void InitDamage();

    protected abstract void DisableDafaultObjects();

    protected void OnEnable()
    {
        Rigidbodies = GetComponentsInChildren<Rigidbody>();
    }

    private void Update()
    {
        if (!IsDamaged)
            return;
        else
        {
            if (ElapsedTime <= 0)
                Destroy();

            ElapsedTime -= Time.deltaTime;
        }
    }

    private void Destroy()
    {
        float x;
        float y;
        float z;

        foreach (var cell in Rigidbodies)
        {
            x = Random.Range(-Range, Range) * 2;
            y = Random.Range(0, Range) * 2;
            z = Random.Range(-Range, Range);

            cell.isKinematic = false;
            cell.AddForce(new Vector3(x, y, z), ForceMode.Impulse);
        }

        DisableDafaultObjects();
        enabled = false;
    }
}
