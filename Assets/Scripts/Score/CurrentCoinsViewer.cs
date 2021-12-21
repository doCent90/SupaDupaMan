using UnityEngine;
using TMPro;

public class CurrentCoinsViewer : MonoBehaviour
{
    private GameWin _gameWin;
    private TMP_Text _coinsTxt;
    private CoinScaler _coinScaler;

    private int _currentCoinsCount = 0;

    private const int Multiply = 2;
    private const string Coins = "Coins";

    private void OnEnable()
    {
        _coinScaler = GetComponentInChildren<CoinScaler>();
        _coinsTxt = GetComponentInChildren<TMP_Text>();
        _gameWin = FindObjectOfType<GameWin>();

        Show();

        _gameWin.Won += SafeScore;
        _coinScaler.Rewarded += AddCoin;
    }

    private void OnDisable()
    {
        _gameWin.Won -= SafeScore;
        _coinScaler.Rewarded -= AddCoin;
    }

    private void AddCoin()
    {
        _currentCoinsCount += Multiply;
        Show();
    }

    private void Show()
    {
        int totalCoins = PlayerPrefs.GetInt(Coins);
        _coinsTxt.text = (totalCoins + _currentCoinsCount).ToString();
    }

    private void SafeScore()
    {
        int totalCoins = PlayerPrefs.GetInt(Coins);
        totalCoins += _currentCoinsCount;

        PlayerPrefs.SetInt(Coins, totalCoins);
    }
}
