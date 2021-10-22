using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Slider))]
public class OverHeatBar : MonoBehaviour
{
    private Slider _slider;
    private AttackState _attack;
    private Text _overHeatText;

    private bool _isReady;
    private float _value;

    private const float _multiUp = 2f;
    private const float _multiDown = 1f;

    public event UnityAction<bool> OverHeated;
    public event UnityAction<float> ValueChanged;

    private void OnEnable()
    {
        _attack = FindObjectOfType<AttackState>();
        _slider = GetComponent<Slider>();
        _overHeatText = GetComponentInChildren<Text>();

        _attack.Fired += Activate;
    }

    private void OnDisable()
    {
        _attack.Fired -= Activate;
    }

    private void Start()
    {
        _overHeatText.enabled = false;
    }

    private void Activate(bool isAttack)
    {
        _isReady = isAttack;
    }

    private void Fill()
    {
        if (_isReady)
        {
            if (_value <= 1)
                _value += Time.deltaTime * _multiUp;
        }
        else if (!_isReady)
        {
            if (_value > 0)
                _value -= Time.deltaTime * _multiDown;
        }

        ValueChanged?.Invoke(_value);
        _slider.value = _value;
    }

    private void ShowTextOverHeat()
    {
        if (_slider.value == 1)
        {
            _overHeatText.enabled = true;
            OverHeated?.Invoke(true);
        }

        if (_slider.value == 0)
        {
            _overHeatText.enabled = false;
            OverHeated?.Invoke(false);
        }
    }

    private void Update()
    {
        Fill();
        ShowTextOverHeat();
    }
}
