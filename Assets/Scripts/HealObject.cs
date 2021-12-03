using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealObject : MonoBehaviour
{

    [SerializeField] private ParticleSystem _ps;

    [SerializeField] private float _respawnDelay = 20;

    [SerializeField] private string _healSound;

    private SpriteRenderer _sprite;
    private Collider2D _collider;
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.ResetHealth();
            audioManager.PlaySound(_healSound);

            StartCoroutine(DelayedDeactivate());
        }
    }
    IEnumerator DelayedDeactivate()
    {
        ObjectEnabled(false);

        _ps.Emit(50);

        yield return new WaitForSeconds(_respawnDelay);
        ObjectEnabled(true);
        //Destroy(gameObject);
    }

    private void ObjectEnabled(bool _enabled)
    {
        _collider.enabled = _enabled;
        _sprite.enabled = _enabled;
    }
}
