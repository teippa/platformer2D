using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour, ObjectPooler.IPooledObject
{

    [SerializeField] private float _bulletSpeed;
    private Rigidbody2D _body;
    private SpriteRenderer _sprite;
    private Collider2D _collider;

    [SerializeField] private ParticleSystem _bulletHitParticles;
    

    public void OnObjectSpawn()
    {
        BulletEnabled(true);
        float r = _body.rotation * Mathf.Deg2Rad;
        _body.velocity = new Vector2(Mathf.Cos(r), Mathf.Sin(r)) * _bulletSpeed;
    }
    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _collider = GetComponent<CapsuleCollider2D>();

        /*
        float r = _body.rotation * Mathf.Deg2Rad;

        _body.velocity = new Vector2(Mathf.Cos(r), Mathf.Sin(r)) * _bulletSpeed;
        */
    }

    void FixedUpdate()
    {
        if (_body.position.magnitude > 50)
        {
            //BulletEnabled(false);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage();
        }


        //collision.GetComponent<PlayerController>()
        if (collision.tag != "Player")
        {
            StartCoroutine(DelayedDeactivate());
        }

    }
    IEnumerator DelayedDeactivate()
    {
        BulletEnabled(false);

        _bulletHitParticles.transform.position = _body.position;
        _bulletHitParticles.transform.rotation = _body.transform.rotation;
        _bulletHitParticles.Emit(50);

        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }

    public void BulletEnabled(bool _enabled)
    {
        if (!_enabled)
            _body.velocity = Vector2.zero;

        _collider.enabled = _enabled;
        _sprite.enabled = _enabled;
        _body.simulated = _enabled;
    }
}
