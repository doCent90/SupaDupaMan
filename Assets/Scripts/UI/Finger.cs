using UnityEngine;

public class Finger : MonoBehaviour
{
    [SerializeField] private ButtonsUI _buttonsUI;

    private Animator _animator;
    private StartGame _startGame;
    private PlayerRotater _playerRotater;
    private SpriteRenderer _spriteRenderer;

    private void OnEnable()
    {
        _startGame = _buttonsUI.StartGame;
        _playerRotater = _buttonsUI.PlayerRotater;

        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _startGame.Started += OnGameStarted;
        _playerRotater.Started += OnFirstAction;
    }

    private void OnDisable()
    {
        _startGame.Started -= OnGameStarted;
        _playerRotater.Started -= OnFirstAction;
    }

    private void OnGameStarted()
    {
        _animator.enabled = true;
        _spriteRenderer.enabled = true;
    }

    private void OnFirstAction()
    {
        _animator.enabled = false;
        _spriteRenderer.enabled = false;
    }
}
