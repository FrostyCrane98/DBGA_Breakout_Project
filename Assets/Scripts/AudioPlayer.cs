using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void Play(AudioClip _clip)
    {
        source.clip = _clip;
        source.Play();
    }

    public void Stop(AudioClip _clip)
    {
        source.clip = _clip;
        source.Stop();
    }

}
