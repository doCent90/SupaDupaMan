using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Button))]
public class LaserView : MonoBehaviour
{
    [SerializeField] private Image _icon;

    private TMP_Text _priceText;
    private Button _sellButton;
    private LaserRenderer2 _laserRenderer;
    private int _price;

    private const string Sold = "SOLD";

    public event UnityAction<LaserRenderer2, LaserView> SellButtonClick;
    public event UnityAction<LaserRenderer2, LaserView> UseButtonClick;

    public void Render(LaserRenderer2 laser)
    {
        _laserRenderer = laser;
        _icon.sprite = laser.Icon;

        _price = laser.Price;
        _priceText.text = _price.ToString();
    }

    public void SetTextSold()
    {
        _priceText.text = Sold;
    }

    private void OnEnable()
    {
        _sellButton = GetComponent<Button>();
        _priceText = GetComponentInChildren<TMP_Text>();

        _sellButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        if(_laserRenderer.IsBuyed == false)
            SellButtonClick?.Invoke(_laserRenderer, this);
        else if(_laserRenderer.IsBuyed)
            UseButtonClick?.Invoke(_laserRenderer, this);
    }
}
