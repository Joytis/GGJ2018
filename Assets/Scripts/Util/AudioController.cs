using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    public static AudioController Instance;

    public AudioClip[] Clips;
    AudioSource _audio;

    void Awake () {
        if (Instance == null) {
            Instance = this;
        }
        Instance._audio = GetComponent<AudioSource>();
    }

    public void PlayAudio (int index) {
        Instance._audio.clip = Clips[index];
        Instance._audio.Play();
    }
}
