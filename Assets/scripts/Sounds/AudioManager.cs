using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public SoundClass[] Sounds;
    void Awake()
    {
        foreach (SoundClass s in Sounds) {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;
            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;
            s.Source.loop = s.Loop;
          
        }
    }

    public void PlaySound(string name) {
        SoundClass s = Array.Find(Sounds, sound => sound.Name == name); //=> is like "where"
        s.Source.Play();
    }

    public float ClipLength(string name) {
        SoundClass s = Array.Find(Sounds, sound => sound.Name == name);
        return s.Source.clip.length;
    }

    public void SetnPlay(string name, float volume, float pitch) {
        SoundClass s = Array.Find(Sounds, sound => sound.Name == name);
        Mathf.Clamp(volume, 0, 1);
        s.Source.pitch = pitch;
        s.Source.volume = volume;
        s.Source.Play();
    }
}
