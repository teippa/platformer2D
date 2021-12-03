using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayScore : MonoBehaviour
{
    private TextMeshProUGUI _textMesh;
    private string _initialText;
    // Start is called before the first frame update
    void Start()
    {

        _textMesh = GetComponent<TextMeshProUGUI>();
        _initialText = _textMesh.text;
    }

    private void FixedUpdate()
    {
        _textMesh.text = $"{_initialText}{GameController.Instance.PlayerScore}";

    }
}

