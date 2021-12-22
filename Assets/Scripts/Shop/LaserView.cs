using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class LaserView : MonoBehaviour
{
    [Header("Laser Icon Component")]
    [SerializeField] private Image _icon;
    [Header("BackGround Target Cell")]
    [SerializeField] private Sprite _targetSprite;

    private LaserRenderer2 _laserRenderer;
    private Image _backGroundSpriteCell;
    private Sprite _defaultSprite;
    private TMP_Text _priceText;
    private Button _sellButton;

    private string _name;
    private int _price;

    private const string Sold = "SOLD";
    private const int True = 1;
    private const int False = 0;

    public Sprite BackGroundSpriteCellTarget => _targetSprite;
    public Sprite BackGroundSpriteCellDefault => _defaultSprite;

    public event Action<LaserRenderer2, LaserView> SellButtonClick;
    public event Action<LaserRenderer2, LaserView> UseButtonClick;

    public void Render(LaserRenderer2 laser)
    {
        _laserRenderer = laser;
        _icon.sprite = laser.Icon;
        _name = laser.Name;

        _price = laser.Price;
        _priceText.text = _price.ToString();

        if (PlayerPrefs.GetInt(_name) == True)
        {
            SetTextSold();
        }
    }

    public void SetTextSold()
    {
        _priceText.text = Sold;
        _icon.sprite = _laserRenderer.SoldIcon;
    }

    public void SetBackGroundSprite(Sprite sprite)
    {
        _backGroundSpriteCell.sprite = sprite;
    }

    private void OnEnable()
    {
        _sellButton = GetComponent<Button>();
        _backGroundSpriteCell = GetComponent<Image>();
        _priceText = GetComponentInChildren<TMP_Text>();

        _defaultSprite = _backGroundSpriteCell.sprite;
        _sellButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        if(_laserRenderer.IsBought == false)
        {
            SellButtonClick?.Invoke(_laserRenderer, this);
            PlayerPrefs.SetInt(_name, True);
        }
        else if (_laserRenderer.IsBought)
        {
            UseButtonClick?.Invoke(_laserRenderer, this);
        }
    }
}
