using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayHighScores : MonoBehaviour
{
    private TextMeshProUGUI _textMesh;
    private string _initialText;
    // Start is called before the first frame update
    void Start()
    {

        _textMesh = GetComponent<TextMeshProUGUI>();
        _initialText = _textMesh.text;

        List<float> s = GameController.Instance.HighScores;
        _textMesh.text = string.Format("1. {0:00000}\n2. {1:00000}\n3. {2:00000}\n", s[0], s[1] , s[2]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
