using UnityEngine;

public class LaserRenderer2 : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _icon;
    [SerializeField] private Sprite _soldIcon;
    [Header("Laser Settings")]
    [SerializeField] private Color laserColor = new Vector4(1,1,1,1);
    [SerializeField] private GameObject HitEffect;
    [SerializeField] private GameObject FlashEffect;

    private ParticleSystem _laserParticalSysytem;
    private ParticleSystem.Particle[] _particles;
    private Vector3[] _particlesPositions;
    private ParticleSystem[] _flashes;
    private ParticleSystem[] _hits;
    private Material _laserMat;

    private float _dissovleTimer = 0;
    private bool _isStartDissovle = false;
    private bool _updateSaver = false;
    private int _particleCount;

    private const float HitOffset = 0.1f;
    private const float LaserScale = 1;
    private const float MaxLength = 85f;

    private const string StartPoint = "_StartPoint";
    private const string Distance = "_Distance";
    private const string EndPoint = "_EndPoint";
    private const string Dissolve = "_Dissolve";
    private const string Scale = "_Scale";

    private const int True = 1;

    public string Name => _name;
    public int Price => _price;
    public Sprite Icon => _icon;
    public Sprite SoldIcon => _soldIcon;
    public bool IsBought { get; private set; }

    public void DisablePrepare()
    {
        _dissovleTimer = 0;
        _isStartDissovle = true;
        _updateSaver = true;
        transform.parent = null;

        if (_flashes != null && _hits != null)
        {
            foreach (var AllHits in _hits)
            {
                if (AllHits.isPlaying) AllHits.Stop();
            }
            foreach (var AllFlashes in _flashes)
            {
                if (AllFlashes.isPlaying) AllFlashes.Stop();
            }
        }
    }

    public void SetBuyStatus()
    {
        IsBought = true;
        _icon = _soldIcon;
        PlayerPrefs.SetInt(_name, True);
    }

    private void Start()
    {
        _laserParticalSysytem = GetComponent<ParticleSystem>();
        _laserMat = GetComponent<ParticleSystemRenderer>().material;
        _flashes = FlashEffect.GetComponentsInChildren<ParticleSystem>();
        _hits = HitEffect.GetComponentsInChildren<ParticleSystem>();

        _laserMat.SetFloat(Scale, LaserScale);

        if (PlayerPrefs.GetInt(_name) == 1)
            SetBuyStatus();
    }

    private void Update()
    {
        if (_laserParticalSysytem != null && _updateSaver == false)
        {
            _laserMat.SetVector(StartPoint, transform.position);

            RaycastHit rayHit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out rayHit, MaxLength))
            {
                _particleCount = Mathf.RoundToInt(rayHit.distance / (2 * LaserScale));
                if (_particleCount < rayHit.distance / (2 * LaserScale))
                {
                    _particleCount += 1;
                }
                _particlesPositions = new Vector3[_particleCount];
                AddParticles();

                _laserMat.SetFloat(Distance, rayHit.distance);
                _laserMat.SetVector(EndPoint, rayHit.point);
                if (_hits != null)
                {
                    HitEffect.transform.position = rayHit.point + rayHit.normal * HitOffset;
                    HitEffect.transform.LookAt(rayHit.point);
                    foreach (var hit in _hits)
                    {
                        if (!hit.isPlaying) hit.Play();
                    }
                    foreach (var flash in _flashes)
                    {
                        if (!flash.isPlaying) flash.Play();
                    }
                }
            }
            else
            {
                var endPosition = transform.position + transform.forward * MaxLength;
                var distance = Vector3.Distance(endPosition, transform.position);
                _particleCount = Mathf.RoundToInt(distance / (2 * LaserScale));
                if (_particleCount < distance / (2 * LaserScale))
                {
                    _particleCount += 1;
                }
                _particlesPositions = new Vector3[_particleCount];
                AddParticles();

                _laserMat.SetFloat(Distance, distance);
                _laserMat.SetVector(EndPoint, endPosition);
                if (_hits != null)
                {
                    HitEffect.transform.position = endPosition;
                    foreach (var AllPs in _hits)
                    {
                        if (AllPs.isPlaying) AllPs.Stop();
                    }
                }
            }          
        }

        if (_isStartDissovle)
        {
            _dissovleTimer += Time.deltaTime;
            _laserMat.SetFloat(Dissolve, _dissovleTimer * 5);
        }
    }

    private void AddParticles()
    {
        _particles = new ParticleSystem.Particle[_particleCount];

        for (int i = 0; i < _particleCount; i++)
        {
            _particlesPositions[i] = Vector3.zero + new Vector3(0f, 0f, i * 2 * LaserScale);
            _particles[i].position = _particlesPositions[i];
            _particles[i].startSize3D = new Vector3(0.001f, 0.001f, 2 * LaserScale);
            _particles[i].startColor = laserColor;
        }
        _laserParticalSysytem.SetParticles(_particles, _particles.Length);
    }
}
