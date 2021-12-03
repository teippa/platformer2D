using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ButtonController : MonoBehaviour
{
    [SerializeField] private SceneLoader.Scene nextScene;

    [SerializeField] private Color hoverColor;

    private SpriteRenderer sprite;
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnMouseUp()
    {
        //Debug.Log(_sceneToLoad.ToString());
        audioManager.PlaySound("ButtonClick");
        SceneLoader.Load(nextScene);
    }

    private void OnMouseEnter()
    {
        sprite.color = hoverColor;
        audioManager.PlaySound("ButtonHover");
    }
    private void OnMouseExit()
    {
        sprite.color = Color.white;
    }

}
