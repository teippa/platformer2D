//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D _body;
    private BoxCollider2D _bodyCollider;
    private SpriteRenderer _sprite;

    [SerializeField] private float _maxVelocity;
    [SerializeField] private float _accelerationForce;
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask _groundLayerMask;

    [SerializeField] private Animator animator;

    private AudioManager audioManager;

    private float _moveDirection; // 1 or -1
    [HideInInspector] public float _lookDirection;
    private bool _jump;




    // Start is called before the first frame update
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _body = GetComponent<Rigidbody2D>();
        _bodyCollider = transform.GetComponent<BoxCollider2D>();
        //Debug.DrawLine(Vector3.zero, new Vector3(5, 0, 0), Color.white, 2.5f);

        audioManager = FindObjectOfType<AudioManager>();

        _lookDirection = 1;
    }


    // Update is called once per frame
    void Update()
    {
        if (!_jump && IsGrounded())
        {
            _jump = Input.GetKeyDown(KeyCode.Space);
        }

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            _moveDirection = 1;
            _sprite.flipX = false;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            _moveDirection = -1;
            _sprite.flipX = true;
        }
        else
        {
            _moveDirection = 0;
        }
    }

    private void FixedUpdate()
    {

        // Movement
        _body.AddForce(new Vector2(_moveDirection * MovementMultiplier(_body.velocity.magnitude), 0f));

        animator.SetFloat("speedX", Mathf.Abs(_body.velocity.normalized.x));
        animator.SetFloat("speedY", Mathf.Abs(_body.velocity.normalized.y));
        animator.SetBool("inAir", !IsGrounded());

        jumping();

    }


    float MovementMultiplier(float x)
    {
        float multiplier = -1 * x * x * (1 / _maxVelocity) + _accelerationForce;
        return multiplier > 0 ? multiplier : 0;
    }


    private void jumping()
    {
        if (_jump)
        {
            //Debug.Log(_jump);
            float jumpX = _jumpForce * _moveDirection;// * 1.5f;
            audioManager.PlaySound("PlayerJump");

            _body.AddForce(new Vector2(jumpX, _jumpForce), ForceMode2D.Impulse);
            //body.velocity = new Vector2(jumpForce * dashDirection * maxVelocity, jumpForce);
            _jump = false;
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(_bodyCollider.bounds.center, _bodyCollider.bounds.size, 0f, Vector2.down, 0.1f, _groundLayerMask);
        //Debug.Log(raycastHit2D.collider);
        bool grounded = raycastHit2D.collider != null;
        return grounded;
    }






}
