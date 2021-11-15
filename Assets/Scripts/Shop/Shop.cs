using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject _itemContainer;
    [Header("Lasers In Shop")]
    [SerializeField] private LaserRenderer2[] _lasers;
    [SerializeField] private LaserView _template;

    private Player _player;
    private LasersActivator _lasersActivator;
    private TotalCoinsViewer _coinsViewer;

    private int _totalCurrentCoins;

    private const string Coins = "Coins";

    private void OnEnable()
    {
        _player = FindObjectOfType<Player>();
        _coinsViewer = GetComponentInChildren<TotalCoinsViewer>();
        _lasersActivator = _player.GetComponentInChildren<LasersActivator>();
        _totalCurrentCoins = PlayerPrefs.GetInt(Coins);
    }

    private void Start()
    {
        for (int i = 0; i < _lasers.Length; i++)
        {
            AddItem(_lasers[i]);
            _lasers[i].SetStatus(isBuyed: false);
        }

        gameObject.SetActive(false);
    }

    private void AddItem(LaserRenderer2 laser)
    {
        var view = Instantiate(_template, _itemContainer.transform);
        view.SellButtonClick += OnSellButtonClick;
        view.UseButtonClick += SetBuyedLaser;
        view.Render(laser);
    }

    private void OnSellButtonClick(LaserRenderer2 laser, LaserView view)
    {
        TrySellLaser(laser, view);
    }

    private void TrySellLaser(LaserRenderer2 laser, LaserView view)
    {
        if (laser.Price <= _totalCurrentCoins)
        {
            _lasersActivator.SetLaser(laser);
            _totalCurrentCoins -= laser.Price;

            PlayerPrefs.SetInt(Coins, _totalCurrentCoins);
            view.SetTextSold();
            view.SellButtonClick -= OnSellButtonClick;

            _coinsViewer.Refresh();
            laser.SetStatus(isBuyed: true);
        }
    }

    private void SetBuyedLaser(LaserRenderer2 laser, LaserView view)
    {
        view.UseButtonClick -= SetBuyedLaser;
        _lasersActivator.SetLaser(laser);
    }
}