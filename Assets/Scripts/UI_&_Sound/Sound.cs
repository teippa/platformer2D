//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


[System.Serializable]
public class Sound
{
    [SerializeField] public string name;

    [SerializeField] public AudioClip clip;

    [Range(0f, 1f)]
    [SerializeField] public float volume;

    [Range(0.3f, 3f)]
    [SerializeField] public float pitch;

    [SerializeField] public bool loop;

    [SerializeField] public bool isMusic;

    [HideInInspector] public AudioSource source;

}
