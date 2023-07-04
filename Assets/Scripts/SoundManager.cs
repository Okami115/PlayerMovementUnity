using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// control in-game sounds
/// </summary>
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource effectSource;
    [SerializeField] public AudioClip button;

    public AudioSource MusicSource { get => musicSource; set => musicSource = value; }

    /// <summary>
    /// Play a specific sound

    /// </summary>
    /// <param name="clip"></param>
    public void PlaySound(AudioClip clip)
    {
        effectSource.PlayOneShot(clip);
    }

    /// <summary>
    /// mute or unmute sounds and music
    /// </summary>
    public void ToggleAudio()
    {
        effectSource.mute = !effectSource.mute;
        MusicSource.mute = !MusicSource.mute;
        PlaySound(button);
    }

    /// <summary>
    /// Play a specific music
    /// </summary>
    public void PlayMusic(AudioClip clip)
    {
        MusicSource.clip = clip;
        MusicSource.Play();
    }

    /// <summary>
    /// stop all music
    /// </summary>
    public void StopMusic()
    {
        MusicSource.Stop();
    }

}
