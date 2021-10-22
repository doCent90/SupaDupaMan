using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HotBarFillArea : MonoBehaviour
{
    private Image _image;
    private OverHeatBar _slider;
    private Vector4 _greenStart;
    private Vector4 _redEnd;

    private void OnEnable()
    {
        _slider = GetComponentInParent<OverHeatBar>();
        _image = GetComponent<Image>();

        _slider.ValueChanged += SetTargetColor;

        _greenStart = Color.green;
        _redEnd = Color.red;
    }

    private void OnDisable()
    {
        _slider.ValueChanged -= SetTargetColor;
    }

    private void SetTargetColor(float value)
    {
        _image.color = Vector4.Lerp(_greenStart, _redEnd, value);
    }
}
