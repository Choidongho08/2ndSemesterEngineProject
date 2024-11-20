using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] private Image _audioVolumeFillImage;
    [SerializeField] private Button _volumDownButton, _volumUpButton;


    public void SetAudioVol()
    {

    }
}
