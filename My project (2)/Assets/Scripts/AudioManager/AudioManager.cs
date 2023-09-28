using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

public class AudioManager : MonoBehaviour
{
    public Sounds[] sounds;
    public AudioSource[] allAudioSources;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        

        foreach (Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    void Start()
    {
        
        allAudioSources = FindObjectsOfType<AudioSource>();
        Play("theme"); 
    }
    // Update is called once per frame
    public void Play(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
    public void Stop(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }
    public bool IsPlaying(string soundName)
    {
        foreach (Sounds s in sounds)
        {
            if (s.name == soundName)
            {
                return s.source.isPlaying;
            }
        }
        return false;
    }
    public void TurnOffAllSounds()
    {
        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.enabled = false;
        }
    }
    //public void changeVolume()
    //{
    //    AudioListener.volume = slider.value;
    //}
}
