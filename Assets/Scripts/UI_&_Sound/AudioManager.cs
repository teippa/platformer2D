using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

/*
 * Brackeys - Introduction to AUDIO in Unity
 * https://www.youtube.com/watch?v=6OT43pvUyfY&t=720s
 */

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _masterMixerGroup;
    [SerializeField] private AudioMixerGroup _musicMixerGroup;
    [SerializeField] private AudioMixerGroup _SFXMixerGroup;
    
    [SerializeField] private Sound[] sounds;

    public static AudioManager instance;


    private bool _inMainMenu;
    private string _currentSong;

    // Start is called before the first frame update
    void Awake()
    {
        #region Singleton, don't destroy
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        #endregion


        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            if (s.isMusic)
                s.source.outputAudioMixerGroup = _musicMixerGroup;
            else
                s.source.outputAudioMixerGroup = _SFXMixerGroup;
        }


    }


    private void LoadMixerVolume(AudioMixer mixer)
    {
        float vol = PlayerPrefs.GetFloat(mixer.name, -5f);

        mixer.SetFloat("Volume", vol);
    }

    public void LoadVolumes()
    {

        LoadMixerVolume(_masterMixerGroup.audioMixer);
        LoadMixerVolume(_musicMixerGroup.audioMixer);
        LoadMixerVolume(_SFXMixerGroup.audioMixer);
    }

    void Start()
    {
        ChangeSong("MainMusic");
        SceneManager.activeSceneChanged += PlayMusic;

    }

    private void ChangeSong(string songName)
    {
        _currentSong = songName;
        PlaySound(songName);
        _inMainMenu = songName == "MainMusic";
    }

    public void PlayMusic(Scene previousScene, Scene changedScene)
    {
        if (changedScene.name == SceneLoader.Scene.GameScene.ToString())
        {
            StopSoundFade("MainMusic", 2000);
            ChangeSong("GameMusic");
        } else if (_inMainMenu == false)
        {
            StopSoundFade("GameMusic", 1000);
            ChangeSong("MainMusic");
        }
    }

    public void StopSoundFade(string name, int fadeTimeMs)
    {   
        Sound s = FindSound(name);
        if (s != null)
        {
            StartCoroutine(StopSoundFadeCoroutine(s, fadeTimeMs));
        }
        else
        {
            Debug.LogWarning("Sound " + name + " Not found! Unable to stop sound.");
        }
    }
    private IEnumerator StopSoundFadeCoroutine(Sound sound, int fadeTimeMs)
    {
        
        float initialVolume = sound.source.volume;
        for (int i = fadeTimeMs; i>=0; i-=10)
        {
            yield return new WaitForSecondsRealtime(0.01f);
            sound.source.volume = initialVolume * i / fadeTimeMs;

            if (_currentSong == sound.name) // Return if song was restarted during loop
            {
                sound.source.volume = sound.volume;
                yield break; // Stops the Coroutine
            }
        }
            
        sound.source.Stop();
        sound.source.volume = sound.volume;
        yield return null;
    }


    public void PlaySound(string name)
    {
        Sound s = FindSound(name);
        if (s != null)
        {
            s.source.Play();
        }
        else
        {
            Debug.LogWarning("Sound " + name + " Not found! Unable to play sound.");
        }
    }
    public void StopSound(string name)
    {
        Sound s = FindSound(name);
        if (s != null)
        {
            s.source.Stop();
        }
        else
        {
            Debug.LogWarning("Sound " + name + " Not found! Unable to stop sound.");
        }
    }

    private Sound FindSound(string name)
    {
        return Array.Find(sounds, sound => sound.name == name);
    }


}
