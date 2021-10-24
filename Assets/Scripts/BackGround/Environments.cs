using UnityEngine;
using DG.Tweening;

public class Environments : MonoBehaviour
{
    [SerializeField] bool _isLastPlatform;

    private BoxCollider _boxCollider;

    private const float Duration = 1f;
    private const float Distance = -256f;

    public bool IsLastPlatform => _isLastPlatform;

    public void MoveDown()
    {
        transform.DOMoveY(Distance, Duration);
    }

    private void OnEnable()
    {
        _boxCollider = GetComponent<BoxCollider>();

        if (_isLastPlatform)
            _boxCollider.enabled = false;
    }
}
