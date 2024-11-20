using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFeedBack : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private AudioSource _audioSource;

    [Range(0f, 1f)] public float volume = 0.3f;

    public void PlayClip()
    {
        if (_audioClip == null)
            return;

        _audioSource.volume = volume;
        _audioSource.PlayOneShot(_audioClip);
    }
}
