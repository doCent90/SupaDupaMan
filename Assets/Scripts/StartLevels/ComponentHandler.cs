using UnityEngine;

[RequireComponent(typeof(StartGame))]
[RequireComponent(typeof(LevelsLoader))]
public class ComponentHandler : MonoBehaviour
{
    [SerializeField] private UI _uI;
    [SerializeField] private Player _player;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameWin _gameWin;
    [SerializeField] private Enemies _enemies;
    [SerializeField] private Environments _environments;
    [SerializeField] private SoundsFXSettings _soundsFXSettings;

    private StartGame _startGame;
    private LevelsLoader _levelsLoader;

    public UI UI => _uI;
    public Player Player => _player;
    public Camera Camera => _camera;
    public GameWin GameWin => _gameWin;
    public Enemies Enemies => _enemies;
    public StartGame StartGame => _startGame;
    public LevelsLoader LevelsLoader => _levelsLoader;
    public Environments Environments => _environments;
    public SoundsFXSettings SoundsFXSettings => _soundsFXSettings;


    private void OnEnable()
    {
        _startGame = GetComponent<StartGame>();
        _levelsLoader = GetComponent<LevelsLoader>();
    }
}

