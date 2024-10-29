using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeNameUI : MonoSingleton<ChangeNameUI>
{
    [SerializeField] private TMP_InputField _inputPlayerName;
    [SerializeField] private Button _changeButton;
    [SerializeField] private Button _cancelButton;
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private GameObject _child;

    private string _playerName;
    

    private void Start()
    {
        MainLobby.instance.UpdatePlayerName(_playerName);
        _mainMenu.GetPlayerName(_playerName);
    }
    private void Awake()
    {
        string path = Application.persistentDataPath + "/";
        string fileName = "SaveFile";

        ReadPlayerName(path, fileName);
        _mainMenu.OnChangeName += () => _child.SetActive(true);
        _changeButton.onClick.AddListener(() =>
        {
            _playerName = _inputPlayerName.text;
            WritePlayerName(path, fileName);
            UpdatePlayerName();
            _mainMenu.GetPlayerName(_playerName);
            Hide();
            _inputPlayerName.text = string.Empty;
        });
        _cancelButton.onClick.AddListener(() => { Hide(); });
    }

    private void Hide()
    {
        _child.SetActive(false);
    }
    private void UpdatePlayerName()
    {
        MainLobby.instance.UpdatePlayerName(_playerName);
    }
    private void ReadPlayerName(string path, string fileName)
    {
        if (File.Exists(path + fileName))
            _playerName = File.ReadAllText(path + fileName);
        else
        {
            _playerName = "Player" + UnityEngine.Random.Range(1, 9090);
            File.WriteAllText(path + fileName, _playerName);
        }
    }
    private void WritePlayerName(string path, string fileName)
    {
        if (File.Exists(path + fileName))
            File.Delete(path + fileName);
        File.WriteAllText(path + fileName, _playerName);
    }
    public string GetPlayerName()
    {
        return _playerName;
    }
}
