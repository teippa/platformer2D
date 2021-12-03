using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private Rigidbody2D _body;
    //private BoxCollider2D _bodyCollider;
    private SpriteRenderer _sprite;

    [SerializeField] private Transform _firePoint;
    //[SerializeField] private GameObject _bulletPool;

    ObjectPooler objectPooler;


    [SerializeField] private ParticleSystem _ps;

    [SerializeField] private float _laserForce;
    [SerializeField] private float _laserMaxCooldown;
    [SerializeField] private float _maxVelocity;

    [SerializeField] private Camera _cam;

    private Vector2 _mousePos;
    private Vector2 _mouseDir;
    private bool _shootLaser;
    private bool _shootBullet;

    private float _laserCooldown;

    private readonly string laserButton = "Fire2";
    private readonly string shootButton = "Fire1";

    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        if (_cam == null)
        {
            Debug.LogWarning("Camera not selected to object " + gameObject.name + "!");
        }
        _sprite = GetComponent<SpriteRenderer>();
        _body = GetComponent<Rigidbody2D>();

        ResetLaserCooldown();

        objectPooler = ObjectPooler.SharedInstance;
        audioManager = FindObjectOfType<AudioManager>();
        _shootLaser = false;
        _shootBullet = false;
    }

    private void OnDisable()
    {
        if (audioManager != null)
        {
            audioManager.StopSoundFade("PlayerLaserLoop", 100);
        }
    }

    // Update is called once per frame
    void Update()
    {
        _mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        _mouseDir = (_mousePos - _body.position).normalized;

        if (!_shootBullet)
            _shootBullet = Input.GetButtonDown(shootButton);

        if (Input.GetButton(laserButton) && LaserReady()) // Start laser if button pressed and laser ready
        {
            _shootLaser = true;
            _ps.Play();
            audioManager.PlaySound("PlayerLaserLoop");
        }
        if (Input.GetButtonUp(laserButton) || _laserCooldown <= 0) // Stop laser if button released or laser depleted
        {
            _shootLaser = false;
            _ps.Stop();
            audioManager.StopSoundFade("PlayerLaserLoop", 300);
        }

    }


    private bool LaserReady()
    {
        return _laserCooldown >= _laserMaxCooldown;
    }

    private void FixedUpdate()
    {
        if (_shootBullet)
        {
            normalShooting();
            _shootBullet = false;
        }
        


        if (_shootLaser)
        {
            laserShooting();
            _laserCooldown -= Time.fixedDeltaTime;
            _sprite.color = Color.Lerp(Color.black, Color.white, _laserCooldown / _laserMaxCooldown);
        }
        else if (!LaserReady())
        {
            _laserCooldown += Time.fixedDeltaTime;
        }else
        {
            _sprite.color = Color.white;
        }
    }
    private void normalShooting()
    {
        audioManager.PlaySound("PlayerShoot");
        objectPooler.SpawnFromPool("Bullet", _firePoint.position, _firePoint.rotation);

        //objectPooler.GetPooledObject();
    }


    private void laserShooting()
    {
        Vector2 laserPushDir = -_mouseDir;

        // Detect when pushing to the same direction as the body velocity
        float pushRate = Vector2.Dot(_body.velocity.normalized, laserPushDir);
        //if (pushRate < 0) pushRate = 0;
        pushRate = CutNegatives(pushRate);

        Vector2 pushForce = laserMultiplier(pushRate) * laserPushDir;

        _body.AddForce(pushForce, ForceMode2D.Force);


        // Initial push
        if (_laserCooldown >= _laserMaxCooldown)
        {
            _body.AddForce(pushForce * 0.2f, ForceMode2D.Impulse);
            _laserCooldown = _laserMaxCooldown * 0.5f;

        }//*/

    }

    public void ResetLaserCooldown()
    {
        _laserCooldown = _laserMaxCooldown;
    }

    float laserMultiplier(float x)
    {
        float multiplier = -1 * x * x * (0.4f / _maxVelocity) + _laserForce;
        return CutNegatives(multiplier);
    }

    private float CutNegatives(float num)
    {
        return num > 0 ? num : 0;
    }
}
