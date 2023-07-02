using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource effectSource;
    [SerializeField] public AudioClip button;

    public AudioSource MusicSource { get => musicSource; set => musicSource = value; }

    public void PlaySound(AudioClip clip)
    {
        effectSource.PlayOneShot(clip);
    }

    public void ToggleAudio()
    {
        effectSource.mute = !effectSource.mute;
        MusicSource.mute = !MusicSource.mute;
        PlaySound(button);
    }

    public void PlayMusic(AudioClip clip)
    {
        MusicSource.clip = clip;
        MusicSource.Play();
    }

    public void StopMusic()
    {
        MusicSource.Stop();
    }

}
