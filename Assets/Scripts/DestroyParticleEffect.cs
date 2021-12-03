using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(ParticleSystem))]

public class DestroyParticleEffect : MonoBehaviour
{
    private SpriteRenderer _sprite;
    private Collider2D _collider;

    [SerializeField] private ParticleSystem _DestroyParticles;
    [SerializeField] private string _destroySound;

    // Start is called before the first frame update
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        //_DestroyParticles = GetComponent<ParticleSystem>();


    }

    public void DestroyWithParticles()
    {
        SetObjectEnabled(false);
        if (_destroySound.Length>0)
        {
            FindObjectOfType<AudioManager>().PlaySound(_destroySound);
        }
        Destroy(gameObject, 1);

        _DestroyParticles.transform.rotation = _sprite.transform.rotation;
        _DestroyParticles.Emit(50);

    }


    public void SetObjectEnabled(bool _enabled)
    {
        _collider.enabled = _enabled;
        _sprite.enabled = _enabled;
    }
}
