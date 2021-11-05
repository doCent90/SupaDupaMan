using UnityEngine;

public class StickmanSlicer : SlicerType1
{
    private CellsDestroyer _cellsDestroyer;

    public void TakeDamage()
    {
        _isDamaged = true;
    }

    protected override void DisableDafaultObjects()
    {
        _cellsDestroyer.enabled = true;
        enabled = false;
    }

    private void Start()
    {
        _cellsDestroyer = GetComponent<CellsDestroyer>();
    }
}
