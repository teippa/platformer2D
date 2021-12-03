using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    [SerializeField] private float _initialVelocity;

    private Rigidbody2D _body;

    private EnemyHealth _health;

    private float _spawnAreaY;
    private float _spawnAreaWidthBy2;

    private BoxCollider2D _parent;
    private PlayerController _player;


    // Start is called before the first frame update
    void Start()
    {
        //_alive = true;
        _health = GetComponent<EnemyHealth>();

        _body = GetComponent<Rigidbody2D>();

        _parent = GetComponentInParent<BoxCollider2D>();
        _player = FindObjectOfType<PlayerController>();


        if (_parent != null)
        {
            _spawnAreaY = _parent.transform.position.y;
            _spawnAreaWidthBy2 = _parent.size.x / 2;
        }
        else
        {
            _spawnAreaY = _body.transform.position.y;
            _spawnAreaWidthBy2 = 0;
        }

        _body.position = getRandomXfixedY(_spawnAreaY);
        _body.velocity = Vector2.down * _initialVelocity;


    }

    // Update is called once per frame
    void Update()
    {
        if (_health.IsAlive())
        {
            //transform.Rotate(new Vector3(0, 0, 1), _spinRate * Time.fixedTime);
            UpdateVelocity();
        }

    }

    private void UpdateVelocity()
    {
        Vector2 playerPos = _player.transform.position;
        Vector2 missilePos = transform.position;
        Vector2 missileVel = _body.velocity;

        Vector2 turnDirection = playerPos - missilePos;

        _body.velocity = turnDirection.normalized * _initialVelocity;
    }


    Vector2 getRandomXfixedY(float y)
    {
        return new Vector2(Random.Range(-_spawnAreaWidthBy2, _spawnAreaWidthBy2), y);
    }
}
