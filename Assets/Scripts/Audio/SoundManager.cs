using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource _musicSource, _effectSource;
    
    void Awake() {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(AudioClip clip) {
        _musicSource.clip = clip;
        _musicSource.Play();
    }

    public void PlayEffect(AudioClip clip, Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(clip, pos);
    }

    public void PlayEffect(AudioClip clip, Vector3 pos, float volume)
    {
        AudioSource.PlayClipAtPoint(clip, pos, volume);
    }

    public void ChangeMasterVolume(float value) {
        AudioListener.volume = value; 
    }

    public void ToggleEffects() {
        _effectSource.mute = !_effectSource.mute;
    }

    public void ToggleMusic() {
        _musicSource.mute = !_musicSource.mute;
    }
}
