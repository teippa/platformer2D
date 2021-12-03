using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreCelebrationScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Display high score celebration text if achieved
        gameObject.SetActive(GameController.Instance.UpdateHighScores());
    }

    
}
