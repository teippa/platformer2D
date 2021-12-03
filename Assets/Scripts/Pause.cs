using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    private bool _paused;

    [SerializeField] GameObject _pauseOverlay;

    // Start is called before the first frame update
    void Start()
    {
        _paused = false;

        DisplayPauseMenu(_paused);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseTime(!_paused);
            DisplayPauseMenu(!_paused);
        }
    }



    public void PauseTime(bool pause)
    {
        if (pause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void DisplayPauseMenu(bool pause)
    {
        _pauseOverlay.SetActive(pause);
    }

    private void OnDestroy()
    {
        Time.timeScale = 1;
    }

}
