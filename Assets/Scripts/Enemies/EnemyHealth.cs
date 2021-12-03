using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _defaultDamage = 50f;
    [SerializeField] private float _scoreOnKill = 20f;

    private DestroyParticleEffect _destroyScript;
    private float _health;
    private bool _alive;

    // Start is called before the first frame update
    void Start()
    {
        _destroyScript = GetComponent<DestroyParticleEffect>();

        ResetHealth();
    }

    /*
    // Update is called once per frame
    void Update()
    {

    }//*/


    public void TakeDamage(float dmgAmount = -1)
    {
        dmgAmount = (dmgAmount == -1) ? _defaultDamage : dmgAmount;
        _health -= dmgAmount;

        _alive = _health > 0;
        if (!_alive)
        {
            EnemyDeath();
        }
    }

    private void EnemyDeath()
    {
        _alive = false;
        GameController.Instance.IncreasePlayerScore(_scoreOnKill);
        FindObjectOfType<PlayerHealth>().IncreaseHealth(0.2f);
        FindObjectOfType<PlayerShooting>().ResetLaserCooldown();
        _destroyScript.DestroyWithParticles();
    }

    public void ResetHealth()
    {
        _health = _maxHealth;
        _alive = true;
    }

    public bool IsAlive()
    {
        return _alive;
    }
}

