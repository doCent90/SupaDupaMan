using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class MoveState : StatePlayer
{
    private PlayerMover _playerMover;

    private void OnEnable()
    {
        _playerMover = GetComponent<PlayerMover>();
        _playerMover.enabled = true;
    }

    private void OnDisable()
    {
        _playerMover.enabled = false;
    }
}
