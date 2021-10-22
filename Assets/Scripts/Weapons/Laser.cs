using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    [SerializeField] private GameObject _hitEffect;

    private bool _isReady = false;

    private Vector4 _length = new Vector4(1,1,1,1);
    private LineRenderer _laser;
    private AttackState _attack;
    private ParticleSystem[] _effects;
    private ParticleSystem[] _hits;

    private const float _hitOffset = 0.1f;
    private const float _noiseTextureLength = 0.5f;
    private const float _mainTextureLength = 0.5f;
    private const float _maxLength = 35f;

    private const string MainTexture = "_MainTex";
    private const string Noise = "_Noise";

    private void OnEnable()
    {
        _laser = GetComponent<LineRenderer>();
        _effects = GetComponentsInChildren<ParticleSystem>();
        _hits = _hitEffect.GetComponentsInChildren<ParticleSystem>();
        _attack = GetComponentInParent<AttackState>();

        _attack.Fired += ActivatLaser;

        ResetLaser();
    }

    private void OnDisable()
    {
        _attack.Fired -= ActivatLaser;
    }

    private void Update()
    {
        InitMaterial();
        StartLaser();
    }

    private void ActivatLaser(bool isAttack)
    {
        _isReady = isAttack;
    }

    private void ResetLaser()
    {
        if (_laser != null)
        {
            _laser.enabled = false;
        }

        if (_effects != null)
        {
            foreach (var allPs in _effects)
            {
                if (allPs.isPlaying) allPs.Stop();
            }
        }
    }

    private void StartLaser()
    {
        if (_laser != null && _isReady == true)
        {
            _laser.SetPosition(0, transform.position);
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, _maxLength))
            {
                _laser.SetPosition(1, hit.point);
                _hitEffect.transform.position = hit.point + hit.normal * _hitOffset;
                _hitEffect.transform.rotation = Quaternion.identity;

                foreach (var partical in _effects)
                {
                    if (!partical.isPlaying)
                        partical.Play();
                }

                _length[0] = _mainTextureLength * (Vector3.Distance(transform.position, hit.point));
                _length[2] = _noiseTextureLength * (Vector3.Distance(transform.position, hit.point));
            }
            else
            {
                var endPos = transform.position + transform.forward * _maxLength;
                _laser.SetPosition(1, endPos);
                _hitEffect.transform.position = endPos;

                foreach (var partical in _hits)
                {
                    if (partical.isPlaying)
                        partical.Stop();
                }

                _length[0] = _mainTextureLength * (Vector3.Distance(transform.position, endPos));
                _length[2] = _noiseTextureLength * (Vector3.Distance(transform.position, endPos));
            }

            if (_laser.enabled == false && _isReady == true)
            {
                _laser.enabled = true;
            }
        }
        else
        {
            _laser.enabled = false;
        }
    }

    private void InitMaterial()
    {
        _laser.material.SetTextureScale(MainTexture, new Vector2(_length[0], _length[1]));
        _laser.material.SetTextureScale(Noise, new Vector2(_length[2], _length[3]));
    }
}
