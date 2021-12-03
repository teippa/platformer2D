using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{

    [SerializeField] Texture2D _cursor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;

        Vector2 hotspot = new Vector2(_cursor.width / 2, _cursor.height / 2);
        Cursor.SetCursor(_cursor, hotspot, CursorMode.Auto);
    }



}
