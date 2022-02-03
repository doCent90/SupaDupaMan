using System;
using UnityEngine;

public class Shop : AmplitudeWriter
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _itemContainer;
    [SerializeField] private CurrentCoinsViewer _currentCoinsViewer;
    [Header("Lasers In Shop")]
    [SerializeField] private LaserRenderer2[] _lasers;
    [SerializeField] private LaserView _template;

    private LasersActivator _laserActivator;
    private TotalCoinsViewer _coinsViewer;
    private LaserView[] _lasersView;
    private Sprite _defaultSprite;
    private Sprite _targetSprite;

    private int _totalCurrentCoins;

    private const string Coins = "Coins";
    private const string SoftSpent = "soft_spent";
    private const string CurrentSoft = "current_soft";
    private const string LastUsedLaser = "LastUsedLaser";

    private void OnEnable()
    {
        if (_player == null || _itemContainer == null || _lasers == null || _template == null || _currentCoinsViewer == null)
            throw new NullReferenceException(nameof(Shop));

        _coinsViewer = GetComponentInChildren<TotalCoinsViewer>();
        _laserActivator = _player.GetComponentInChildren<LasersActivator>();
        _totalCurrentCoins = PlayerPrefs.GetInt(Coins);

        UpdateScore();
        Fill();

        _currentCoinsViewer.ScoreChanged += UpdateScore;
    }

    private void OnDisable()
    {
        _currentCoinsViewer.ScoreChanged -= UpdateScore;

        foreach (LaserView view in _lasersView)
        {
            view.UseButtonClick -= SetBuyedLaser;
        }
    }

    private void OnApplicationQuit()
    {
        SetAmplitudeValue(CurrentSoft, _totalCurrentCoins);
    }

    private void AddItem(LaserRenderer2 laser, out LaserView laserView)
    {
        LaserView view = Instantiate(_template, _itemContainer.transform);
        view.SellButtonClick += OnSellButtonClick;
        view.UseButtonClick += SetBuyedLaser;
        view.Render(laser);
        laserView = view;
    }

    private void Fill()
    {
        _lasersView = new LaserView[_lasers.Length];

        for (int i = 0; i < _lasers.Length; i++)
        {
            AddItem(_lasers[i], out LaserView laserView);
            _lasersView[i] = laserView;
            _targetSprite = laserView.BackGroundSpriteCellTarget;
            _defaultSprite = laserView.BackGroundSpriteCellDefault;
        }
    }

    private void TrySellLaser(LaserRenderer2 laser, LaserView view)
    {
        if (laser.Price <= _totalCurrentCoins)
        {
            _laserActivator.SetLaser(laser);
            _totalCurrentCoins -= laser.Price;

            PlayerPrefs.SetInt(Coins, _totalCurrentCoins);
            SetBackGroundCellDefault();

            view.SetTextSold();
            view.SetBackGroundSprite(_targetSprite);
            view.SellButtonClick -= OnSellButtonClick;

            UpdateScore();
            laser.SetBuyStatus();

            PlayerPrefs.SetString(LastUsedLaser, laser.Name);

            SetAmplitudeValue(SoftSpent, laser.Price, "Shop");
            SetAmplitudeValue(SoftSpent, laser.Price, laser.Name);
            SetAmplitudeValue(SoftSpent, laser.Price, laser.Price.ToString());
        }
    }

    private void SetBuyedLaser(LaserRenderer2 laser, LaserView view)
    {
        SetBackGroundCellDefault();
        view.SetBackGroundSprite(_targetSprite);
        _laserActivator.SetLaser(laser);

        PlayerPrefs.SetString(LastUsedLaser, laser.Name);
    }

    private void UpdateScore()
    {
        _coinsViewer.Refresh();
    }

    private void SetBackGroundCellDefault()
    {
        foreach (LaserView view in _lasersView)
        {
            view.SetBackGroundSprite(_defaultSprite);
        }
    }

    private void OnSellButtonClick(LaserRenderer2 laser, LaserView view)
    {
        TrySellLaser(laser, view);
    }
}
