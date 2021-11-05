using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerRotater))]
public class Player : MonoBehaviour
{
    private PlayerMover _mover;
    private StartGame _startGame;
    private PlayerRotater _rotater;
    private ObjectsSelector _objectsSelector;

    private void OnEnable()
    {
        _mover = GetComponent<PlayerMover>();
        _rotater = GetComponent<PlayerRotater>();
        _startGame = FindObjectOfType<StartGame>();
        _objectsSelector = GetComponentInChildren<ObjectsSelector>();

        _startGame.Started += OnStarted;
    }

    private void OnDisable()
    {
        _startGame.Started -= OnStarted;
    }

    private void OnStarted()
    {
        _mover.enabled = true;
        _rotater.enabled = true;
        _objectsSelector.enabled = true;
    }
}
