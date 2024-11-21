using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] private Image _audioVolumeFillImage;
    [SerializeField] private Button _volumDownButton, _volumUpButton;

    [Range(0.0f, 1.0f)] public float volume = 0.300f;
    public AudioSource audioSource;

    private void Awake()
    {
        _volumDownButton.onClick.AddListener(AudioVolDown);
        _volumUpButton.onClick.AddListener(AudioVolUp);
    }
    private void Start()
    {
        audioSource.volume = volume;
    }

    public void AudioVolUp()
    {
        volume += 0.100f;
        if(volume > 1.0f)
            volume = 1.0f;
        audioSource.volume = volume;
        Debug.Log(volume);
    }
    public void AudioVolDown()
    {

        volume -= 0.100f;
        if (volume < 0.0f)
            volume = 0.0f;
        audioSource.volume = volume;
        Debug.Log(volume);
    }
}
