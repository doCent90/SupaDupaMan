using UnityEngine;
using DG.Tweening;

public class Environments : MonoBehaviour
{
    [SerializeField] bool _isLastPlatform;
    [SerializeField] private Material _targetMaterial;

    private MeshRenderer _mesh;
    private BoxCollider _boxCollider;
    private PlatformSwitcher _nextPlatform;

    private const float Duration = 1f;
    private const float DistanceLastPlatfom = -241f;
    private const float DistanceStandartPlatform = -256f;

    public bool IsLastPlatform => _isLastPlatform;

    public void MoveDown()
    {
        transform.DOMoveY(DistanceLastPlatfom, Duration);
    }

    private void OnEnable()
    {
        _mesh = GetComponent<MeshRenderer>();
        _boxCollider = GetComponent<BoxCollider>();
        _nextPlatform = GetComponentInParent<PlatformSwitcher>();

        if (_isLastPlatform)
            _boxCollider.enabled = false;

        _nextPlatform.Switched += SetColor;
    }

    private void OnDisable()
    {
        _nextPlatform.Switched -= SetColor;
    }

    private void DropDownPlatform()
    {
        transform.DOMoveY(DistanceStandartPlatform, Duration);
    }

    private void SetColor()
    {
        _mesh.material = _targetMaterial;
        if (!_isLastPlatform)
            DropDownPlatform();
    }
}
