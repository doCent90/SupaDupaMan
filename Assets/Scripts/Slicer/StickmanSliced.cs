using UnityEngine;

public class StickmanSliced : Slicer
{
    private StickmanCells _cells;

    public void StartSclice()
    {
        _isDamaged = true;
    }

    protected override void DisableDafaultObjects()
    {
        _cells.enabled = true;
        enabled = false;
    }

    private void Start()
    {
        _cells = GetComponent<StickmanCells>();
    }
}
