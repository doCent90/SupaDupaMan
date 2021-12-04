using UnityEngine;

[RequireComponent(typeof(CellsDestroyer))]
public class StickmanSlicer : Slicer
{
    private CellsDestroyer _cellsDestroyer;

    protected override void DisableDafaultObjects()
    {
        _cellsDestroyer.enabled = true;
    }

    protected override void InitDamage() { }

    private void Start()
    {
        _cellsDestroyer = GetComponent<CellsDestroyer>();
    }
}
