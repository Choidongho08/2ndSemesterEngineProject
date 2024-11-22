using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] private Image _audioVolumeFillImage;
    [SerializeField] private Button _volumDownButton, _volumUpButton;

    private string _path = string.Empty;
    private string _fileName = "SaveFile_AudioVolume";
    private string _volumeString;

    [Range(0.0f, 1.0f)] public float volume = 0.300f;
    public AudioSource audioSource;

    private void Awake()
    {
        _path = Application.persistentDataPath + "/";
        Debug.Log(_path);
        _volumDownButton.onClick.AddListener(AudioVolDown);
        _volumUpButton.onClick.AddListener(AudioVolUp);
        _volumeString = volume.ToString();
        ReadVolume(_path, _fileName);
    }
    private void Start()
    {
        audioSource.volume = volume;
        _audioVolumeFillImage.fillAmount = volume;
    }

    public void AudioVolUp()
    {
        volume += 0.100f;
        if(volume > 1.0f)
            volume = 1.0f;
        audioSource.volume = volume;
        _audioVolumeFillImage.fillAmount = volume;
        _volumeString = volume.ToString();
        WriteVolume(_path, _fileName);
        Debug.Log(volume);
    }
    public void AudioVolDown()
    {

        volume -= 0.100f;
        if (volume < 0.0f)
            volume = 0.0f;
        audioSource.volume = volume;
        _audioVolumeFillImage.fillAmount = volume;
        _volumeString = volume.ToString();
        WriteVolume(_path, _fileName);
        Debug.Log(volume);
    }
    private void ReadVolume(string path, string fileName)
    {
        if (File.Exists(path + fileName))
        {
            if (File.ReadAllText(path + fileName) != null)
            {
                _volumeString = File.ReadAllText(path + fileName);
                volume = float.Parse(_volumeString);
            }
            else
            {
                File.WriteAllText(path + fileName, _volumeString);
            }
        }
        else
        {
            File.WriteAllText(path + fileName, _volumeString);
        }
    }
    private void WriteVolume(string path, string fileName)
    {
        if (File.Exists(path + fileName))
            File.Delete(path + fileName);
        File.WriteAllText(path + fileName, _volumeString);
    }
}
