using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] private float _playerMaxHealth;
    public float Health { get; private set; }

    [SerializeField] private GameObject _healthBar;

    // Start is called before the first frame update
    void Start()
    {
        ResetHealth();
        GameController.Instance.ResetPlayerScore();
    }


    private void PlayerDeath()
    {
        SceneLoader.Load(SceneLoader.Scene.GameOverScene);
    }

    public void ResetHealth()
    {
        Health = _playerMaxHealth;
        SetHealthBar(1f);
    }

    public void IncreaseHealth(float healAmount)
    {
        if (Health <= 20)
            healAmount *= 2;

        if (Health < _playerMaxHealth)
        {
            Health += healAmount;
            
            SetHealthBar(Health / _playerMaxHealth);
        }
    }
    public void TakeDamage(float dmgAmount)
    {
        Health -= dmgAmount;
        
        if (!IsAlive())
        {
            PlayerDeath();
        }
        SetHealthBar(Health / _playerMaxHealth);
    }

    private void SetHealthBar(float value)
    {
        if (_healthBar != null)
        {
            _healthBar.GetComponent<Image>().fillAmount = value;
        }
    }

    public bool IsAlive()
    {
        return 0 < Health;
    }

}
