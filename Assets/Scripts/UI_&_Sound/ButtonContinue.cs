using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonContinue : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;
    private Pause _pauseScript;
    // Start is called before the first frame update
    void Start()
    {
        _pauseScript = _canvas.GetComponent<Pause>();
    }

    private void OnMouseUp()
    {
        _pauseScript.PauseTime(false);
        _pauseScript.DisplayPauseMenu(false);
    }

}
