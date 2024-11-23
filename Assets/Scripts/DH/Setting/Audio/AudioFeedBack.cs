using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFeedBack : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;

    

    public void PlayClip()
    {
        if (_audioClip == null)
            return;

        AudioManager.instance.audioSource.PlayOneShot(_audioClip);
    }
}
