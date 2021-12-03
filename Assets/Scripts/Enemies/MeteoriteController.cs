using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class MeteoriteController : MonoBehaviour
{
    [SerializeField] private float _initialVelocity;

    private Rigidbody2D _body;
    //private BoxCollider2D _bodyCollider;

    private EnemyHealth _health;

    private float _spawnAreaY;
    private float _spawnAreaWidthBy2;


    private BoxCollider2D _parent;

    private float _spinRate;



    // Start is called before the first frame update
    void Start()
    {

        _body = GetComponent<Rigidbody2D>();
        _health = GetComponent<EnemyHealth>();

        _parent = GetComponentInParent<BoxCollider2D>();

        _spinRate = Random.Range(-.1f, .1f);

        if (_parent != null)
        {
            _spawnAreaY = _parent.transform.position.y;
            _spawnAreaWidthBy2 = _parent.size.x / 2;
            myInit();
        } else
        {
            _spawnAreaY = _body.transform.position.y;
            _spawnAreaWidthBy2 = 0;
        }



    }


    void FixedUpdate()
    {
        if(_body.position.y < -_spawnAreaY)
        {
            myInit();
        }
        //transform.Rotate(new Vector3(0, 0, 1), _spinRate * Time.fixedTime);

    }

    private void myInit()
    {
        _health.ResetHealth();
        _body.position = getRandomXfixedY(_spawnAreaY);

        Vector2 vel = getRandomXfixedY(-30) - _body.position;
        _body.velocity = vel.normalized * _initialVelocity;
    }

    Vector2 getRandomXfixedY(float y)
    {
        return new Vector2(Random.Range(-_spawnAreaWidthBy2, _spawnAreaWidthBy2) , y);
    }
}
