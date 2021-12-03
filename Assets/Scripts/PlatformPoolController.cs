using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlatformPoolController : MonoBehaviour
{

    private Rigidbody2D _body;


    private Vector2 _startPos;
    private Vector2 _endPos;

    [SerializeField] private float _velocityMultiplier = 1;
    [SerializeField] private bool _singleUsePlatforms;

    [HideInInspector] public bool inUse = false;

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();

        _startPos = Vector2.up * 20f;
        _endPos = Vector2.down * 20f;

        if(_singleUsePlatforms)
        {
            _body.velocity = Vector2.down * _velocityMultiplier;
            _startPos = Vector2.down * 20f;
        }

        Debug.Log(gameObject.name + _startPos);

    }

    // Update is called once per frame
    void Update()
    {
        if (_body.position.y < _endPos.y)
        {
            DisablePlatforms();
        }//*/
    }

    public void EnablePlatforms()
    {
        _body.gameObject.SetActive(true);
        _body.MovePosition(_startPos);
        if (Random.value > 0.5f) // Possibility of mirroring platforms
            _body.transform.localScale = new Vector2(_body.transform.localScale.x * -1, 1);
        _body.velocity = Vector2.down * _velocityMultiplier;
        inUse = true;
    }

    public void DisablePlatforms()
    {
        _body.velocity = Vector2.zero;
        _body.MovePosition(_startPos);
        //_body.gameObject.SetActive(false); // This causes problems. For some resason the position is not changed to _startPos when this is uncommented
        inUse = false;
    }
}
