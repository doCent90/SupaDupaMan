using UnityEngine;

public class BackGroundMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private PlayerMover _playerMover;

    private void OnEnable()
    {
        _playerMover = FindObjectOfType<PlayerMover>();

        _playerMover.LastPointCompleted += Stop;
    }

    private void OnDisable()
    {
        _playerMover.LastPointCompleted -= Stop;
    }

    private void Update()
    {
        transform.Translate(Vector3.back * _speed * Time.deltaTime);
    }

    private void Stop()
    {
        enabled = false;
    }
}
