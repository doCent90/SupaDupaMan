using UnityEngine;
using UnityEngine.Events;

public class FixedHandPointer : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _clickDelay = 0.1f;
    [SerializeField] private float _hitDelay = 0.1f;
    [SerializeField] private HandAnimatorEventListener _animationEvents;

    private int _anger = 1;
    private int _downAnger;

    public event UnityAction<Vector2> MouseDown;
    public event UnityAction<Vector2> MouseUp;
    public bool IsPressing { get; private set; } = false;


    public void Play(string animation) => _animator.SetTrigger(animation);

    public void ResetAngry() => _anger = 1;

    public void AddAngry() => _anger = _anger == 3 ? 1 : _anger + 1;

    private void OnEnable()
    {
        _animationEvents.HandPressed += HandlePress;
    }

    private void OnDisable()
    {
        _animationEvents.HandPressed -= HandlePress;
    }

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.visible = false;
            string animation = HandAnimations.MouseDown;
            switch (_anger)
            {
                case 2:
                    animation = HandAnimations.MouseDownAngry;
                    break;
                case 3:
                    animation = HandAnimations.MouseDownHit;
                    break;
                default:
                    break;
            }
            _downAnger = _anger;
            _animator.SetTrigger(animation);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            string animation = HandAnimations.MouseUp;
            switch (_downAnger)
            {
                case 2:
                    animation = HandAnimations.MouseUpAngry;
                    break;
                case 3:
                    animation = HandAnimations.MouseUpHit;
                    break;
                default:
                    break;
            }
            _animator.SetTrigger(animation);
        }

        IsPressing = Input.GetMouseButton(0);

        if (Input.GetKeyDown(KeyCode.Alpha1))
            Play(HandAnimations.Angry);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            Play(HandAnimations.Ok);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            Play(HandAnimations.ThumbUp);

        if (Input.GetKeyDown(KeyCode.UpArrow))
            AddAngry();
    }

    private void HandlePress()
    {
        MouseDown?.Invoke(Input.mousePosition);
    }
}
