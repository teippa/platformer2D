using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeLoadingScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        /*
         * Has to be done in a game object , because AudioManager initiates too early,
         * so the values would be over written when the AudioMixers load.
         * At least thet's my theory on that.
         * */

        AudioManager.instance.LoadVolumes();
    }

}
