using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DamagePlayer : MonoBehaviour
{
    //private Rigidbody2D _body;
    //private SpriteRenderer _sprite;
    //private Collider2D _collider;

    [SerializeField] private float _damageAmount;
    [SerializeField] private bool _killOnDamage;

    private DestroyParticleEffect _destroyScript;

    private void Start()
    {
        //_body = GetComponent<Rigidbody2D>();
        //_sprite = GetComponent<SpriteRenderer>();
        //_collider = GetComponent<Collider2D>();
        _destroyScript = GetComponent<DestroyParticleEffect>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(_damageAmount);

            
            if (_killOnDamage && _destroyScript != null)
            {
                _destroyScript.DestroyWithParticles();
            }
            

        }

    }

}

