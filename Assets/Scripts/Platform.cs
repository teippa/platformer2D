using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] bool _randomSprites;
    [SerializeField] Sprite[] _sprites;
    // Start is called before the first frame update
    void Start()
    {
        if (_randomSprites && _sprites.Length > 0) {
            Sprite rndSprite = _sprites[Random.Range(0, _sprites.Length - 1)];
            if (rndSprite != null)
            {
                GetComponent<SpriteRenderer>().sprite = rndSprite;
            }
        }
    }

}
