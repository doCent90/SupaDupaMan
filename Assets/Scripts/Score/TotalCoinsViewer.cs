using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TotalCoinsViewer : MonoBehaviour
{
    private TMP_Text _totalcoinsText;

    private const string Coins = "Coins";

    public void Refresh()
    {
        Show();
    }

    private void OnEnable()
    {
        _totalcoinsText = GetComponent<TMP_Text>();

        Show();
    }

    private void Show()
    {
        var currentScore = PlayerPrefs.GetInt(Coins);

        _totalcoinsText.text = $"Bank: {currentScore}";
    }
}
