using UnityEngine;

[RequireComponent(typeof(GameWin))]
[RequireComponent(typeof(StartGame))]
[RequireComponent(typeof(LevelsLoader))]
public class ComponentHandler : MonoBehaviour
{
    [SerializeField] private UI _uI;
    [SerializeField] private Player _player;
    [SerializeField] private Camera _camera;
    [SerializeField] private Enemies _enemies;
    [SerializeField] private Environments _environments;
    [SerializeField] private EndLevelFyPoint _endLevelFyPoint;
    [SerializeField] private SoundsFXSettings _soundsFXSettings;

    private GameWin _gameWin;
    private StartGame _startGame;
    private LevelsLoader _levelsLoader;

    public UI UI => _uI;
    public Player Player { get; private set; }
    public Camera Camera { get; private set; }
    public GameWin GameWin { get; private set; }
    public Enemies Enemies { get; private set; }
    public StartGame StartGame { get; private set; }
    public LevelsLoader LevelsLoader { get; private set; }
    public Environments Environments { get; private set; }
    public EndLevelFyPoint EndLevelFyPoint { get; private set; }
    public SoundsFXSettings SoundsFXSettings { get; private set; }


    private void OnEnable()
    {
        _gameWin = GetComponent<GameWin>();
        _startGame = GetComponent<StartGame>();
        _levelsLoader = GetComponent<LevelsLoader>();

        Player = _player;
        Camera = _camera;
        GameWin = _gameWin;
        Enemies = _enemies;
        StartGame = _startGame;
        LevelsLoader = _levelsLoader;
        Environments = _environments;
        EndLevelFyPoint = _endLevelFyPoint;
        SoundsFXSettings = _soundsFXSettings;
    }
}

