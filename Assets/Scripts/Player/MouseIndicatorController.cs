//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class MouseIndicatorController : MonoBehaviour
{

    private Vector2 _mousePos;

    private Rigidbody2D _parent;
    private SpriteRenderer _self;

    [SerializeField] private Camera _cam;


    // Start is called before the first frame update
    void Start()
    {
        _self = GetComponent<SpriteRenderer>();
        _parent = _self.GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 lookDir = _mousePos - _parent.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x);

        //i.transform.localRotation = new Quaternion(0,0,1,angle);
        Quaternion rotation = new Quaternion
        {
            eulerAngles = new Vector3(0, 0, angle*Mathf.Rad2Deg)
        };
        _self.transform.localRotation = rotation;
        _self.transform.localPosition = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * _self.transform.localPosition.magnitude;
        //*/
        /*
        if (angle < 3 && !_particleSystem.isPlaying)
        {
            _particleSystem.Play();

        } else if (_particleSystem.isPlaying)
        {
            _particleSystem.Stop();

        }//*/
    }
}
