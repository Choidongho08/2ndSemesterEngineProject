using System.IO;
using System.Text.RegularExpressions;
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
    [SerializeField] private PlayerSO _playerSo;

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
            if (ChangePlayerName())
            {
                WritePlayerName(path, fileName);
                UpdatePlayerName();
                _mainMenu.GetPlayerName(_playerName);
                Hide();
                _inputPlayerName.text = string.Empty;
            }
            else
            {
                Hide();
            }
            
        });
        _cancelButton.onClick.AddListener(() => { Hide(); });
    }

    private bool ChangePlayerName()
    {
        _playerName = Regex.Replace(_inputPlayerName.text, @"[^0-9a-zA-Z°¡-ÆR]", "", RegexOptions.Singleline);
        if (!_inputPlayerName.text.Equals(_playerName) || _playerName == "")
        {
            Util.instance.LoadingHide();
            Debug.Log("Æ¯¼ö¹®ÀÚ ¾ÈµÅ! ÀÌ ¸ÓÀú¸®¾ß");
            _inputPlayerName.text = string.Empty;
            return false;
        }
        else
        {
            Debug.Log("¼º°ø!");
            _playerSo.playerName = _playerName;
            return true;
        }
    }
    private void Hide()
    {
        _child.SetActive(false);
    }
    private void UpdatePlayerName()
    {
        MainLobby.instance.UpdatePlayerName(_playerName);
        _playerSo.playerName = _playerName;
    }
    private void ReadPlayerName(string path, string fileName)
    {
        if (File.Exists(path + fileName))
        {
            if (File.ReadAllText(path + fileName) != null)
            {
                _playerName = File.ReadAllText(path + fileName);
            }
            else
            {
                _playerName = "Player" + UnityEngine.Random.Range(1, 9090);
                File.WriteAllText(path + fileName, _playerName);
            }
        }
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
        _playerSo.playerName = _playerName;
        return _playerName;
    }
}
